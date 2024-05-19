using PhotoboothBranchService.Domain.Common.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PhotoboothBranchService.Application.Common;

public class PasswordHasher : IPasswordHasher
{
    public byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }

    public byte[] HashPassword(string password, byte[] salt)
    {
        using (var hmac = new HMACSHA512(salt))
        {
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
