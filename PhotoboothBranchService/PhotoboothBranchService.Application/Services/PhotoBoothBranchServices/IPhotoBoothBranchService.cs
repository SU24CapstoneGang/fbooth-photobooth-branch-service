using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBoothBranch;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;

public interface IPhotoBoothBranchService : IService<PhotoBoothBranchresponse,
    CreatePhotoBoothBranchRequest,
    UpdatePhotoBoothBranchRequest,
    PhotoBoothBranchFilter,
    PagingModel>
{
    Task<IEnumerable<PhotoBoothBranchresponse>> SearchByName(string name);
    Task<IEnumerable<PhotoBoothBranchresponse>> GetByStatus(ManufactureStatus status);

}
