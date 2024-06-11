using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PhotoSessionServices
{
    public interface IPhotoSessionService : IService<PhotoSessionResponse,CreatePhotoSessionRequest,UpdatePhotoSessionRequest,PhotoSessionFilter,PagingModel>
    {
    }
}
