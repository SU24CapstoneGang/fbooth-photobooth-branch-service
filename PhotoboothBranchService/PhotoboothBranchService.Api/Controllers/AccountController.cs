using AutoMapper;
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
    private readonly IMapper _mapper;
    public AccountController(IAccountService accountService, IFirebaseService firebaseService, IMapper mapper)
    {
        _accountService = accountService;
        _firebaseService = firebaseService;
        _mapper = mapper;
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
        return Ok(_mapper.Map<AccountResponse>(account));

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
    [Authorization("ADMIN")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(Guid id)
    {
        await _accountService.DeleteAsync(id);
        return Ok();
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

