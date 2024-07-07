using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceServices
{
    public interface IServiceService : IServiceBase<ServiceResponse, ServiceFilter, PagingModel>
    {
        Task<IEnumerable<ServiceResponse>> GetByName(string name);
        public Task<CreateServiceResponse> CreateAsync(CreateServiceRequest createModel);
        public Task UpdateAsync(Guid id, UpdateServiceRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
