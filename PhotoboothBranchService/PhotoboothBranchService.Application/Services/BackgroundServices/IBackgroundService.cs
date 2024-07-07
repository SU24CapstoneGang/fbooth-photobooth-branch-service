using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BackgroundServices;

public interface IBackgroundService : IServiceBase<BackgroundResponse, BackgroundFilter, PagingModel>
{
    Task<IEnumerable<BackgroundResponse>> GetByName(string name);
    Task<BackgroundResponse> CreateBackgroundAsync(IFormFile file, CreateBackgroundRequest createModel);
    public Task<CreateBackgroundResponse> CreateAsync(CreateBackgroundRequest createModel);
    public Task UpdateAsync(Guid id, UpdateBackgroundRequest updateModel);
    public Task DeleteAsync(Guid id);
}
