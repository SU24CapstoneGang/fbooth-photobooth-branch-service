using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers;

public class AccountController : ControllerBaseApi
{
    private readonly IAccountService _accountService;
    private readonly IFirebaseService _firebaseService;

    public AccountController(IAccountService accountService, IFirebaseService firebaseService)
    {
        _accountService = accountService;
        _firebaseService = firebaseService;
    }

    // Read all
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAllAccount()
    {
        try
        {
            var account = await _accountService.GetAllAsync();
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving accounts: {ex.Message}");
        }
    }
    // Read all with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetPagingAccounts(
        [FromQuery] AccountFilter accountFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var account = await _accountService.GetAllPagingAsync(accountFilter, pagingModel);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving accounts: {ex.Message}");
        }
    }
    // Read by name
    [HttpGet("email/{email}")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountByEmail(string email)
    {
        try
        {
            var account = await _accountService.GetByEmail(email);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving account by name: {ex.Message}");
        }
    }
    [HttpGet("phone-number/{phone}")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountByPhoneNumber(string phone)
    {
        try
        {
            var account = await _accountService.GetByPhoneNumber(phone);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving account by name: {ex.Message}");
        }
    }

    // Read by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponse>> GetAccountById(Guid id)
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

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAccount(Guid id, [FromQuery] UpdateAccountRequestModel updateAccountRequest)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _accountService.UpdateAsync(id, updateAccountRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the account: {ex.Message}");
        }
    }

    // Delete
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




    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestModel request)
    {
        var token = await _accountService.Login(request);
        if (token != null)
            return Ok(token);
        return BadRequest("Login fail!!!");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateAccountRequestModel request, AccountRole userRole)
    {

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Register(request, userRole);
            if (result != null)
                return Ok(result);
            return BadRequest("Registration failed. Please try again.");

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during login: {ex.Message}");
        };
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
    {
        var token = await _accountService.RefreshToken(request);
        if (token != null)
            return Ok(token);
        return BadRequest("Login fail!!!");
    }

    [HttpGet("profile/reset-password-link")]
    public async Task<IActionResult> ResetPassword()
    {
        var email = Request.HttpContext.Items["Email"]?.ToString();
        var result = await _accountService.ResetPassword(email);
        if (result != null)
            return Ok(result);
        return BadRequest();
    }

    [HttpGet("firebase-users")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAllFirebaseUsers()
    {
        try
        {
            var users = await _firebaseService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving Firebase users: {ex.Message}");
        }
    }

    // Delete
    //[Authorization("ADMIN")]
    [HttpDelete("delete-firebase/{firebaseEmail}")]
    public async Task<ActionResult> DeleteFireBase(string firebaseEmail)
    {
        try
        {
            await _firebaseService.DeleteUserAsync(firebaseEmail);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the account: {ex.Message}");
        }
    }
}

