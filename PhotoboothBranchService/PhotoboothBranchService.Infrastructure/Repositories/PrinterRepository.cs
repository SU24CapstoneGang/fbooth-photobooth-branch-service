using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

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
    public async Task<IQueryable<Printer>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Printers);
    }

    public async Task<IQueryable<Printer>> GetAsync(Expression<Func<Printer, bool>> predicate)
    {
        return await Task.FromResult(_dbContext.Printers.Where(predicate));
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
