using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public interface IServiceItemService : IService<ServiceItemResponse,CreateServiceItemRequest,UpdateServiceItemRequest,ServiceItemFilter,PagingModel>
    {
    }
}
