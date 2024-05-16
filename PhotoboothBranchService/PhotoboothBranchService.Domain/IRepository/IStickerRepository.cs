using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface IStickerRepository
{
    Task<IEnumerable<Sticker>> GetAll();
    Task<IEnumerable<Sticker>> GetByName(string name);
    Task<Guid> AddAsync(Sticker sticker);
    Task<Sticker?> GetByIdAsync(Guid stickerId);
    Task RemoveAsync(Sticker sticker);
    Task UpdateAsync(Sticker sticker);
}
