using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.ThemeStickerServices
{
    public interface IThemeStickerService : IService<ThemeStickerResponse
        , CreateThemeStickerRequest
        , UpdateThemeStickerRequest
        , ThemeStickerFilter
        , PagingModel>
    {
        Task<IEnumerable<ThemeStickerResponse>> GetByName(string name);
    }
}
