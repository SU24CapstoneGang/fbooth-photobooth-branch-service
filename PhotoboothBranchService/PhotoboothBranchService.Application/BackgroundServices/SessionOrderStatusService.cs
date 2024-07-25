﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoboothBranchService.Application.Services.PaymentServices;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.BackgroundServices
{
    public class SessionOrderStatusService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SessionOrderStatusService(IServiceProvider serviceProvider)
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
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var sessionOrderRepository = scope.ServiceProvider.GetRequiredService<ISessionOrderRepository>();
            //    var boothRepository = scope.ServiceProvider.GetRequiredService<IBoothRepository>();

            //    //cancel service
            //    var now = DateTime.Now;
            //    var orders = (await sessionOrderRepository.GetAsync(o =>
            //        (o.Status == SessionOrderStatus.Deposited ||
            //         o.Status == SessionOrderStatus.Created) &&
            //         o.StartTime <= now
            //         )).ToList();

            //    foreach (var order in orders)
            //    {
            //        if ((now - order.StartTime).TotalMinutes > 15)
            //        {
            //            order.Status = SessionOrderStatus.Canceled;
            //            await sessionOrderRepository.UpdateAsync(order);
            //            var booth = (await boothRepository.GetAsync(i => i.BoothID == order.BoothID)).FirstOrDefault();
            //            if (booth != null)
            //            {
            //                booth.Status = BoothStatus.Active;
            //                await boothRepository.UpdateAsync(booth);
            //            }
            //        }
            //    }

            //    //change booth to InUse
            //    orders = (await sessionOrderRepository.GetAsync(o =>
            //        (o.Status == SessionOrderStatus.Deposited ||
            //         o.Status == SessionOrderStatus.Created ||
            //         o.Status == SessionOrderStatus.Waiting) &&
            //         o.StartTime <= now &&
            //         o.EndTime >= now, i => i.Booth)).ToList();
            //    foreach (var order in orders)
            //    {
            //        if (order.Booth.Status == BoothStatus.Active)
            //        {
            //            var booth = order.Booth;
            //            booth.Status = BoothStatus.InUse;
            //            await boothRepository.UpdateAsync(booth);
            //        }
            //    }

            //    //end session service
            //    orders = (await sessionOrderRepository.GetAsync(o =>
            //        (o.Status == SessionOrderStatus.Waiting ||
            //         o.Status == SessionOrderStatus.Processsing) &&
            //         o.EndTime <= now)).ToList();
            //    foreach (var order in orders)
            //    {
            //        if ((now - order.EndTime.Value).TotalMinutes > 5)
            //        {
            //            order.Status = SessionOrderStatus.Done;
            //            await sessionOrderRepository.UpdateAsync(order);
            //            var booth = (await boothRepository.GetAsync(i => i.BoothID == order.BoothID)).FirstOrDefault();
            //            if (booth != null)
            //            {
            //                booth.Status = BoothStatus.Active;
            //                await boothRepository.UpdateAsync(booth);
            //            }
            //        }
            //    }
            //}
        }
    }
}
