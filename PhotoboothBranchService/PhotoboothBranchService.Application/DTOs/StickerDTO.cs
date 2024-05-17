using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs;

public class StickerDTO
{
    public Guid? StickerId { get; set; }
    public string StickerName { get; set; }
    public string StrickerURL { get; set; }

}
