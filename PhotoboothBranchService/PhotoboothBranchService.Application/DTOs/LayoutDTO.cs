using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs;

public class LayoutDTO
{
    public Guid? LayoutID { get; set; }
    public string LayoutURL { get; set; } = default!;
    public double LayoutPrice { get; set; } = default!;
}
