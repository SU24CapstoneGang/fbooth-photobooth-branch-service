using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs;

public class FrameDTO
{
    public Guid? FrameID { get; set; } = default!;
    public string FrameName { get; set; } = default!;
    public string FrameURL { get; set; } = default!;

}
