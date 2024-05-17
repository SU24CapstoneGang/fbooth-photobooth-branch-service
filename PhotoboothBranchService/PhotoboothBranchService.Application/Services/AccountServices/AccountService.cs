using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels.Account;
using PhotoboothBranchService.Application.DTOs.RequestModels.Authentication;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Authentication;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository,  IJwtTokenGenerator jwtTokenGenerator, IRoleRepository roleRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<AuthenticationResult> Login(LoginRequestModel loginDTO)
        {
            var user = await _accountRepository.Login(loginDTO.Email, loginDTO.Password);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password");
            }

            return new AuthenticationResult
            {
                Id = user.AccountID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = _jwtTokenGenerator.GenerateToken(user),
            };
        }


        public async Task<AuthenticationResult> Register(CreateAccountRequestModel accountDTO)
        {
            var userRole = await _roleRepository.GetByName("User");

            if (userRole == null)
            {
                throw new Exception("User role does not exist in the system.");
            }

            if (!await _accountRepository.IsEmailUnique(accountDTO.Email))
            {
                throw new Exception("Email is already in use. Please choose a different email.");
            }
            using var hmac = new HMACSHA512();

            // Tạo một đối tượng Account từ AccountDTO
            var newAccount = new Account
            {
                FirstName = accountDTO.FirstName,
                LastName = accountDTO.LastName,
                Email = accountDTO.Email,
                Address = accountDTO.Address,
                DateOfBirth = accountDTO.DateOfBirth,
                PhoneNumber = accountDTO.PhoneNumber,
                Status = Domain.Enum.AccountStatus.Active,
                RoleID = userRole.Single().RoleID,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(accountDTO.Password))
            };

            var accountId = await _accountRepository.AddAsync(newAccount);
            var authResult = new AuthenticationResult
            {
                Id = accountId,
                FirstName = newAccount.FirstName,
                LastName = newAccount.LastName,
                Email = newAccount.Email,
                Token = _jwtTokenGenerator.GenerateToken(newAccount)
            };

            return authResult;
        }
    }
}
