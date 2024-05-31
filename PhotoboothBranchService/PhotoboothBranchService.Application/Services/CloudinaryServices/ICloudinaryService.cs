using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace PhotoboothBranchService.Application.Services.CloudinaryServices
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
