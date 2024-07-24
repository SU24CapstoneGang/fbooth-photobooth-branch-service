using PhotoboothBranchService.Application.DTOs.Sticker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class DashboardStickerResponse
    {
        public int Count { get; set; }
        public StickerResponse Sticker { get; set; }
    }
}
