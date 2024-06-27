using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoSessionServices
{
    public interface IPhotoSessionService : IService<PhotoSessionResponse, CreatePhotoSessionRequest, CreatePhotoSessionResponse, UpdatePhotoSessionRequest, PhotoSessionFilter, PagingModel>
    {
    }
}
