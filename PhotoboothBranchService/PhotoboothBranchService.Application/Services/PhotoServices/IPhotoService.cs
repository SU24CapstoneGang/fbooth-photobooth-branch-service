using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoServices
{
    public interface IPhotoService : IServiceBase<PhotoResponse, PhotoFilter, PagingModel>
    {
        Task<PhotoResponse> CreatePhotoAsync(IFormFile file, CreatePhotoRequest createPhotoRequest);
        Task UpdateAsync(Guid id, UpdatePhotoRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
