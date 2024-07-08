using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BackgroundServices;

public interface IBackgroundService : IServiceBase<BackgroundResponse, BackgroundFilter, PagingModel>
{
    public Task<IEnumerable<BackgroundResponse>> GetByName(string name);
    public Task DeleteAsync(Guid id);
    public Task<BackgroundResponse> CreateBackgroundAsync(IFormFile file, Guid layoutID);
    public Task UpdateBackGroundAsync(IFormFile file, Guid BackGroundID, UpdateBackgroundRequest updateBackgroundRequest);

}
