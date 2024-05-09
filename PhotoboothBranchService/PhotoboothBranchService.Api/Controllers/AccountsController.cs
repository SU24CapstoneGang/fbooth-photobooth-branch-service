using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.DTO;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers
{
    public class AccountsController : ControllerBaseApi
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsController(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAllAccounts(CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountsRepository.GetAll(cancellationToken);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving accounts: {ex.Message}");
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAccountsByStatus(AccountStatus status, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountsRepository.GetAll(status, cancellationToken);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving accounts by status: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAccountsByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountsRepository.GetListByEmail(email, cancellationToken);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving accounts by email: {ex.Message}");
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<Accounts>> Login([FromQuery] string email, [FromQuery] string password, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountsRepository.Login(email, password, cancellationToken);
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

        [HttpPost]
        public async Task<ActionResult> CreateAccount(AccountsDTO accountDTO, CancellationToken cancellationToken)
        {
            try
            {
                Accounts account = new Accounts(Guid.NewGuid(), accountDTO.EmailAddress, accountDTO.PhoneNumber, 
                    accountDTO.Password, accountDTO.Role, accountDTO.Status);

                await _accountsRepository.AddAsync(account, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the account: {ex.Message}");
            }
        }

        /*   [HttpPost]
           public async Task<ActionResult> CreateAccount(Accounts account, CancellationToken cancellationToken)
           {
               try
               {
                   await _accountsRepository.AddAsync(account, cancellationToken);
                   return Ok();
               }
               catch (Exception ex)
               {
                   return StatusCode(500, $"An error occurred while creating the account: {ex.Message}");
               }
           }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetAccountById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountsRepository.GetByIdAsync(id, cancellationToken);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccount(Guid id, Accounts account, CancellationToken cancellationToken)
        {
            try
            {
                if (id != account.Id)
                {
                    return BadRequest("Invalid ID.");
                }

                await _accountsRepository.UpdateAsync(account, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the account: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountsRepository.GetByIdAsync(id, cancellationToken);
                if (account == null)
                {
                    return NotFound();
                }

                await _accountsRepository.RemoveAsync(account, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the account: {ex.Message}");
            }
        }
    }
}
