using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs.RequestModels.Account;
using PhotoboothBranchService.Application.DTOs.RequestModels.Authentication;
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
    public async Task<ActionResult<CreateAccountRequestModel>> Login([FromBody] LoginRequestModel loginDTO)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountService.Login(loginDTO);
            if (account == null)
                return NotFound("Invalid email or password.");
            return Ok(account);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during login: {ex.Message}");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateAccountRequestModel request)
    {

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Register(request, "User");
            if (result != null)
                return Ok(result);
            return BadRequest("Registration failed. Please try again.");

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during login: {ex.Message}");
        };
    }


}

