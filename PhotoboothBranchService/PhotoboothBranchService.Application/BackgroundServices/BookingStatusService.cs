using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.BackgroundServices
{
    public class BookingStatusService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BookingStatusService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessSessionOrdersAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
            }
        }

        private async Task ProcessSessionOrdersAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
                var bookingServiceRepository = scope.ServiceProvider.GetRequiredService<IBookingServiceRepository>();
                var boothRepository = scope.ServiceProvider.GetRequiredService<IBoothRepository>();

                //delete service
                var now = DateTimeHelper.GetVietnamTimeNow();
                var bookings = (await bookingRepository.GetAsync(o =>
                    o.Status == BookingStatus.PendingPayment && o.PaymentStatus == PaymentStatus.Processing)).ToList();

                foreach (var booking in bookings)
                {
                    if ((now - booking.LastModified).TotalMinutes > 5)
                    {
                        var bookingServices = (await bookingServiceRepository.GetAsync(i => i.BookingID == booking.BookingID)).ToList();
                        var removalTasks = bookingServices.Select(bookingService =>
                            bookingServiceRepository.RemoveAsync(bookingService)
                            );
                        await Task.WhenAll(removalTasks);
                        await bookingRepository.RemoveAsync(booking);
                    }
                }

                //change booth to InUse
                bookings = (await bookingRepository.GetAsync(o =>
                    (o.Status == BookingStatus.PendingChecking) &&
                     o.StartTime <= now &&
                     o.EndTime >= now, i => i.Booth)).ToList();
                foreach (var order in bookings)
                {
                    if (order.Booth.Status == BoothStatus.Active)
                    {
                        var booth = order.Booth;
                        booth.isBooked = true;
                        await boothRepository.UpdateAsync(booth);
                    }
                }

                //no showing & booking
                bookings = (await bookingRepository.GetAsync(o =>
                    (o.Status == BookingStatus.PendingChecking || o.Status == BookingStatus.TakingPhoto) &&
                     o.EndTime <= now)).ToList();
                foreach (var booking in bookings)
                {
                    if (booking.Status == BookingStatus.PendingChecking){
                        booking.Status = BookingStatus.NoShow;
                    } else
                    {
                        booking.Status = BookingStatus.CompleteChecked;
                    }
                    await bookingRepository.UpdateAsync(booking);
                    var booth = (await boothRepository.GetAsync(i => i.BoothID == booking.BoothID)).FirstOrDefault();
                    if (booth != null)
                    {
                        booth.isBooked = false;
                        await boothRepository.UpdateAsync(booth);
                    }
                }

            }
        }
    }
}
