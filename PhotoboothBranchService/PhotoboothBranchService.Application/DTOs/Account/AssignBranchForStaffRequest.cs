using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Account
{
    public class AssignBranchForStaffRequest
    {
        public Guid StaffID { get; set; }
        public Guid BranchID { get; set; }
    }
}
