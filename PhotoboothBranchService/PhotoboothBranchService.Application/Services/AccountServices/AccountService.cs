using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs.RequestModels.Account;
using PhotoboothBranchService.Application.DTOs.RequestModels.Authentication;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Authentication;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public AccountService(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator, IRoleRepository roleRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        //Login using Email and password
        public async Task<AuthenticationResult> Login(LoginRequestModel loginRequestModel)
        {
            var users = await _accountRepository.GetAsync(l => l.Email.Equals(loginRequestModel));
            var user = users.FirstOrDefault();
            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password");
            }
            else if (user.VerifyPassword(loginRequestModel.Password, _passwordHasher))
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

        //Register for role input
        public async Task<AuthenticationResult> Register(CreateAccountRequestModel createAccountRequestModel, String roleName)
        {
            var userRoles = await _roleRepository.GetAsync(r => r.RoleName.Equals(roleName));
            var userRole = userRoles.FirstOrDefault();
            //validation in db
            if (userRole == null)
            {
                throw new Exception("User role does not exist in the system.");
            }

            if (!await _accountRepository.IsEmailUnique(createAccountRequestModel.Email))
            {
                throw new Exception("Email is already in use. Please choose a different email.");
            }

            // Create User entity
            Account newAccount = _mapper.Map<Account>(createAccountRequestModel);
            newAccount.SetPassword(createAccountRequestModel.Password, _passwordHasher);
            newAccount.RoleID = userRole.RoleID;

            //Add user
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
