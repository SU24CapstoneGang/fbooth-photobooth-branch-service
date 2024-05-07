using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly AppDbContext _dbContext;
    public AccountsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Accounts>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts.ToListAsync();
    }
    public async Task AddAsync(Accounts account, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(account, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
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

    public async Task RemoveAsync(Accounts account, CancellationToken cancellationToken)
    {
        _dbContext.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Accounts account, CancellationToken cancellationToken)
    {
        _dbContext.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}
