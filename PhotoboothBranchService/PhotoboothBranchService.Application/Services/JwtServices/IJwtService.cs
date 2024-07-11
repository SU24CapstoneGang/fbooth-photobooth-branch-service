using PhotoboothBranchService.Application.DTOs.Authentication;

namespace PhotoboothBranchService.Application.Services.JwtServices
{
    public interface IJwtService
    {
        Task<LoginResponeModel> GetForCredentialsAsync(string email, string password);
        Task<LoginResponeModel> RefreshToken(string refreshToken);
    }
}
