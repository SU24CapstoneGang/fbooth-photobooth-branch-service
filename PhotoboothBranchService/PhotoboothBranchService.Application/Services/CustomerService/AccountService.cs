using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Exceptions;
using PhotoboothBranchService.Application.Response;
using PhotoboothBranchService.Application.Services.AuthentiacationService;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.CustomerService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AccountService(IAccountRepository accountRepository,  IJwtTokenGenerator jwtTokenGenerator)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthenticationResult> Login(LoginDTO loginDTO)
        {
            var user = await _accountRepository.Login(loginDTO.Email, loginDTO.Password);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for(int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                {
                    throw new UnauthorizedException("Invalid email or password");
                }
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


        public async Task<AuthenticationResult> Register(AccountDTO accountDTO)
        {

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
                PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes(accountDTO.Password)),
                PasswordHash = hmac.Key
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
            //var newAccount = _mapper.Map<AccountDTO, Account>(accountDTO);

            //// Generate salt and hash for password
            //using var hmac = new HMACSHA512();
            //newAccount.PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes(accountDTO.Password));
            //newAccount.PasswordHash = hmac.Key;

            //var accountId = await _accountRepository.AddAsync(newAccount);

            //var authResult = new AuthenticationResult
            //{
            //    Id = accountId,
            //    FirstName = newAccount.FirstName,
            //    LastName = newAccount.LastName,
            //    Email = newAccount.Email,
            //    Token = _jwtTokenGenerator.GenerateToken(newAccount)
            //};

            //return authResult;

        }
    }
}
