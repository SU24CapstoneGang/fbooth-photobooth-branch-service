using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BackgroundServices;

public interface IBackgroundService : IService<BackgroundResponse, CreateBackgroundRequest, CreateBackgroundResponse, UpdateBackgroundRequest, BackgroundFilter, PagingModel>
{
    Task<IEnumerable<BackgroundResponse>> GetByName(string name);
    Task<BackgroundResponse> CreateBackgroundAsync(IFormFile file, Guid layoutID);
    Task UpdateBackGroundAsync(IFormFile file, Guid BackGroundID, UpdateBackgroundRequest updateBackgroundRequest);

}
