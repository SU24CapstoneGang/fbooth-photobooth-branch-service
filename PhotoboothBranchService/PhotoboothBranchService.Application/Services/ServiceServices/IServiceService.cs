using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceServices
{
    public interface IServiceService : IServiceBase<ServiceResponse, ServiceFilter, PagingModel>
    {
        Task<IEnumerable<ServiceResponse>> GetByName(string name);
        Task<CreateServiceResponse> CreateAsync(CreateServiceRequest createModel);
        Task UpdateAsync(Guid id, UpdateServiceRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
