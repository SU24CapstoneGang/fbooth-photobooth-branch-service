using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.AuthentiacationService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Account account);
    }
}
