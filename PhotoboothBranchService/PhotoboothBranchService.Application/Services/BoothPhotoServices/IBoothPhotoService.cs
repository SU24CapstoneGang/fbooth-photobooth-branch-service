using PhotoboothBranchService.Application.DTOs.BoothPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.BoothPhotoServices
{
    public interface IBoothPhotoService
    {
        Task<IEnumerable<BoothPhotoResponse>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<BoothPhotoResponse> GetByIdAsync(Guid id);
    }
}
