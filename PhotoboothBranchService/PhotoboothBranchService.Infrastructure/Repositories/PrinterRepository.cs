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
    public async Task<Guid> AddAsync(Printer printer)
    {
        await _dbContext.AddAsync(printer);
        await _dbContext.SaveChangesAsync();
        return printer.PrinterID;
    }

    //Read
    public async Task<IEnumerable<Printer>> GetAll( )
    {
        return await _dbContext.Printers.ToListAsync();
    }

    public async Task<IEnumerable<Printer>> GetByName(string name)
    {
        return await _dbContext.Printers.Where(p => p.ModelName.Contains(name)).ToListAsync();
    }

    public async Task<Printer?> GetByIdAsync(Guid printerId)
    {
        return await _dbContext.Printers.FindAsync(printerId);
    }

    //Update
    public async Task UpdateAsync(Printer printer)
    {
        _dbContext.Update(printer);
        await _dbContext.SaveChangesAsync();
    }

    //Delete
    public async Task RemoveAsync(Printer printer)
    {
        _dbContext.Printers.Remove(printer);
        await _dbContext.SaveChangesAsync();
    }
}
