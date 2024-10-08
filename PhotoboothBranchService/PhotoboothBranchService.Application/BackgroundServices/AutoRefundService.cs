﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.BackgroundServices
{
    public class AutoRefundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoRefundService(IServiceProvider serviceProvider)
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
                    await ProcessRefundAsync();
                }
                finally
                {
                    BackgroundServiceLocks.ServiceLock.Release();
                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Check every day
            }
        }

        private async Task ProcessRefundAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
                var refundService = scope.ServiceProvider.GetRequiredService<IRefundService>();
                var bookings = (await bookingRepository.GetAsync(i => i.PaymentStatus == PaymentStatus.PendingRefund)).ToList();
                foreach (var booking in bookings)
                {
                    try
                    {
                        if (booking.BookingStatus == BookingStatus.Canceled)
                        {
                            await refundService.RefundByBookingID(booking.BookingID, false, null, null);
                        } else if (booking.BookingStatus == BookingStatus.CancelledBySystem)
                        {
                            await refundService.RefundByBookingID(booking.BookingID, true, null, null);
                        }
                    }
                    catch {}
                }

                var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();
                var payments = (await paymentRepository.GetAsync(i => i.Status == TransactionStatus.Redundant)).ToList();
                foreach (var payment in payments)
                {
                    try
                    {
                        await refundService.RefundByTransID(payment.PaymentID, true, null, null, false);
                    }
                    catch {}
                }
            }
        }
    }
}
