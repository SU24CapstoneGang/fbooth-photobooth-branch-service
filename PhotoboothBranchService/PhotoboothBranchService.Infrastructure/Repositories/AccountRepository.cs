using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _dbContext;
    public AccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(Account account)
    {
        await _dbContext.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return account.AccountID;
    }

    //Read
    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _dbContext.Accounts.ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetAll(AccountStatus status)
    {
        return await _dbContext.Accounts.Where(c => c.Status == status).ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetListByEmail(string email)
    {
        return await _dbContext.Accounts.Where(c => c.Email.Contains(email)).ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid accountId)
    {
        return await _dbContext.Accounts.FindAsync(accountId);
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

    //login
    public async Task<Account?> GetByEmail(string email)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email.Equals(email));
    }

    public async Task<Account?> GetByUsername(string username)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName.Equals(username));
    }
}
