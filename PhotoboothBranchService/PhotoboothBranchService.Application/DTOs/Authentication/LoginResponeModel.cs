using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Authentication
{
    public class LoginResponeModel
    {
        public string TokenId { get; set; }
        public string RefreshToken { get; set; }
    }
}
