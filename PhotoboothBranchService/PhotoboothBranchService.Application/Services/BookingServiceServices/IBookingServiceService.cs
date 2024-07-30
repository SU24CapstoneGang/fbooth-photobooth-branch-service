using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public interface IServiceItemService : IServiceBase<ServiceItemResponse, ServiceItemFilter, PagingModel>
    {
        Task<AddListServiceItemResponse> AddListServiceItem(AddListServiceItemRequest request);
        Task<CreateServiceItemResponse> CreateAsync(CreateServiceItemRequest createModel);
        Task UpdateAsync(Guid id, UpdateServiceItemRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
