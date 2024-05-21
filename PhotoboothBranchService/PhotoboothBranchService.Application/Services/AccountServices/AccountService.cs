using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
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

        public Task<Guid> CreateAsync(CreateAccountRequestModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountRespone>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AccountRespone> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
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

        public async Task<AccountRespone> Register(CreateAccountRequestModel request, UserRole role)
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

                    var result = await _accountRepository.AddAsync(newAccount);
                    var accountRespone = _mapper.Map<AccountRespone>(result);
                    return accountRespone;
                }
                throw new BadRequestException("Register fail!!!");
            }
            throw new Exception("User role does not exist in the system.");
        }

        public Task UpdateAsync(Guid id, UpdateAccountRequestModel updateModel)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<AccountRespone>> IService<AccountRespone, CreateAccountRequestModel, UpdateAccountRequestModel, AccountFilter, PagingModel>.GetAllPagingAsync(AccountFilter filter, PagingModel paging)
        {
            throw new NotImplementedException();
        }
    }
}
