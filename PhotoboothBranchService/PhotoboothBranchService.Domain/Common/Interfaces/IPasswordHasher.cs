using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IPasswordHasher
{
    byte[] GenerateSalt();
    byte[] HashPassword(string password, byte[] salt);
}

