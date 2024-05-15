using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class AccountDTO
{
    public Guid? AccountId { get; set; } = null;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public AccountStatus Status { get; set; } = default!;
    public Guid RoleID { get; set; }

    public AccountDTO()
    {
    }

    [JsonConstructor]
    public AccountDTO(string firstName, string lastName, string userName, string password, DateTime dateOfBirth, string address, string email, string phoneNumber, AccountStatus status, Guid roleID)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Password = password;
        DateOfBirth = dateOfBirth;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = status;
        RoleID = roleID;
    }
}
