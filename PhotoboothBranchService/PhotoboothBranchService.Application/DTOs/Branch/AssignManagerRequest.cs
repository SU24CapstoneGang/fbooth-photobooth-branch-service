using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class AssignManagerRequest
    {
        [Required]
        public Guid ManagerID { get; set; }
    }
}
