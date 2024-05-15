using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class FrameRepository : IFrameRepository
{
    private readonly AppDbContext _dbContext;
    public FrameRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Frame frame)
    {
        await _dbContext.AddAsync(frame);
        await _dbContext.SaveChangesAsync();
        return frame.FrameID;
    }

    public async Task<IEnumerable<Frame>> GetAll()
    {
        return await _dbContext.Frames.ToListAsync();
    }

    public async Task<Frame?> GetByIdAsync(Guid frameId)
    {
        return await _dbContext.Frames.FindAsync(frameId);
    }

    public async Task<IEnumerable<Frame>> GetByName(string name)
    {
        return await _dbContext.Frames.Where(c => c.FrameName.Contains(name)).ToListAsync();
    }

    public async Task RemoveAsync(Frame frame)
    {
        _dbContext.Remove(frame);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Frame frame)
    {
        _dbContext.Update(frame);
        await _dbContext.SaveChangesAsync();
    }
}
