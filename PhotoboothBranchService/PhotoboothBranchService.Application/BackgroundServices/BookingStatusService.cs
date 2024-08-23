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
                await BackgroundServiceLocks.ServiceLock.WaitAsync(stoppingToken);
                try
                {
                    await ProcessBookingsAsync();
                }
                finally
                {
                    BackgroundServiceLocks.ServiceLock.Release();
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
            }
        }

        private async Task ProcessBookingsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
                var bookingServiceRepository = scope.ServiceProvider.GetRequiredService<IBookingServiceRepository>();
                var boothRepository = scope.ServiceProvider.GetRequiredService<IBoothRepository>();
                var photoSessionRepository = scope.ServiceProvider.GetRequiredService<IPhotoSessionRepository>();
                var bookingSlotRepository = scope.ServiceProvider.GetRequiredService<IBookingSlotRepository>();
                //delete service
                var now = DateTimeHelper.GetVietnamTimeNow();
                var bookings = (await bookingRepository.GetAsync(o =>
                    o.BookingStatus == BookingStatus.PendingPayment && o.PaymentStatus == PaymentStatus.Processing)).ToList();

                foreach (var booking in bookings)
                {
                    if ((now - booking.LastModified).TotalMinutes > 15)
                    {
                        var bookingServices = (await bookingServiceRepository.GetAsync(i => i.BookingID == booking.BookingID)).ToList();
                        var bookingSlots = (await bookingSlotRepository.GetAsync(i => i.BookingID == booking.BookingID)).ToList();
                        
                        var removalTasks = bookingServices.Select(bookingService =>
                            bookingServiceRepository.RemoveAsync(bookingService)
                            );
                        await Task.WhenAll(removalTasks); 
                        
                        removalTasks = bookingSlots.Select(bookingSlot =>
                            bookingSlotRepository.RemoveAsync(bookingSlot)
                            );
                        await Task.WhenAll(removalTasks);

                        await bookingRepository.RemoveAsync(booking);
                    }
                }

                //change booth to InUse
                bookings = (await bookingRepository.GetAsync(o =>
                    (o.BookingStatus == BookingStatus.PendingChecking) &&
                     o.StartTime <= now &&
                     o.EndTime >= now, i => i.Booth)).ToList();
                foreach (var order in bookings)
                {
                    if (order.Booth.Status == BoothStatus.Active)
                    {
                        var booth = order.Booth;
                        booth.Status = BoothStatus.Booked;
                        await boothRepository.UpdateAsync(booth);
                    }
                }

                //no showing & finish booking
                bookings = (await bookingRepository.GetAsync(o =>
                    (o.BookingStatus == BookingStatus.PendingChecking || o.BookingStatus == BookingStatus.TakingPhoto || o.BookingStatus == BookingStatus.ExtraService) &&
                     o.EndTime <= now)).ToList();
                foreach (var booking in bookings)
                {
                    if (booking.BookingStatus == BookingStatus.PendingChecking){
                        booking.BookingStatus = BookingStatus.NoShow;
                    } else
                    {
                        booking.BookingStatus = BookingStatus.CompleteChecked;
                        var photoSessions = (await photoSessionRepository.GetAsync(i => i.BookingID == booking.BookingID)).ToList();
                        if (photoSessions != null && photoSessions.Count > 0)
                        {
                            var photoSession = photoSessions.MaxBy(i => i.SessionIndex);
                            if (photoSession != null && photoSession.Status == PhotoSessionStatus.Ongoing)
                            {
                                photoSession.Status = PhotoSessionStatus.Ended;
                                await photoSessionRepository.UpdateAsync(photoSession);
                            }
                        }
                    }
                    await bookingRepository.UpdateAsync(booking);
                    var booth = (await boothRepository.GetAsync(i => i.BoothID == booking.BoothID)).FirstOrDefault();
                    if (booth != null)
                    {
                        booth.Status = BoothStatus.Active;
                        await boothRepository.UpdateAsync(booth);
                    }
                }

            }
        }
    }
}
