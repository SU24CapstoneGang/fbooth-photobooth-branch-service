namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IPasswordHasher
{
    byte[] GenerateSalt();
    byte[] HashPassword(string password, byte[] salt);
}

