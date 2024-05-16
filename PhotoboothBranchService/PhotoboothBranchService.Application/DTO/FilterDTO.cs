using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class FilterDTO
{
    public Guid? FilterID { get; set; } = default!;
    public string FilterName { get; set; } = default!;
    public string FilterURL { get; set; } = default!;

}
