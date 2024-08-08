using PhotoboothBranchService.Application.DTOs.BranchPhoto;

namespace PhotoboothBranchService.Application.Services.BranchPhotoServices
{
    public interface IBranchPhotoService
    {
        Task<IEnumerable<BranchPhotoResponse>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<BranchPhotoResponse> GetByIdAsync(Guid id);
    }
}
