using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class FrameRepository : IFrameRepository
{
    private readonly AppDbContext _dbContext;
    public FrameRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //Create
    public async Task<Guid> AddAsync(Frame frame)
    {
        await _dbContext.AddAsync(frame);
        await _dbContext.SaveChangesAsync();
        return frame.FrameID;
    }

    //Read
    public async Task<IQueryable<Frame>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Frames);
    }

    public async Task<IQueryable<Frame>> GetAsync(
        Expression<Func<Frame, bool>> predicate = null,
        params Expression<Func<Frame, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Frames : _dbContext.Frames.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<Frame>().AsQueryable());
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
    public async Task RemoveAsync(Frame frame)
    {
        _dbContext.Remove(frame);
        await _dbContext.SaveChangesAsync();
    }

    //Update
    public async Task UpdateAsync(Frame frame)
    {
        frame.LastModified = DateTime.UtcNow;
        _dbContext.Update(frame);
        await _dbContext.SaveChangesAsync();
    }
}
