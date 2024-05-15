using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Application.Mapping;
using PhotoboothBranchService.Application.Services.CustomerService;
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
