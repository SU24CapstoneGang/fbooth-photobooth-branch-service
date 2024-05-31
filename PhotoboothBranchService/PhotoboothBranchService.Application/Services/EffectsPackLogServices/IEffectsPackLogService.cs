using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.EffectsPackLog;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.EffectsPackLogServices
{
    public interface IEffectsPackLogService : IService<EffectsPackLogResponse,
        CreateEffectsPackLogRequest,
        UpdateEffectsPackLogRequest,
        EffectsPackLogFilter,
        PagingModel>
    {
    }
}
