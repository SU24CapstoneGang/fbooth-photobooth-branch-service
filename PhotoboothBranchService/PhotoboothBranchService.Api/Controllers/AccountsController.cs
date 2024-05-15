using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers;

public class AccountsController : ControllerBaseApi
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult> CreateAccount(AccountDTO accountDTO)
    {
        try
        {
            var createdAccount = await _accountService.CreateAsync(accountDTO);
            return Ok(createdAccount);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the account: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAccounts()
    {
        try
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving accounts: {ex.Message}");
        }
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccountsByStatus(AccountStatus status)
    {
        try
        {
            var accounts = await _accountService.GetAll(status);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving accounts by status: {ex.Message}");
        }
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccountsByEmail(string email)
    {
        try
        {
            var accounts = await _accountService.GetListByEmail(email);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving accounts by email: {ex.Message}");
        }
    }

    [HttpGet("login")]
    public async Task<ActionResult<AccountDTO>> Login([FromQuery] string email, [FromQuery] string password)
    {
        try
        {
            var account = await _accountService.LoginWithPassword(email, password);
            if (account == null)
            {
                return NotFound("Invalid email or password.");
            }
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during login: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDTO>> GetAccountById(Guid id)
    {
        try
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the account by ID: {ex.Message}");
        }
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAccount(Guid id, AccountDTO accountDTO)
    {
        try
        {
            await _accountService.UpdateAsync(id, accountDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the account: {ex.Message}");
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(Guid id)
    {
        try
        {
            await _accountService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the account: {ex.Message}");
        }
    }
}
