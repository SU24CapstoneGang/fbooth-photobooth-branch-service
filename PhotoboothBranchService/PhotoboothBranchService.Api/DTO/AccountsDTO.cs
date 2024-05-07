using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.DTO;

public class AccountsDTO
{
    public string AccountId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public AccountRole Role { get; }
    public AccountStatus Status { get; }

   public AccountsDTO(string accountID, string emailAddress,string phoneNumber, AccountRole role, AccountStatus status)
   {
        AccountId = accountID;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Role = role;
        Status = status;
   }
    public AccountsDTO( string emailAddress, string phoneNumber, AccountRole role, AccountStatus status)
    {
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Role = role;
        Status = status;
    }
}
