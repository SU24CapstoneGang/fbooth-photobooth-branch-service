using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionOrderServices;

public interface ISessionOrderService : IServiceBase<SessionOrderResponse, SessionOrderFilter, PagingModel>
{
    public Task<SessionOrderResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionOrderRequest);
    public Task<CreateSessionOrderResponse> CreateAsync(CreateSessionOrderRequest createModel);
    public Task UpdateAsync(Guid id, UpdateSessionOrderRequest updateModel);
    public Task DeleteAsync(Guid id);
}
