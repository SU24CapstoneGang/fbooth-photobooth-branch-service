using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public interface ILayoutService : IServiceBase<LayoutResponse, LayoutFilter, PagingModel>
{
    Task<LayoutResponse> CreateLayoutAuto(IFormFile file);
    Task DeleteAsync(Guid id);
    Task UpdateLayoutAsync(IFormFile file, Guid BackGroundID, UpdateLayoutRequest updateLayoutRequest);

}
