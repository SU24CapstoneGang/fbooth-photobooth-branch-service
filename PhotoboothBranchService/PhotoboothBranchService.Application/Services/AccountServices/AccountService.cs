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
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IFirebaseService _firebaseService;

        public AccountService(IAccountRepository accountRepository,
            IMapper mapper, IPasswordHasher passwordHasher,
            IJwtService jwtService, IFirebaseService firebaseService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _firebaseService = firebaseService;
        }

        public async Task<string> ResetPassword(string email)
        {
            var link = await _firebaseService.GetResetPasswordLink(email);
            return link;
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var accounts = (await _accountRepository.GetAsync(a => a.AccountID == id)).FirstOrDefault();
                if (accounts != null)
                {
                    if (accounts.Role == AccountRole.Admin)
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
                var account = (await _accountRepository.GetAllAsync()).ToList().AutoFilter(filter);
                var listAccountresponse = _mapper.Map<IEnumerable<AccountResponse>>(account);
                return listAccountresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
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
                var account = (await _accountRepository.GetAsync(a => a.Email == request.Email)).FirstOrDefault();

                var loginViewModel = await _jwtService.GetForCredentialsAsync(request.Email, request.Password);
                if (loginViewModel != null && account != null)
                {
                    if (!string.IsNullOrEmpty(account.ResetPasswordToken))
                    {
                        // Clear the password reset token and update the password in the database
                        account.ResetPasswordToken = null;
                        account.SetPassword(request.Password, _passwordHasher);
                        await _accountRepository.UpdateAsync(account);
                    }
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

        public async Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, AccountRole role)
        {
            try
            {
                //validation in db
                if (Enum.IsDefined(typeof(AccountRole), role))
                {
                    var uid = await _firebaseService.RegisterAsync(request.Email, request.Password);
                    if (uid != null)
                    {
                        if (!await _accountRepository.IsEmailUnique(request.Email))
                        {
                            throw new Exception("Email is already in use. Please choose a different email.");
                        }
                        if (!await _accountRepository.IsPhoneNumberUnique(request.PhoneNumber))
                        {
                            throw new Exception("Phone number is already in use. Please choose a different Phone number.");
                        }

                        var newAccount = _mapper.Map<Account>(request);
                        newAccount.AccountFBID = uid;
                        newAccount.SetPassword(request.Password, _passwordHasher);
                        newAccount.Role = role;
                        newAccount.Status = AccountStatus.Active;
                        newAccount.ResetPasswordToken = null;

                        var result = await _accountRepository.AddAsync(newAccount);

                        var accountRespone = _mapper.Map<AccountRegisterResponse>(result);
                        return accountRespone;
                    }
                    throw new BadRequestException("Register fail!!!");
                }
                throw new NotFoundException("Account", role, "User role does not exist in the system.");
            }
            catch (Exception ex)
            {
                await _firebaseService.DeleteUserAsync(request.Email);
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
                    throw new NotFoundException("Account", email, "Email does not exist in the system.");
                }
                return _mapper.Map<AccountResponse>(account);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the account: " + ex.Message);
            }
        }

        public async Task<AccountResponse> GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var account = (await _accountRepository.GetAsync(a => a.PhoneNumber.Equals(phoneNumber))).FirstOrDefault();
                if (account == null)
                {
                    throw new NotFoundException("Account", phoneNumber, "Phone number does not exist in the system.");
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
