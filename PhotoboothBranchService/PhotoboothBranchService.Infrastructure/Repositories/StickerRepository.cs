using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class StickerRepository : IStickerRepository
{
    private readonly AppDbContext _dbContext;

    public StickerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Sticker sticker)
    {
        await _dbContext.AddAsync(sticker);
        await _dbContext.SaveChangesAsync();
        return sticker.StickerId;
    }

    public async Task<IEnumerable<Sticker>> GetAll()
    {
        return await _dbContext.Stickers.ToListAsync();
    }

    public async Task<Sticker?> GetByIdAsync(Guid stickerId)
    {
        return await _dbContext.Stickers.FindAsync(stickerId);
    }

    public async Task<IEnumerable<Sticker>> GetByName(string name)
    {
        return await _dbContext.Stickers.Where(c => c.StickerName.Contains(name)).ToListAsync();
    }

    public async Task RemoveAsync(Sticker sticker)
    {
        _dbContext.Remove(sticker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sticker sticker)
    {
        _dbContext.Update(sticker);
        await _dbContext.SaveChangesAsync();
    }
}
