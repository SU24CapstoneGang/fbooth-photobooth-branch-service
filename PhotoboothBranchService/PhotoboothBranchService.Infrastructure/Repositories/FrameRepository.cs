﻿using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq;
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

    public async Task<IQueryable<Frame>> GetAsync(Expression<Func<Frame, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Frames.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Frame>().AsQueryable());
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
        _dbContext.Update(frame);
        await _dbContext.SaveChangesAsync();
    }
}
