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
                var boothRepository = scope.ServiceProvider.GetRequiredService<IBoothRepository>();

                //cancel service
                var now = DateTimeHelper.GetVietnamTimeNow();
                var orders = (await bookingRepository.GetAsync(o =>
                    o.Status == BookingStatus.PendingPayment)).ToList();

                foreach (var order in orders)
                {
                    if ((now - order.CreatedDate).TotalMinutes > 15)
                    {
                        order.IsCancelled = true;
                        await bookingRepository.UpdateAsync(order);
                    }
                }

                //change booth to InUse
                orders = (await bookingRepository.GetAsync(o =>
                    (o.Status == BookingStatus.PendingChecking) &&
                     o.StartTime <= now &&
                     o.EndTime >= now, i => i.Booth)).ToList();
                foreach (var order in orders)
                {
                    if (order.Booth.Status == BoothStatus.Active)
                    {
                        var booth = order.Booth;
                        booth.isBooked = true;
                        await boothRepository.UpdateAsync(booth);
                    }
                }

                //no showing
                orders = (await bookingRepository.GetAsync(o =>
                    (o.Status == BookingStatus.PendingChecking) &&
                     o.EndTime <= now)).ToList();
                foreach (var order in orders)
                {
                    order.Status = BookingStatus.NoShow;
                    await bookingRepository.UpdateAsync(order);
                    var booth = (await boothRepository.GetAsync(i => i.BoothID == order.BoothID)).FirstOrDefault();
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
