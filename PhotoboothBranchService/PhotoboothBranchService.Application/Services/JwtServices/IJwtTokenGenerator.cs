using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.Services.JwtServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Account account);
    }
}
