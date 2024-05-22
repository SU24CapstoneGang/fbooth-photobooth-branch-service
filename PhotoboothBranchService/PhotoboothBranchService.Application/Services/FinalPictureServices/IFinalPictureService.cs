using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FinalPictureServices
{
    public interface IFinalPictureService : IService<FinalPictureResponse, CreateFinalPictureRequest, UpdateFinalPictureRequest, FinalPictureFilter, PagingModel>
    {
    }
}
