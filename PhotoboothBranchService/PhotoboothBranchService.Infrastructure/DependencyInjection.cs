﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Application.Services.PhotoBoxServices;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using PhotoboothBranchService.Infrastructure.Repositories;

namespace PhotoboothBranchService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddDbContext<AppDbContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("Fbooth")));

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBoothRepository, BoothRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IBackgroundRepository, BackgroundRepository>();
            services.AddScoped<IConstantRepository, ConstantRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ILayoutRepository, LayoutRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPhotoBoxRepository, PhotoBoxRepository>();
            services.AddScoped<IRefundRepository, RefundRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IPhotoSessionRepository, PhotoSessionRepository>();
            services.AddScoped<IPhotoStickerRepository, PhotoStickerRepository>();
            services.AddScoped<IServiceSessionRepository, ServiceSessionRepository>();
            services.AddScoped<IServicePackageRepository, ServicePackageRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<ISessionOrderRepository, SessionOrderRepository>();
            services.AddScoped<IStickerRepository, StickerRepository>();
            services.AddScoped<IPhotoBoxRepository, PhotoBoxRepository>();

            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
