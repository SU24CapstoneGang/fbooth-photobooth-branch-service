using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionOrderServices;

public interface ISessionOrderService : IService<SessionOrderResponse, CreateSessionOrderRequest, CreateSessionOrderResponse, UpdateSessionOrderRequest, SessionOrderFilter, PagingModel>
{
}
