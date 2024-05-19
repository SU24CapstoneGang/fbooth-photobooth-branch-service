using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Frame;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Frame;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.FrameServices;

public interface IFrameService : IService<FrameResponse,CreateFrameRequest,UpdateFrameRequest,FrameFilter,PagingModel>
{
    Task<IEnumerable<FrameResponse>> GetByName(string name);
}
