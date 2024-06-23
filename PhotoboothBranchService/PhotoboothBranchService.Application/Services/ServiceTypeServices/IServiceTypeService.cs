using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public interface IServiceTypeService : IService<ServiceTypeResponse, CreateServiceTypeRequest, CreateServiceTypeResponse, UpdateServiceTypeRequest, ServiceTypeFilter, PagingModel>
    {
        Task<IEnumerable<ServiceTypeResponse>> GetByName(string name);
    }
}
