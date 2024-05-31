using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FinalPictureServices
{
    public interface IFinalPictureService : IService<FinalPictureResponse, CreateFinalPictureRequest, UpdateFinalPictureRequest, FinalPictureFilter, PagingModel>
    {
        public Task<FinalPictureResponse> CreateFinalPictureAsync(IFormFile file, Guid branchID, int photoTaken, Guid layoutID, string? discountCode, Guid? accountID);
    }
}
