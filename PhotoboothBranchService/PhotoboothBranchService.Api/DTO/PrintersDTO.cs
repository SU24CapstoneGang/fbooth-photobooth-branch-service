using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.DTO;

public class PrintersDTO
{
    public string PrinterId { get; set; }
    public string ModelName { get; } = null;
    public float Price { get; }

    public PrintersDTO(string printerId,string modelName, string lens, float price)
    {
        PrinterId = printerId;
        ModelName = modelName;
        Price = price;
    }
    [JsonConstructor]

    public PrintersDTO(string modelName, string lens, float price)
    {
        ModelName = modelName;
        Price = price;
    }
}
