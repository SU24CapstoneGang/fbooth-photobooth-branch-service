using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class PrintersRepository : IPrintersRepository
{
    private readonly AppDbContext _dbContext;

    public PrintersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Printers>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Printers>> GetAll(ManufactureStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.Where(p => p.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Printers>> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.Where(p => p.ModelName.Contains(name)).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Printers printer, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(printer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Printers?> GetByIdAsync(Guid printerId, CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.FindAsync(printerId, cancellationToken);
    }

    public async Task RemoveAsync(Printers printer, CancellationToken cancellationToken)
    {
        _dbContext.Printers.Remove(printer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Printers printer, CancellationToken cancellationToken)
    {
        _dbContext.Update(printer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
