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
    public async Task<Guid> CreateAsync(AccountDTO entityDTO, CancellationToken cancellationToken)
    {
        Account account = _mapper.Map<Account>(entityDTO);
        account.AccountID= Guid.NewGuid();
        account.SetPassword(entityDTO.Password, _passwordHasher);
        return await _accountsRepository.AddAsync(account,cancellationToken);
    }

    //Delete
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Account? accounts = await _accountsRepository.GetByIdAsync(id, cancellationToken);
            if (accounts != null)
            {
                await _accountsRepository.RemoveAsync(accounts, cancellationToken);
            }
        } catch (Exception ex)
        {
            throw;
        }
        
    }

    //Read
    public async Task<IEnumerable<AccountDTO>> GetAll(AccountStatus status, CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetAll(status, cancellationToken);
        return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetAll(cancellationToken);
        return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
    }

    public async Task<AccountDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetByIdAsync(id,cancellationToken);
        return _mapper.Map<AccountDTO>(accounts);
    }

    public async Task<IEnumerable<AccountDTO>> GetListByEmail(string email, CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetListByEmail(email, cancellationToken);
        return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
    }
    //Login
    public async Task<AccountDTO?> LoginWithPassword(string emailOrUsername, string password, CancellationToken cancellationToken)
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
    public async Task UpdateAsync(Guid id, AccountDTO entityDTO, CancellationToken cancellationToken)
    {
        entityDTO.AccountId = id;
        Account accounts = _mapper.Map<Account>(entityDTO);
        await _accountsRepository.UpdateAsync(accounts,cancellationToken);
    }
}
