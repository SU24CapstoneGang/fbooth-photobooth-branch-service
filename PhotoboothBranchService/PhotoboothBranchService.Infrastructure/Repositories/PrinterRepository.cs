using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class PrinterRepository : IPrinterRepository
{
    private readonly AppDbContext _dbContext;

    public PrinterRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(Printer printer, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(printer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return printer.PrinterID;
    }

    //Read
    public async Task<IEnumerable<Printer>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Printer>> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.Where(p => p.ModelName.Contains(name)).ToListAsync(cancellationToken);
    }

    public async Task<Printer?> GetByIdAsync(Guid printerId, CancellationToken cancellationToken)
    {
        return await _dbContext.Printers.FindAsync(printerId, cancellationToken);
    }

    //Update
    public async Task UpdateAsync(Printer printer, CancellationToken cancellationToken)
    {
        _dbContext.Update(printer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    //Delete
    public async Task RemoveAsync(Printer printer, CancellationToken cancellationToken)
    {
        _dbContext.Printers.Remove(printer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
