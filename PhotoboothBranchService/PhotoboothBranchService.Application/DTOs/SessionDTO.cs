using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class SessionDTO
{
    public Guid? SessionID { get; set; } = default!;
    public DateTime StartTime { get; set; } = default!;
    public DateTime EndTime { get; set; } = default!;
    public Guid BranchesID { get; set; }
}
