using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeFilter;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ThemeFilterServices
{
    public interface IThemeFilterService : IService<ThemeFilterResponse,
        CreateThemeFilterRequest,
        UpdateThemeFilterRequest,
        ThemeFilterFilter,
        PagingModel>
    {
        Task<IEnumerable<ThemeFilterResponse>> GetByName(string name);
    }
}
