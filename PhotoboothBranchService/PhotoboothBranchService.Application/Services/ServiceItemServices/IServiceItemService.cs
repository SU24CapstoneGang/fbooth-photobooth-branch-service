using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public interface IServiceItemService : IService<ServiceItemResponse, CreateServiceItemRequest, CreateServiceItemResponse, UpdateServiceItemRequest, ServiceItemFilter, PagingModel>
    {
        Task AddTheFirstServiceItem(Guid SessionOrderID, Guid ServiceID);
    }
}
