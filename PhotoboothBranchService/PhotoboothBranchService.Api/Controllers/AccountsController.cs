using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsRepository _accountsRepository;

    public AccountsController(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Accounts>>> GetAllAccounts(CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetAll(cancellationToken);
        return Ok(accounts);
    }

    [HttpGet("{status}")]
    public async Task<ActionResult<IEnumerable<Accounts>>> GetAccountsByStatus(AccountStatus status, CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetAll(status, cancellationToken);
        return Ok(accounts);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<IEnumerable<Accounts>>> GetAccountsByEmail(string email, CancellationToken cancellationToken)
    {
        var accounts = await _accountsRepository.GetListByEmail(email, cancellationToken);
        return Ok(accounts);
    }

    [HttpGet("login")]
    public async Task<ActionResult<Accounts>> Login([FromQuery] string email, [FromQuery] string password, CancellationToken cancellationToken)
    {
        var account = await _accountsRepository.Login(email, password, cancellationToken);
        if (account == null)
        {
            return NotFound("Invalid email or password.");
        }
        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount(Accounts account, CancellationToken cancellationToken)
    {
        await _accountsRepository.AddAsync(account, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Accounts>> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        var account = await _accountsRepository.GetByIdAsync(id, cancellationToken);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAccount(Guid id, Accounts account, CancellationToken cancellationToken)
    {
        if (id != account.Id)
        {
            return BadRequest("Invalid ID.");
        }

        await _accountsRepository.UpdateAsync(account, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
    {
        var account = await _accountsRepository.GetByIdAsync(id, cancellationToken);
        if (account == null)
        {
            return NotFound();
        }

        await _accountsRepository.RemoveAsync(account, cancellationToken);
        return NoContent();
    }
}

