using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Helpers
{
    public static class PasswordGenerator
    {
        private static readonly Random _random = new Random();

        public static string GeneratePassword(int length = 10)
        {
            if (length < 6 || length > 100)
                throw new ArgumentException("Password length must be between 6 and 100 characters.");

            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";

            // Ensure password contains at least one lowercase letter, one uppercase letter, and one digit
            StringBuilder password = new StringBuilder();
            password.Append(lowercase[_random.Next(lowercase.Length)]);
            password.Append(uppercase[_random.Next(uppercase.Length)]);
            password.Append(digits[_random.Next(digits.Length)]);

            // Fill the remaining length with a random mix of lowercase, uppercase, and digits
            string allChars = lowercase + uppercase + digits;
            for (int i = 3; i < length; i++)
            {
                password.Append(allChars[_random.Next(allChars.Length)]);
            }

            // Shuffle the characters in the password to ensure randomness
            return new string(password.ToString().OrderBy(c => _random.Next()).ToArray());
        }
    }
}
