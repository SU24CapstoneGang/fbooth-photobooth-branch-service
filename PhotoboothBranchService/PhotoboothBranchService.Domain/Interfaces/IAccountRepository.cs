using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Accounts>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<Accounts>> GetAll(AccountStatus status, CancellationToken cancellationToken);
    Task<IEnumerable<Accounts>> GetListByEmail(string email, CancellationToken cancellationToken);
    Task<Accounts?> Login(string email, string password, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Accounts account, CancellationToken cancellationToken);
    Task<Accounts?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken);
    Task RemoveAsync(Accounts account, CancellationToken cancellationToken);
    Task UpdateAsync(Accounts account, CancellationToken cancellationToken);
}
