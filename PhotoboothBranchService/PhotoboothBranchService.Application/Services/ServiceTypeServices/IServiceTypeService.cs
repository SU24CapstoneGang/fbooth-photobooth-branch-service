using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public interface IServiceTypeService : IServiceBase<ServiceTypeResponse, ServiceTypeFilter, PagingModel>
    {
        Task<IEnumerable<ServiceTypeResponse>> GetByName(string name);
        public Task<CreateServiceTypeResponse> CreateAsync(CreateServiceTypeRequest createModel);
        public Task UpdateAsync(Guid id, UpdateServiceTypeRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
