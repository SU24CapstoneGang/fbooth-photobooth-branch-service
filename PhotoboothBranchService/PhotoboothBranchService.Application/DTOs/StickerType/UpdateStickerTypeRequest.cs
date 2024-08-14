using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.StickerType
{
    public class UpdateStickerTypeRequest
    {
        public string? StickerTypeName { get; set; } = default!;
        public IFormFile? file { get; set; }
    }
}
