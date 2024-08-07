using Microsoft.AspNetCore.Authorization;
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
        var account = await _accountService.GetAllAsync();
        return Ok(account);

    }
    // Read all with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetPagingAccounts(
        [FromQuery] AccountFilter accountFilter, [FromQuery] PagingModel pagingModel)
    {

        var account = await _accountService.GetAllPagingAsync(accountFilter, pagingModel);
        return Ok(account);

    }
    // Read by name
    [HttpGet("email/{email}")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountByEmail(string email)
    {

        var account = await _accountService.GetByEmail(email);
        return Ok(account);

    }
    [HttpGet("phone-number/{phone}")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountByPhoneNumber(string phone)
    {
        var account = await _accountService.GetByPhoneNumber(phone);
        return Ok(account);

    }

    // Read by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponse>> GetAccountById(Guid id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    // Update
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateAccount([FromBody] UpdateAccountRequestModel updateAccountRequest)
    {
        var email = Request.HttpContext.Items["Email"]?.ToString();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await _accountService.UpdateAsync(updateAccountRequest, email);
        return Ok();
    }

    // Delete
    [AdminAuthorization]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(Guid id)
    {
        await _accountService.DeleteAsync(id);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestModel request)
    {
        var token = await _accountService.Login(request);
        if (token != null)
            return Ok(token);
        return BadRequest("Login fail!!!");
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateAccountRequestModel request, AccountRole userRole)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.Register(request, userRole);
        if (result != null)
            return Ok(result);
        return BadRequest("Registration failed. Please try again.");

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
        var users = await _firebaseService.GetAllUsersAsync();
        return Ok(users);
    }

    // Delete
    [Authorization("ADMIN")]
    [HttpDelete("delete-firebase/{firebaseEmail}")]
    public async Task<ActionResult> DeleteFireBase(string firebaseEmail)
    {
        await _firebaseService.DeleteUserAsync(firebaseEmail);
        return Ok();
    }

    [Authorization("ADMIN")]
    [HttpPut("assign-branch")]
    public async Task<ActionResult> AssignBranchForStaff(AssignBranchForStaffRequest request)
    {
        await _accountService.AssignBranchForStaff(request);
        return Ok();
    }
}

