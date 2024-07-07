using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.LayoutServices;

public interface ILayoutService : IServiceBase<LayoutResponse, LayoutFilter, PagingModel>
{
    Task<LayoutResponse> CreateLayoutAsync(IFormFile file, CreateLayoutRequest createModel);
    Task<LayoutResponse> CreateLayoutAuto(IFormFile file);
    public Task<CreateLayoutResponse> CreateAsync(CreateLayoutRequest createModel);
    public Task UpdateAsync(Guid id, UpdateLayoutRequest updateModel);
    public Task DeleteAsync(Guid id);
}
