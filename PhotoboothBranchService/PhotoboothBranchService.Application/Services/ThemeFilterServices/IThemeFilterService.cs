using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Role;
using PhotoboothBranchService.Application.DTOs.ThemeFilter;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
