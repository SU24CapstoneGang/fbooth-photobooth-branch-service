using AutoMapper.Internal;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.CameraServices;
using PhotoboothBranchService.Application.Services.DiscountServices;
using PhotoboothBranchService.Application.Services.FilterServices;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.FrameServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Application.Services.LayoutServices;
using PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;
using PhotoboothBranchService.Application.Services.PrinterServices;
using PhotoboothBranchService.Application.Services.RoleServices;
using PhotoboothBranchService.Application.Services.StickerServices;
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
            services.AddScoped<IPhotoBoothBranchService, PhotoBoothBranchService>();
            services.AddScoped<IPrinterService, PrinterService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<IFrameService, FrameService>();
            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IFirebaseService, FirebaseService>();


            //Password hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            //JWT service
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
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


            //jwt service
            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidateLifetime = true,
            //        ValidIssuer = configuration["JwtSettings:Issuer"],
            //        ValidAudience = configuration["JwtSettings:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
            //    };
            //});

            //Mapper
            //services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
