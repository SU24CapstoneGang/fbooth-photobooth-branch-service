using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.FinalPictureServices
{
    public interface IFinalPictureService : IService<FinalPictureResponse, CreateFinalPictureRequest, UpdateFinalPictureRequest, FinalPictureFilter, PagingModel>
    {
        public Task<FinalPictureResponse> CreateFinalPictureAsync(IFormFile file, Guid branchID, int photoTaken, Guid layoutID, string? discountCode, Guid? accountID);
    }
}
