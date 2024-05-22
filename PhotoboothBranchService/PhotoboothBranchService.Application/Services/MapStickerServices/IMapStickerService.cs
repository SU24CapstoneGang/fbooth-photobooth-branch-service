using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MapSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.MapStickerServices
{
    public interface IMapStickerService : IService<MapStickerResponse,CreateMapStickerRequest,UpdateMapStickerRequest,MapStickerFilter,PagingModel>
    {
    }
}
