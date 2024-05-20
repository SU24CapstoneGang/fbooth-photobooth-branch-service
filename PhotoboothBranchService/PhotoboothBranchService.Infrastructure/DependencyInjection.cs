using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using PhotoboothBranchService.Infrastructure.Repositories;
using System.ComponentModel.Design;
using System.Text;

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

            return services;
        }
    }
}
