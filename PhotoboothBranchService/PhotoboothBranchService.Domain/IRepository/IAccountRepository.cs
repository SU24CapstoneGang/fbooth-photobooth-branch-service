﻿using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.IRepository;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAll();
    Task<IEnumerable<Account>> GetAll(AccountStatus status);
    Task<IEnumerable<Account>> GetListByEmail(string email);
    Task<Account?> GetByEmail(string email);
    Task<Account> AddAsync(Account account);
    Task<Account> GetByIdAsync(Guid accountId);
    Task RemoveAsync(Account account);
    Task UpdateAsync(Account account);
    Task<bool> IsEmailUnique (string email);
}
