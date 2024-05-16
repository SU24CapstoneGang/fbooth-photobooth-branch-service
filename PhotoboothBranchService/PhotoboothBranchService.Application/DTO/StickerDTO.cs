using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class StickerDTO
{
    public Guid? StickerId { get; set; } = default!;
    public string StickerName { get; set; } = default!;
    public string StrickerURL { get; set; } = default!;

    [JsonConstructor]
    public StickerDTO(string stickerName, string strickerURL)
    {
        StickerName = stickerName;
        StrickerURL = strickerURL;
    }
}
