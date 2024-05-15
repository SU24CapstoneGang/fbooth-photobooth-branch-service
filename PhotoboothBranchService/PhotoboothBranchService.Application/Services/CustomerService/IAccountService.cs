using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Response;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.CustomerService
{
    public interface IAccountService
    {
        Task<AuthenticationResult> Register(AccountDTO accountDTO);
        Task<AuthenticationResult> Login(LoginDTO loginDTO);
    }
}
