﻿using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTO;

public class AccountDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
