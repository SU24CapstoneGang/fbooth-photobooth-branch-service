using PhotoboothBranchService.Application.DTOs.Account;

namespace PhotoboothBranchService.Application.Services.FirebaseServices
{
    public interface IFirebaseService
    {
        Task<string> RegisterAsync(string email, string password);
        Task<string> GetResetPasswordLink(string email);
        Task DeleteUserAsync(string email);
        Task UpdatePasswordOnFirebase(string email, string newPassword);
        Task<IEnumerable<AccountResponse>> GetAllUsersAsync();
    }
}
