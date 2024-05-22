namespace PhotoboothBranchService.Application.Services.FirebaseServices
{
    public interface IFirebaseService
    {
        public Task<string> RegisterAsync(string email, string password);
        public Task<string> GetResetPasswordLink(string email);
    }
}
