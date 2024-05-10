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
    public async Task<Guid> AddAsync(Accounts account, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(account, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return account.Id;
    }

    //Read
    public async Task<IEnumerable<Accounts>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts.ToListAsync();
    }

    public async Task<IEnumerable<Accounts>> GetAll(AccountStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts.Where(c => c.Status == status).ToListAsync();
    }

    public async Task<Accounts?> Login(string email, string password, CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.EmailAddress.Equals(email) && a.Password.Equals(password), cancellationToken);
    }

    public async Task<IEnumerable<Accounts>> GetListByEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts.Where(c => c.EmailAddress.Contains(email)).ToListAsync();
    }

    public async Task<Accounts?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts.FindAsync(accountId, cancellationToken);
    }

    //Update
    public async Task UpdateAsync(Accounts account, CancellationToken cancellationToken)
    {
        _dbContext.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    //Delete
    public async Task RemoveAsync(Accounts account, CancellationToken cancellationToken)
    {
        _dbContext.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
