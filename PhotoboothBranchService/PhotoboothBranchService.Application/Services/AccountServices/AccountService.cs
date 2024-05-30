using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IFirebaseService _firebaseService;

        public AccountService(IAccountRepository accountRepository,
            IRoleRepository roleRepository, IMapper mapper, IPasswordHasher passwordHasher,
            IJwtService jwtService, IFirebaseService firebaseService)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _firebaseService = firebaseService;
        }

        //khong co sai
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
                    if (accounts.RoleID == userRole.RoleID)
                    {
                        throw new Exception("Admin account cannot be delete!");
                    }
                    await _firebaseService.DeleteUserAsync(accounts.Email);
                    await _accountRepository.RemoveAsync(accounts);
                }
                throw new NotFoundException("Account", id, "Account ID not found");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the account: " + ex.Message);
            }
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAsync()
        {
            try
            {
                var accounts = await _accountRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<AccountResponse>>(accounts.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the account: " + ex.Message);
            }
        }

        public async Task<AccountResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var account = (await _accountRepository.GetAsync(a => a.AccountID == id)).FirstOrDefault();
                if (account == null)
                {
                    throw new NotFoundException("Account", id, "Account ID not found");
                }
                return _mapper.Map<AccountResponse>(account);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the account: " + ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, UpdateAccountRequestModel updateModel)
        {
            try
            {
                var account = (await _accountRepository.GetAsync(a => a.AccountID == id)).FirstOrDefault();
                if (account == null)
                {
                    throw new NotFoundException("Account", id, "Account ID not found");
                }

                var updateAccount = _mapper.Map(updateModel, account);
                updateAccount.SetPassword(updateModel.Password, _passwordHasher);

                await _firebaseService.UpdatePasswordOnFirebase(account.Email, updateModel.Password);
                await _accountRepository.UpdateAsync(updateAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update the account: " + ex.Message);
            }
        }

        public async Task<IEnumerable<AccountResponse>> GetAllPagingAsync(AccountFilter filter, PagingModel paging)
        {
            try
            {
                var cameras = (await _accountRepository.GetAllAsync()).ToList().AutoFilter(filter);
                var listAccountresponse = _mapper.Map<IEnumerable<AccountResponse>>(cameras);
                listAccountresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
                return listAccountresponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the account: " + ex.Message);
            }
        }

        public async Task<LoginResponeModel> Login(LoginRequestModel request)
        {
            try
            {
                var loginViewModel = await _jwtService.GetForCredentialsAsync(request.Email, request.Password);
                if (loginViewModel != null)
                {
                    return loginViewModel;
                }
                throw new BadRequestException("Login fail!!!");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while login the account: " + ex.Message);
            }
        }

        public async Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request)
        {
            try
            {
                var loginViewModel = await _jwtService.RefreshToken(request.RefreshToken);
                if (loginViewModel != null)
                {
                    return loginViewModel;
                }
                throw new BadRequestException("Refresh token fail!!!");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while refeshing the account: " + ex.Message);
            }
        }

        public async Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, UserRole role)
        {
            try
            {
                var userRole = (await _roleRepository.GetAsync(r => r.RoleName.Trim() == role.ToString().Trim())).FirstOrDefault();

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
                        newAccount.AccountFBID = uid;
                        newAccount.SetPassword(request.Password, _passwordHasher);
                        newAccount.RoleID = userRole.RoleID;
                        newAccount.Status = AccountStatus.Active;

                        var result = await _accountRepository.CreateAccount(newAccount);

                        var accountRespone = _mapper.Map<AccountRegisterResponse>(result);
                        accountRespone.RoleName = userRole.RoleName;

                        return accountRespone;
                    }
                    throw new BadRequestException("Register fail!!!");
                }
                throw new NotFoundException("Account", role, "User role does not exist in the system.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while register the account: " + ex.Message);
            }
        }

        public async Task<AccountResponse> GetByEmail(string email)
        {
            try
            {
                var account = (await _accountRepository.GetAsync(a => a.Email.Equals(email))).FirstOrDefault();
                if (account == null)
                {

                }
                return _mapper.Map<AccountResponse>(account);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the account: " + ex.Message);
            }
        }
    }
}
