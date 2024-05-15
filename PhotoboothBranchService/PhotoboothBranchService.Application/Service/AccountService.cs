using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Service;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountsRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    public AccountService(IAccountRepository accountsRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _accountsRepository = accountsRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }


    //Create
    public async Task<Guid> CreateAsync(AccountDTO entityDTO)
    {
        Account account = _mapper.Map<Account>(entityDTO);
        account.AccountID= Guid.NewGuid();
        account.SetPassword(entityDTO.Password, _passwordHasher);
        return await _accountsRepository.AddAsync(account);
    }

    //Delete
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            Account? accounts = await _accountsRepository.GetByIdAsync(id);
            if (accounts != null)
            {
                await _accountsRepository.RemoveAsync(accounts);
            }
        } catch (Exception ex)
        {
            throw;
        }
        
    }

    //Read
    public async Task<IEnumerable<AccountDTO>> GetAll(AccountStatus status)
    {
        var accounts = await _accountsRepository.GetAll(status);
        return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAsync()
    {
        var accounts = await _accountsRepository.GetAll();
        return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
    }

    public async Task<AccountDTO> GetByIdAsync(Guid id)
    {
        var accounts = await _accountsRepository.GetByIdAsync(id);
        return _mapper.Map<AccountDTO>(accounts);
    }

    public async Task<IEnumerable<AccountDTO>> GetListByEmail(string email)
    {
        var accounts = await _accountsRepository.GetListByEmail(email);
        return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
    }
    //Login
    public async Task<AccountDTO?> LoginWithPassword(string emailOrUsername, string password)
    {
        var accounts = await _accountsRepository.GetByEmail(emailOrUsername);
        if (accounts == null)
        {
            accounts = await _accountsRepository.GetByUsername(emailOrUsername);
        }
        if (accounts == null)
        {
            return null;
        }
        var isPasswordValid = accounts.VerifyPassword(password, _passwordHasher);
        if (!isPasswordValid)
        {
            return null;
        }
        return _mapper.Map<AccountDTO>(accounts);
    }

    //Update
    public async Task UpdateAsync(Guid id, AccountDTO entityDTO )
    {
        entityDTO.AccountId = id;
        Account accounts = _mapper.Map<Account>(entityDTO);
        await _accountsRepository.UpdateAsync(accounts);
    }
}
