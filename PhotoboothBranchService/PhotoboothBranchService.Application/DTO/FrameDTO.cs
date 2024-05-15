using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class FrameDTO
{
    public Guid FrameID { get; set; } = default!;
    public string FrameName { get; set; } = default!;
    public string FrameURL { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime LastModified { get; set; }
}
