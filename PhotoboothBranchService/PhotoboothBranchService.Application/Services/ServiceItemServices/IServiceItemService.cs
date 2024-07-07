using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public interface IServiceItemService : IServiceBase<ServiceItemResponse, CreateServiceItemRequest, CreateServiceItemResponse, UpdateServiceItemRequest, ServiceItemFilter, PagingModel>
    {
        public Task<AddListServiceItemResponse> AddListServiceItem(AddListServiceItemRequest request);
    }
}
