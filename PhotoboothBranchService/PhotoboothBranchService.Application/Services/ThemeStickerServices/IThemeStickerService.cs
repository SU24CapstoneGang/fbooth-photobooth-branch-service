using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Role;
using PhotoboothBranchService.Application.DTOs.ThemeSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ThemeStickerServices
{
    public interface IThemeStickerService : IService<ThemeStickerResponse
        ,CreateThemeStickerRequest
        ,UpdateThemeStickerRequest
        ,ThemeStickerFilter
        ,PagingModel>
    {
        Task<IEnumerable<ThemeStickerResponse>> GetByName(string name);
    }
}
