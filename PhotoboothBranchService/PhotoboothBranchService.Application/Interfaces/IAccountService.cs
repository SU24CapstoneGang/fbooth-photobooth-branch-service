﻿using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Interfaces;

public interface IAccountService : IService<AccountDTO>
{
    Task<IEnumerable<AccountDTO>> GetAll(AccountStatus status);
    Task<IEnumerable<AccountDTO>> GetListByEmail(string email);
    Task<AccountDTO?> LoginWithPassword(string emailOrUsername, string password);
}
