using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IBoothBranchRepository, BoothBranchRepository>();
            services.AddScoped<IBackgroundRepository, BackgroundRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ILayoutRepository, LayoutRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IPhotoSessionRepository, PhotoSessionRepository>();
            services.AddScoped<IPhotoStickerRepository, PhotoStickerRepository>();
            services.AddScoped<IServiceItemRepository, ServiceItemRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<ISessionOrderRepository, SessionOrderRepository>();
            services.AddScoped<ISessionPackageRepository, SessionPackageRepository>();
            services.AddScoped<IStickerRepository, StickerRepository>();

            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
