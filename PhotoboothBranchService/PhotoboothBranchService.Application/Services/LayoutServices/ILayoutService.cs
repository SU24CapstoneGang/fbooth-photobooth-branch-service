using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public interface ILayoutService : IService<LayoutResponse, CreateLayoutRequest, CreateLayoutResponse, UpdateLayoutRequest, LayoutFilter, PagingModel>
{
    Task<LayoutResponse> CreateLayoutAsync(IFormFile file, CreateLayoutRequest createModel);
}
