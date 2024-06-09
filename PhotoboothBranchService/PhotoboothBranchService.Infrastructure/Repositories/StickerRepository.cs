using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
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
    public async Task<Guid> AddAsync(Sticker sticker)
    {
        await _dbContext.AddAsync(sticker);
        await _dbContext.SaveChangesAsync();
        return sticker.StickerID;
    }

    //Read
    public async Task<IQueryable<Sticker>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Stickers);
    }

    public async Task<IQueryable<Sticker>> GetAsync(Expression<Func<Sticker, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Stickers.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Sticker>().AsQueryable());
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
