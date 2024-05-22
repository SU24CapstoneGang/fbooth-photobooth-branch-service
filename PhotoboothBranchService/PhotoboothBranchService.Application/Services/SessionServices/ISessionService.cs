using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Session;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionServices;

public interface ISessionService : IService<SessionResponse, CreateSessionRequest, UpdateSessionRequest, SessionFilter, PagingModel>
{
}
