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
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Guid? PhotoBoothBrachId { get; set; }
    public AccountRole Role { get; }
    public AccountStatus Status { get; }
    
    //contrustor respone
   public AccountDTO(Guid accountId, string emailAddress,string phoneNumber, AccountRole role, AccountStatus status, Guid? photoBoothBrachId)
   {
        AccountId = accountId;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Role = role;
        Status = status;
        PhotoBoothBrachId = photoBoothBrachId;
   }

    //contrustor request
    [JsonConstructor]
    public AccountDTO(string emailAddress, string phoneNumber, AccountRole role, AccountStatus status, string password, Guid? photoBoothBrachId)
    {
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Role = role;
        Status = status;
        Password = password;
        PhotoBoothBrachId = photoBoothBrachId;
    }
}
