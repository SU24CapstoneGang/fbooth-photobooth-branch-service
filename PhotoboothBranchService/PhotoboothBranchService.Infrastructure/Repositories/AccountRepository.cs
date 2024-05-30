using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _dbContext;
    public AccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Account> CreateAccount(Account account)
    {
        var result = await _dbContext.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Guid> AddAsync(Account account)
    {
        await _dbContext.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return account.AccountID;
    }

    //Update
    public async Task UpdateAsync(Account account)
    {
        _dbContext.Update(account);
        await _dbContext.SaveChangesAsync();
    }

    //Delete
    public async Task RemoveAsync(Account account)
    {
        _dbContext.Remove(account);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        var existingAccounts = await _dbContext.Accounts.Where(c => c.Email.Equals(email)).ToListAsync();
        return existingAccounts.Count == 0;
    }

    //Read
    public async Task<IQueryable<Account>> GetAllAsync() 
    {
        return await Task.FromResult(_dbContext.Accounts.Include(a => a.Role).AsQueryable());
    }

    public async Task<IQueryable<Account>> GetAsync(Expression<Func<Account, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Accounts.Include(a => a.Role).Where(predicate).AsQueryable();
            if (!result.Any())
            {
                return await Task.FromResult(new List<Account>().AsQueryable());
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }


}
