using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.ServiceServices
{
    public interface IServiceService : IServiceBase<ServiceResponse, ServiceFilter, PagingModel>
    {
        Task<IEnumerable<ServiceResponse>> GetByName(string name);
        Task<CreateServiceResponse> CreateAsync(CreateServiceRequest createModel, StatusUse status);
        Task UpdateAsync(Guid id, UpdateServiceRequest updateModel, StatusUse? status);
        Task DeleteAsync(Guid id);
    }
}
