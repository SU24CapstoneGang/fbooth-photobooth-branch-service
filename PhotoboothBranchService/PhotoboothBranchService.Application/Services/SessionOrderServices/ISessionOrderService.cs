using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionOrderServices;

public interface ISessionOrderService : IServiceBase<SessionOrderResponse, SessionOrderFilter, PagingModel>
{
    Task<SessionOrderResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionOrderRequest);
    Task<CreateSessionOrderResponse> CreateAsync(CreateSessionOrderRequest createModel);
    Task UpdateAsync(Guid id, UpdateSessionOrderRequest updateModel);
    Task DeleteAsync(Guid id);
    Task CancelSessionOrder(Guid sessionOrdeID, string? ipAddress);
}
