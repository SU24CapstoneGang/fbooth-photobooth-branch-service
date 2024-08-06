using AutoMapper.Internal;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Application.BackgroundServices;
using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.BackgroundServices;
using PhotoboothBranchService.Application.Services.BookingServices;
using PhotoboothBranchService.Application.Services.BookingServiceServices;
using PhotoboothBranchService.Application.Services.BoothServices;
using PhotoboothBranchService.Application.Services.BranchServices;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.DashboardServices;
using PhotoboothBranchService.Application.Services.DeviceServices;
using PhotoboothBranchService.Application.Services.EmailServices;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.FullPaymentPolicyServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Application.Services.LayoutServices;
using PhotoboothBranchService.Application.Services.MoMoServices;
using PhotoboothBranchService.Application.Services.PaymentMethodServices;
using PhotoboothBranchService.Application.Services.PhotoBoxServices;
using PhotoboothBranchService.Application.Services.PhotoServices;
using PhotoboothBranchService.Application.Services.PhotoSessionServices;
using PhotoboothBranchService.Application.Services.PhotoStickerServices;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Application.Services.ServiceServices;
using PhotoboothBranchService.Application.Services.StickerServices;
using PhotoboothBranchService.Application.Services.TransactionServices;
using PhotoboothBranchService.Application.Services.VNPayServices;
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
            services.AddScoped<IBoothService, BoothService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IBackgroundService, BackgroundService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IPhotoBoxService, PhotoBoxService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoSessionService, PhotoSessionService>();
            services.AddScoped<IPhotoStickerService, PhotoStickerService>();
            services.AddScoped<IRefundService, RefundService>();
            services.AddScoped<IBookingServiceService, BookingServiceService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<IPhotoBoxService, PhotoBoxService>();
            services.AddScoped<IFullPaymentPolicyServices, FullPaymentPolicyServices>();

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

            //Vnpay Service
            services.AddScoped<IVNPayService, VNPayService>();
            //momo service
            services.AddScoped<IMoMoService, MoMoService>();
            //email sender service
            services.AddScoped<IEmailService, EmailService>();
            //background service
            services.AddHostedService<BookingStatusService>();
            return services;
        }
    }
}
