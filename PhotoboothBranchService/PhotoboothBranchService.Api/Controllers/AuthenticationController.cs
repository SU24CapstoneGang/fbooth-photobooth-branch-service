using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.Common.Enum;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.AuthenticationServices;
using PhotoboothBranchService.Application.Services.FirebaseServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : ControllerBaseApi
    {
        private readonly IAuthenService _authenticationService;
        private readonly IFirebaseService _firebaseService;

        public AuthenticationController(IAuthenService authenticationService, IFirebaseService firebaseService)
        {
            _authenticationService = authenticationService;
            _firebaseService = firebaseService;
        }

        [AllowAnonymous]
        [HttpPost("admin")]
        public async Task<IActionResult> AdminLogin(LoginRequestModel request)
        {
            var token = await _authenticationService.Login(request, AccountRole.Admin);
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("staff")]
        public async Task<IActionResult> StaffLogin(LoginRequestModel request)
        {
            var token = await _authenticationService.Login(request, AccountRole.Staff);
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("customer")]
        public async Task<IActionResult> CustomerLogin(LoginRequestModel request)
        {
            var token = await _authenticationService.Login(request, AccountRole.Customer);
            return Ok(token);
        }

        [HttpGet("forget-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string username)
        {
            await _authenticationService.ForgetPassword(username);
            return Ok();
        }

        [HttpGet("profile/reset-password-link")]
        [Authorize]
        public async Task<IActionResult> ResetPassword()
        {
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var result = await _authenticationService.ResetPassword(email);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CustomerRegister([FromBody] CreateAccountRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationService.Register(request, AccountRole.Customer);
            if (result != null)
                return Ok(result);
            return BadRequest("Registration failed. Please try again.");

        }

        [Authorization("ADMIN")]
        [HttpPost("admin/register")]
        public async Task<IActionResult> AdminRegister([FromBody] CreateAccountRequestModel request, AccountRoleForInput userRole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationService.Register(request, (AccountRole)userRole);
            if (result != null)
                return Ok(result);
            return BadRequest("Registration failed. Please try again.");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
        {
            var token = await _authenticationService.RefreshToken(request);
            if (token != null)
                return Ok(token);
            return BadRequest("Login fail!!!");
        }
    }
}
