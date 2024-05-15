using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Response;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService
    {
        Task<AuthenticationResult> Register(AccountDTO accountDTO);
        Task<AuthenticationResult> Login(LoginDTO loginDTO);
    }
}
