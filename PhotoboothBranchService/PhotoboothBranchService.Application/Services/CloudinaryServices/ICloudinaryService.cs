using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace PhotoboothBranchService.Application.Services.CloudinaryServices
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string folder);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<ImageUploadResult> UpdatePhotoAsync(IFormFile file, string publicId);
    }
}
