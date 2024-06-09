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
            services.AddScoped<IFilterRepository, FilterRepository>();
            services.AddScoped<IFrameRepository, FrameRepository>();
            services.AddScoped<ILayoutRepository, LayoutRepository>();
            services.AddScoped<IMapStickerRepository, MapStickerRepository>();
            services.AddScoped<IPhotoBoothBranchRepository, PhotoBoothBranchRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IStickerRepository, StickerRepository>();
            services.AddScoped<IThemeFrameRepository, ThemeFrameRepository>();

            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
