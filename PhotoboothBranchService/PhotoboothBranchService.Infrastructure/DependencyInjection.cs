using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Services.AuthentiacationService;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Authentication;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using PhotoboothBranchService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Fbooth"),
            b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
            ServiceLifetime.Scoped);

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPrinterRepository, PrinterRepository>();
            services.AddScoped<ICameraRepository, CameraRepository>();
            services.AddScoped<IPhotoBoothBranchRepository, PhotoBoothBranchRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IFrameRepository, FrameRepository>();
            services.AddScoped<IFilterRepository, FilterRepository>();
            services.AddScoped<IStickerRepository, StickerRepository>();
            services.AddScoped<ILayoutRepository, LayoutRepository>();

            services.AddScoped<IMapper, Mapper>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
                };
            });
            return services;
        }
    }
}
