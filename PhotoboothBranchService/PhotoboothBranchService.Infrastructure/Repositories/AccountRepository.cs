using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
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
    public async Task<Account> AddAsync(Account account)
    {
        var result = await _dbContext.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
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
        return await Task.FromResult(_dbContext.Accounts.AsQueryable());
    }

    public async Task<IQueryable<Account>> GetAsync(Expression<Func<Account, bool>> predicate = null,
        params Expression<Func<Account, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Accounts : _dbContext.Accounts.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Account>().AsQueryable());
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
}
