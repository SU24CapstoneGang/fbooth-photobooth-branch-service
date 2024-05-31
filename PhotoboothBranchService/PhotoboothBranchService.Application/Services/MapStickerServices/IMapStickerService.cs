using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MapSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.MapStickerServices
{
    public interface IMapStickerService : IService<MapStickerResponse, CreateMapStickerRequest, UpdateMapStickerRequest, MapStickerFilter, PagingModel>
    {
    }
}
