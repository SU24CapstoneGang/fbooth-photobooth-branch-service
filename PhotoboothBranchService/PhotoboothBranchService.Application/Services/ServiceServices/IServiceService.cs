using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ServiceServices
{
    public interface IServiceService : IService<ServiceResponse,CreateServiceRequest,UpdateServiceRequest,ServiceFilter,PagingModel>
    {
        Task<IEnumerable<ServiceResponse>> GetByName(string name);
    }
}
