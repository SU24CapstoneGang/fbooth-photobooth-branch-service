using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.StickerType;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.StickerTypeServices
{
    public interface IStickerTypeService : IServiceBase<StickerTypeResponse, StickerTypeFilter, PagingModel>
    {
        Task<StickerTypeResponse> CreateAsync(CreateStickerTypeRequest createModel, StatusUse status);
        Task UpdateAsync(Guid id, UpdateStickerTypeRequest updateModel, StatusUse? status);
    }
}
