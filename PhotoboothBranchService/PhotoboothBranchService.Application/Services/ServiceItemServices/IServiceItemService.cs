using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public interface IServiceItemService : IServiceBase<ServiceItemResponse, ServiceItemFilter, PagingModel>
    {
        public Task<AddListServiceItemResponse> AddListServiceItem(AddListServiceItemRequest request);
        public Task<CreateServiceItemResponse> CreateAsync(CreateServiceItemRequest createModel);
        public Task UpdateAsync(Guid id, UpdateServiceItemRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
