using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public interface IServiceTypeService : IServiceBase<ServiceTypeResponse, ServiceTypeFilter, PagingModel>
    {
        Task<IEnumerable<ServiceTypeResponse>> GetByName(string name);
        Task<CreateServiceTypeResponse> CreateAsync(CreateServiceTypeRequest createModel);
        Task UpdateAsync(Guid id, UpdateServiceTypeRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
