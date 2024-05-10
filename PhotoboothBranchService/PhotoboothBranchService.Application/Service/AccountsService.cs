using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Service;

public class AccountsService : IAccountService
{
    private readonly IAccountRepository _accountsRepository;
    private readonly IMapper _mapper;

    public AccountsService(IAccountRepository accountsRepository, IMapper mapper)
    {
        _accountsRepository = accountsRepository;
        _mapper = mapper;
    }

    public Task<Guid> CreateAsync(AccountDTO entity, CancellationToken cancellationToken)
    {
        Accounts account = new Accounts(Guid.NewGuid(), entity.EmailAddress, entity.PhoneNumber,
                    entity.Password, entity.Role, entity.Status, entity.PhotoBoothBrachId);
        return _accountsRepository.AddAsync(account,cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Accounts? accounts = await _accountsRepository.GetByIdAsync(id, cancellationToken);
            if (accounts != null)
            {
                await _accountsRepository.RemoveAsync(accounts, cancellationToken);
            }
        } catch (Exception ex)
        {
            throw;
        }
        
    }

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

    public async Task<AccountDTO?> Login(string email, string password, CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.Login(email,password, cancellationToken);
        return _mapper.Map<AccountDTO>(accounts);
    }

    public async Task UpdateAsync(Guid id, AccountDTO entity, CancellationToken cancellationToken)
    {
        entity.AccountId = id;
        Accounts accounts = _mapper.Map<Accounts>(entity);
        await _accountsRepository.UpdateAsync(accounts,cancellationToken);
    }
}
