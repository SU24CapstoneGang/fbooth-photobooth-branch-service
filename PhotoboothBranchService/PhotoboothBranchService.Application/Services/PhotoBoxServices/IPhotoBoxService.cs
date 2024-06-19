using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PhotoBoxServices
{
    public interface IPhotoBoxService : IService<PhotoBoxResponse,CreatePhotoBoxRequest,UpdatePhotoBoxRequest,PhotoBoxFilter,PagingModel>
    {
    }
}
