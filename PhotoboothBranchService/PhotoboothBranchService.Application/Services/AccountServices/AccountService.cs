using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Camera;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Data;
using System.Security.Principal;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IFirebaseService _firebaseService;

        public AccountService(IAccountRepository accountRepository,  IJwtTokenGenerator jwtTokenGenerator, 
            IRoleRepository roleRepository, IMapper mapper, IPasswordHasher passwordHasher,
            IJwtService jwtService, IFirebaseService firebaseService)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _firebaseService = firebaseService;
        }

        public async Task<Guid> CreateAsync(CreateAccountRequestModel createModel)
        {
            Account account = _mapper.Map<Account>(createModel);
            return await _accountRepository.AddAsync(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var accounts = (await _accountRepository.GetAsync(a => a.AccountID == id)).FirstOrDefault();
                if (accounts != null)
                {
                    var userRole = (await _roleRepository.GetAsync(r => r.RoleName == UserRole.Admin.ToString())).FirstOrDefault();
                    if (userRole != null)
                    {
                        throw new Exception("Admin account cannit be delete!");
                    }
                    await _accountRepository.RemoveAsync(accounts);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AccountRespone>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountRespone>>(accounts.ToList());
        }

        public async Task<AccountRespone> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetAsync(a => a.AccountID == id);
            return _mapper.Map<AccountRespone>(account);
        }

        public async Task UpdateAsync(Guid id, UpdateAccountRequestModel updateModel)
        {
            var account = (await _accountRepository.GetAsync(a => a.AccountID == id)).FirstOrDefault();
            if (account == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            var updateCamera = _mapper.Map(updateModel, account);
            await _accountRepository.UpdateAsync(updateCamera);
        }

        public async Task<IEnumerable<AccountRespone>> GetAllPagingAsync(AccountFilter filter, PagingModel paging)
        {
            var cameras = (await _accountRepository.GetAllAsync()).AutoPaging(paging.PageSize, paging.PageIndex);
            var listAccountresponse = _mapper.Map<IEnumerable<AccountRespone>>(cameras.ToList());
            listAccountresponse.AutoFilter(filter);
            return listAccountresponse;
        }

        public async Task<LoginResponeModel> Login(LoginRequestModel request)
        {
            var loginViewModel = await _jwtService.GetForCredentialsAsync(request.Email, request.Password);
            if (loginViewModel != null)
            {
                return loginViewModel;
            }
            throw new BadRequestException("Login fail!!!");
        }

        public async Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request)
        {
            var loginViewModel = await _jwtService.RefreshToken(request.RefreshToken);
            if (loginViewModel != null)
            {
                return loginViewModel;
            }
            throw new BadRequestException("Refresh token fail!!!");
        }

        public async Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, UserRole role)
        {
            var userRole = (await _roleRepository.GetAsync(r => r.RoleName == role.ToString())).FirstOrDefault();

            //validation in db
            if (userRole != null)
            {
                var uid = await _firebaseService.RegisterAsync(request.Email, request.Password);
                if (uid != null)
                {
                    if (!await _accountRepository.IsEmailUnique(request.Email))
                    {
                        throw new Exception("Email is already in use. Please choose a different email.");
                    }

                    var newAccount = _mapper.Map<Account>(request);
                    newAccount.SetPassword(request.Password, _passwordHasher);
                    newAccount.RoleID = userRole.RoleID;
                    newAccount.Status = AccountStatus.Active;   

                    var result = await _accountRepository.CreateAccount(newAccount);
                    var accountRespone = _mapper.Map<AccountRegisterResponse>(result);

                    var loginViewModel = await _jwtService.GetForCredentialsAsync(request.Email, request.Password);
                    accountRespone.TokenId = loginViewModel.TokenId;
                    accountRespone.RefreshToken = loginViewModel.RefreshToken;

                    return accountRespone;
                }
                throw new BadRequestException("Register fail!!!");
            }
            throw new Exception("User role does not exist in the system.");
        }

        public async Task<IEnumerable<AccountRespone>> GetByEmail(string email)
        {
            var account = (await _accountRepository.GetAsync(a => a.Email.Equals(email)));
            return _mapper.Map<IEnumerable<AccountRespone>>(account.ToList());
        }
    } 
}
