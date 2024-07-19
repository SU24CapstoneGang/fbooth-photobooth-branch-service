using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Domain.IRepository;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<bool> IsEmailUnique(string email);
    Task<bool> IsPhoneNumberUnique(string phoneNumber);
}
