using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Frame;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.FrameServices;

public interface IFrameService : IService<FrameResponse, CreateFrameRequest, UpdateFrameRequest, FrameFilter, PagingModel>
{
    Task<IEnumerable<FrameResponse>> GetByName(string name);
}
