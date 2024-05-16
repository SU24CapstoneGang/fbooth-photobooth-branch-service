using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Application.Mapping;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.CameraServices;
using PhotoboothBranchService.Application.Services.FilterServices;
using PhotoboothBranchService.Application.Services.FrameServices;
using PhotoboothBranchService.Application.Services.LayoutServices;
using PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;
using PhotoboothBranchService.Application.Services.PrinterServices;
using PhotoboothBranchService.Application.Services.RoleServices;
using PhotoboothBranchService.Application.Services.StickerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicaiton(this IServiceCollection services)
        {
            //Service
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICameraService, CameraService>();
            services.AddScoped<IPhotoBoothBranchService, PhotoBoothBranchService>();
            services.AddScoped<IPrinterService, PrinterService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<IFrameService, FrameService>();
            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<ILayoutService, LayoutService>();

            //Mapper
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
