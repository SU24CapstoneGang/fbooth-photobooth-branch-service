using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.Services.EmailServices;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Application.Services.JwtServices;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.AuthenticationServices
{
    public class AuthenService : IAuthenService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IFirebaseService _firebaseService;
        private readonly IBranchRepository _branchRepository;
        private readonly IEmailService _emailService;

        public AuthenService(IAccountRepository accountRepository, IMapper mapper, IPasswordHasher passwordHasher, IJwtService jwtService, IFirebaseService firebaseService, IBranchRepository branchRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _firebaseService = firebaseService;
            _branchRepository = branchRepository;
            _emailService = emailService;
        }
        public async Task ForgetPassword(string username)
        {
            var usernameType = TypeHelper.DetectAndFormatInput(username);
            Account? account;
            if (usernameType.Item1 == UserNameInputType.Email)
            {
                account = (await _accountRepository.GetAsync(a => a.Email == username)).FirstOrDefault();
            }
            else if (usernameType.Item1 == UserNameInputType.PhoneNumber)
            {
                account = (await _accountRepository.GetAsync(a => a.PhoneNumber == usernameType.Item2)).FirstOrDefault();
            }
            else
            {
                throw new BadRequestException("Invalid username");
            }
            if (account == null)
            {
                throw new NotFoundException("Account not found");
            }
            var link = await this.ResetPassword(account.Email);
            if (!link.IsNullOrEmpty())
            {
                await _emailService.SendResetPasswordEmail(account.Email, link, $"{account.FirstName} {account.LastName}");
            }
            else
            {
                throw new Exception("An exception happend in system");
            }
        }
        public async Task<LoginResponeModel> Login(LoginRequestModel request, AccountRole role)
        {
            var usernameType = TypeHelper.DetectAndFormatInput(request.Username);
            Account? account;
            if (usernameType.Item1 == UserNameInputType.Email)
            {
                account = (await _accountRepository.GetAsync(a => a.Email == request.Username)).FirstOrDefault();
            }
            else if (usernameType.Item1 == UserNameInputType.PhoneNumber)
            {
                account = (await _accountRepository.GetAsync(a => a.PhoneNumber == usernameType.Item2)).FirstOrDefault();
            }
            else
            {
                throw new BadRequestException("Invalid username");
            }
            if (account == null)
            {
                throw new NotFoundException("Email or phone not found");
            }
            if (account.Role == role)
            {
                var loginViewModel = await _jwtService.GetForCredentialsAsync(account.Email, request.Password);
                if (loginViewModel != null)
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
            }
            throw new BadRequestException("Login fail!!!");
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
                            throw new BadRequestException("Email is already in use. Please choose a different email.");
                        }
                        if (!await _accountRepository.IsPhoneNumberUnique(request.PhoneNumber))
                        {
                            throw new BadRequestException("Phone number is already in use. Please choose a different Phone number.");
                        }

                        var isPhoneExisted = (await _accountRepository.GetAsync(pn => pn.PhoneNumber.Equals(request.PhoneNumber))).FirstOrDefault();
                        if (isPhoneExisted != null)
                        {
                            throw new BadRequestException("PhoneNumber is already in use. Please choose a different PhoneNumber.");
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
            catch (BadRequestException ex)
            {
                await _firebaseService.DeleteUserAsync(request.Email);
                throw new BadRequestException("An error occurred while register the account: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                await _firebaseService.DeleteUserAsync(request.Email);
                throw new NotFoundException("An error occurred while register the account: " + ex.Message);
            }
            catch (Exception ex)
            {
                await _firebaseService.DeleteUserAsync(request.Email);
                throw new Exception("An error occurred while register the account: " + ex.Message);
            }
        }
        public async Task<string> ResetPassword(string email)
        {
            var link = await _firebaseService.GetResetPasswordLink(email);
            return link;
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
    }
}
