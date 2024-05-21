using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService : IService<AccountRespone, CreateAccountRequestModel, UpdateAccountRequestModel, AccountFilter, PagingModel>
    {
        Task<AccountRespone> Register(CreateAccountRequestModel request, UserRole userRole);
        Task<LoginResponeModel> Login(LoginRequestModel request);
        Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
    }
}
