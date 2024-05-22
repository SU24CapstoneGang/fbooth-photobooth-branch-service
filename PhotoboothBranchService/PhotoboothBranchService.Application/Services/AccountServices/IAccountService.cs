﻿using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService : IService<AccountResponse, CreateAccountRequestModel, UpdateAccountRequestModel, AccountFilter, PagingModel>
    {
        Task<IEnumerable<AccountResponse>> GetByEmail(string Email);
        Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, UserRole userRole);
        Task<LoginResponeModel> Login(LoginRequestModel request);
        Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
    }
}
