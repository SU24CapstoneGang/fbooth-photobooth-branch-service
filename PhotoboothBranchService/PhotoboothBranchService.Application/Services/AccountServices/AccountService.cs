using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Exceptions;
using PhotoboothBranchService.Application.Response;
using PhotoboothBranchService.Application.Services.AuthentiacationService;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
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
        private readonly IPasswordHasher _passwordHasher;

        public AccountService(IAccountRepository accountRepository,  IJwtTokenGenerator jwtTokenGenerator, IRoleRepository roleRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResult> Login(LoginDTO loginDTO)
        {
            var user = await _accountRepository.GetByEmail(loginDTO.Email);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password");
            } else if (user.VerifyPassword(loginDTO.Password, _passwordHasher))
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


        public async Task<AuthenticationResult> Register(AccountDTO accountDTO)
        {
            var userRole = await _roleRepository.GetByName("User");

            //validation in db
            if (userRole.Count() == 0)
            {
                throw new Exception("User role does not exist in the system.");
            }

            if (!await _accountRepository.IsEmailUnique(accountDTO.Email))
            {
                throw new Exception("Email is already in use. Please choose a different email.");
            }

            // Tạo một đối tượng Account từ AccountDTO
            Account newAccount = _mapper.Map<Account>(accountDTO);
            newAccount.SetPassword(accountDTO.Password, _passwordHasher);
            newAccount.RoleID = userRole.ElementAt(0).RoleID;
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
