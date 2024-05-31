using AutoMapper.Internal;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.CameraServices;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.DiscountServices;
using PhotoboothBranchService.Application.Services.EffectsPackLogServices;
using PhotoboothBranchService.Application.Services.FilterServices;
using PhotoboothBranchService.Application.Services.FinalPictureServices;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.FrameServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Application.Services.LayoutServices;
using PhotoboothBranchService.Application.Services.MapStickerServices;
using PhotoboothBranchService.Application.Services.PaymentMethodServices;
using PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;
using PhotoboothBranchService.Application.Services.PrinterServices;
using PhotoboothBranchService.Application.Services.PrintPricingServices;
using PhotoboothBranchService.Application.Services.RoleServices;
using PhotoboothBranchService.Application.Services.SessionServices;
using PhotoboothBranchService.Application.Services.StickerServices;
using PhotoboothBranchService.Application.Services.ThemeFilterServices;
using PhotoboothBranchService.Application.Services.ThemeFrameServices;
using PhotoboothBranchService.Application.Services.ThemeStickerServices;
using PhotoboothBranchService.Application.Services.TransactionHistoryServices;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System.Reflection;

namespace PhotoboothBranchService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicaiton(this IServiceCollection services, ConfigurationManager configuration)
        {
            //Service
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICameraService, CameraService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IEffectsPackLogService, EffectsPackLogService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<IFinalPictureService, FinalPictureService>();
            services.AddScoped<IFrameService, FrameService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IMapStickerService, MapStickerService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IPhotoBoothBranchService, PhotoBoothBranchService>();
            services.AddScoped<IPrinterService, PrinterService>();
            services.AddScoped<IPrintPricingService, PrintPricingService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<IThemeFilterService, ThemeFilterService>();
            services.AddScoped<IThemeFrameService, ThemeFrameService>();
            services.AddScoped<IThemeStickerService, ThemeStickerService>();
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();

            // cloudinary
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            //Firebase
            services.AddScoped<IFirebaseService, FirebaseService>();

            //Password hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            ////JWT service
            //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            // Add http client
            services.AddHttpClient<IJwtService, JwtService>();

            //Mapper config
            services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, Assembly.GetExecutingAssembly());

            //firebase authen config
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("firebase.json")
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration["FirebaseJwt:Issuer"];
                options.Audience = configuration["FirebaseJwt:Audience"];
                options.TokenValidationParameters.ValidIssuer = configuration["FirebaseJwt:Issuer"];
            });

            return services;
        }
    }
}
