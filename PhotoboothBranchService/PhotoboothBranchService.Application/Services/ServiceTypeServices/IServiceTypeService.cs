using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public interface IServiceTypeService : IServiceBase<ServiceTypeResponse, ServiceTypeFilter, PagingModel>
    {
        Task<IEnumerable<ServiceTypeResponse>> GetByName(string name);
        Task<CreateServiceTypeResponse> CreateAsync(CreateServiceTypeRequest createModel, StatusUse status);
        Task UpdateAsync(Guid id, UpdateServiceTypeRequest updateModel, StatusUse? status);
        Task DeleteAsync(Guid id);
    }
}
