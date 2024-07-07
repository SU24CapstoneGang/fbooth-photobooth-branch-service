using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceServices
{
    public interface IServiceService : IServiceBase<ServiceResponse, CreateServiceRequest, CreateServiceResponse, UpdateServiceRequest, ServiceFilter, PagingModel>
    {
        Task<IEnumerable<ServiceResponse>> GetByName(string name);
    }
}
