using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public interface IStickerService : IService<StickerDTO>
{
    Task<IEnumerable<StickerDTO>> GetByName(string name);
}
