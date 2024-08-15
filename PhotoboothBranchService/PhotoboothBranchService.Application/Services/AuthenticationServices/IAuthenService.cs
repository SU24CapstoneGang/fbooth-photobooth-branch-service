using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.AuthenticationServices
{
    public interface IAuthenService
    {
        Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, AccountRole userRole);
        Task<LoginResponeModel> Login(LoginRequestModel request, AccountRole role);
        Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
        Task<string> ResetPassword(string email);
        Task ForgetPassword(string username);
    }
}
