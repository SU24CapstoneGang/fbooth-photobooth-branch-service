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
    public DateTime CreatedDate { get; set; }
    public DateTime LastModified { get; set; }

    [JsonConstructor]
    public FilterDTO(string filterName, string filterURL, DateTime createdDate, DateTime lastModified)
    {
        FilterName = filterName;
        FilterURL = filterURL;
        CreatedDate = createdDate;
        LastModified = lastModified;
    }
}
