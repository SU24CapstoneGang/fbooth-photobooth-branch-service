using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs;

public class PrinterDTO
{
    public Guid? PrinterId { get; set; }
    public string ModelName { get; set; }
    public float Price { get; set; }

}
