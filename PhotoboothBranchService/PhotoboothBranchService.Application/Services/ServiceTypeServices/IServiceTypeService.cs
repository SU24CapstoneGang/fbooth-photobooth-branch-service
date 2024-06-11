using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public interface IServiceTypeService : IService<ServiceTypeResponse,CreateServiceTypeRequest,UpdateServiceTypeRequest,ServiceTypeFilter,PagingModel>
    {
    }
}
