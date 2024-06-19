using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Theme;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ThemeServices
{
    public interface IThemeService : IService<ThemeResponse,
        CreateThemeRequest,
        UpdateThemeRequest,
        ThemeFilter,
        PagingModel>
    {
        Task<IEnumerable<ThemeResponse>> GetByName(string name);
    }
}
