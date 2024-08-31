using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.Services.BookingServices;
using PhotoboothBranchService.Application.Services.EmailServices;
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
        private readonly IFirebaseService _firebaseService;
        private readonly IBranchRepository _branchRepository;
        public AccountService(IAccountRepository accountRepository,
            IMapper mapper, IPasswordHasher passwordHasher, IFirebaseService firebaseService, IBranchRepository branchRepository)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _firebaseService = firebaseService;
            _branchRepository = branchRepository;
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
                var accounts = await _accountRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<AccountResponse>>(accounts.ToList());
        }

        public async Task<AccountResponse> GetByIdAsync(Guid id)
        {
                var account = (await _accountRepository.GetAsync(a => a.AccountID == id)).FirstOrDefault();
                if (account == null)
                {
                    throw new NotFoundException("Account", id, "Account ID not found");
                }
                return _mapper.Map<AccountResponse>(account);
        }

        public async Task UpdateAsync(UpdateAccountRequestModel updateModel, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ForbiddenAccessException("Can not update infomation of another account");
            }
            var account = (await _accountRepository.GetAsync(a => a.Email.Equals(email))).FirstOrDefault();
            if (account == null)
            {
                throw new NotFoundException("Account", email, "Account ID not found");
            }
            var updateAccount = _mapper.Map(updateModel, account);
            //updateAccount.SetPassword(updateModel.Password, _passwordHasher);

            //await _firebaseService.UpdatePasswordOnFirebase(account.Email, updateModel.Password);
            await _accountRepository.UpdateAsync(updateAccount);
        }

        public async Task<IEnumerable<AccountResponse>> GetAllPagingAsync(AccountFilter filter, PagingModel paging)
        {
                var account = (await _accountRepository.GetAllAsync()).ToList().AutoFilter(filter);
                var listAccountresponse = _mapper.Map<IEnumerable<AccountResponse>>(account);
                return listAccountresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        public async Task<Account> GetByEmail(string email)
        {
            var account = (await _accountRepository.GetAsync(a => a.Email.Equals(email))).FirstOrDefault();
            if (account == null)
            {
                throw new NotFoundException("Account", email, "Email does not exist in the system.");
            }
            return account;
        }

        public async Task<AccountResponse> GetByPhoneNumber(string phoneNumber)
        {
            var account = (await _accountRepository.GetAsync(a => a.PhoneNumber.Equals(phoneNumber))).FirstOrDefault();
            if (account == null)
            {
                throw new NotFoundException("Account", phoneNumber, "Phone number does not exist in the system.");
            }
            return _mapper.Map<AccountResponse>(account);

        }

        public async Task AssignBranchForStaff(AssignBranchForStaffRequest request)
        {
            var acc = (await _accountRepository.GetAsync(i => i.AccountID == request.StaffID)).FirstOrDefault();
            var branch = (await _branchRepository.GetAsync(i => i.BranchID == request.BranchID)).FirstOrDefault();
            if (acc == null) 
            {
                throw new NotFoundException("Not found account");
            }
            if (acc.Role != AccountRole.Staff)
            {
                throw new BadRequestException("Account is not staff");
            }
            if (branch == null)
            {
                throw new NotFoundException("Not found branch");
            }
            acc.BranchID = branch.BranchID;
            await _accountRepository.UpdateAsync(acc);
        }

        public async Task<Account> ValidateCustomerAsync(string? phoneNumber, string? email)
        {
            Account? accountByPhone = null;
            Account? accountByEmail = null;

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                accountByPhone = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(phoneNumber))).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(email))
            {
                accountByEmail = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).FirstOrDefault();
            }

            if (accountByPhone == null && accountByEmail == null)
            {
                throw new NotFoundException("Account not found");
            }

            // If both phone and email are provided but only one account is found, throw an error
            if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(email))
            {
                if (accountByPhone == null || accountByEmail == null)
                {
                    throw new BadRequestException("The provided phone number and email do not match the same account");
                }
            }

            // If both are not null but belong to different accounts, throw an error
            if (accountByPhone != null && accountByEmail != null && accountByPhone.AccountID != accountByEmail.AccountID)
            {
                throw new BadRequestException("The provided phone number and email correspond to different accounts");
            }

            // Get the found account (either by phone or email)
            Account account = accountByPhone ?? accountByEmail;

            // Validate the account's role and status
            if (account.Role != AccountRole.Customer)
            {
                throw new BadRequestException("Account is not Customer");
            }

            if (account.Status != AccountStatus.Active)
            {
                throw new BadRequestException("Account is not active to do this function");
            }

            return account;
        }
    }
}
