using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeFrame;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ThemeFrameServices
{
    public interface IThemeFrameService : IService<ThemeFrameResponse,
        CreateThemeFrameRequest,
        UpdateThemeFrameRequest,
        ThemeFrameFilter,
        PagingModel>
    {
        Task<IEnumerable<ThemeFrameResponse>> GetByName(string name);
    }
}
