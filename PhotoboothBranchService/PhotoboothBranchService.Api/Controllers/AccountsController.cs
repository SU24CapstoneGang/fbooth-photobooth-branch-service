using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.Services.AccountServices;

namespace PhotoboothBranchService.Api.Controllers;

public class AccountsController : ControllerBaseApi
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
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
    public async Task<IActionResult> Register([FromBody] CreateAccountRequestModel request)
    {

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Register(request);
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
}

