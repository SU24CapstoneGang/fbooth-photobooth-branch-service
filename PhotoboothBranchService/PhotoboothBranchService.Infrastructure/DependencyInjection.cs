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
            services.AddScoped<ICameraRepository, CameraRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IEffectsPackLogRepository, EffectsPackLogRepository>();
            services.AddScoped<IFilterRepository, FilterRepository>();
            services.AddScoped<IFinalPictureRepository, FinalPictureRepository>();
            services.AddScoped<IFrameRepository, FrameRepository>();
            services.AddScoped<ILayoutRepository, LayoutRepository>();
            services.AddScoped<IMapStickerRepository, MapStickerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPhotoBoothBranchRepository, PhotoBoothBranchRepository>();
            services.AddScoped<IPrinterRepository, PrinterRepository>();
            services.AddScoped<IPrintPricingRepository, PrintPricingRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IStickerRepository, StickerRepository>();
            services.AddScoped<IThemeFilterRepository, ThemeFilterRepository>();
            services.AddScoped<IThemeFrameRepository, ThemeFrameRepository>();
            services.AddScoped<IThemeStickerRepository, ThemeStickerRepository>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();

            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
