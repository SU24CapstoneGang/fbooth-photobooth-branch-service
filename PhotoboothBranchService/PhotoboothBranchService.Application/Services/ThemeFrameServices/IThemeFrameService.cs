using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Role;
using PhotoboothBranchService.Application.DTOs.ThemeFrame;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
