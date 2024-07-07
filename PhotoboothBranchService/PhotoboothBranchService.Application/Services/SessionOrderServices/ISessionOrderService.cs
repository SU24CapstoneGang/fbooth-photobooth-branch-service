using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionOrderServices;

public interface ISessionOrderService : IServiceBase<SessionOrderResponse, CreateSessionOrderRequest, CreateSessionOrderResponse, UpdateSessionOrderRequest, SessionOrderFilter, PagingModel>
{
    public Task<SessionOrderResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionOrderRequest);

}
