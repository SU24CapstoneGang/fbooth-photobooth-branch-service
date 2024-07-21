using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class StickerRepository : IStickerRepository
{
    private readonly AppDbContext _dbContext;

    public StickerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    //Create
    public async Task<Sticker> AddAsync(Sticker sticker)
    {
        var result = await _dbContext.AddAsync(sticker);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    //Read
    public async Task<IQueryable<Sticker>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Stickers);
    }

    public async Task<IQueryable<Sticker>> GetAsync(
        Expression<Func<Sticker, bool>> predicate = null,
        params Expression<Func<Sticker, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Stickers : _dbContext.Stickers.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<Sticker>().AsQueryable());
            }
            else
            {
                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        if (IncludeHelper.IsValidInclude(includeProperty))
                        {
                            result = result.Include(includeProperty);
                        }
                    }
                }
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //Delete
    public async Task RemoveAsync(Sticker sticker)
    {
        _dbContext.Remove(sticker);
        await _dbContext.SaveChangesAsync();
    }

    //Update
    public async Task UpdateAsync(Sticker sticker)
    {
        _dbContext.Update(sticker);
        await _dbContext.SaveChangesAsync();
    }
}
