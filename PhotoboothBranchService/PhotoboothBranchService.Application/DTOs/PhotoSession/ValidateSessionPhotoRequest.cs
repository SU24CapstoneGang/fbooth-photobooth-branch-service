using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class ValidateSessionPhotoRequest
    {
        public Guid PhotoSessionID { get; set; }
        public long ValidateCode { get; set; }
    }
}
