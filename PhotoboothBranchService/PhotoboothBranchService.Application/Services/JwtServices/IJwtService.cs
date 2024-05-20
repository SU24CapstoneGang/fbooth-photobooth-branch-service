using PhotoboothBranchService.Application.DTOs.Authentication;

namespace PhotoboothBranchService.Application.Services.JwtServices
{
    public interface IJwtService
    {
        public Task<LoginResponeModel> GetForCredentialsAsync(string email, string password);
        public Task<LoginResponeModel> RefreshToken(string refreshToken);
    }
}
