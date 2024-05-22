using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.MapSticker
{
    public class MapStickerResponse
    {
        public Guid MapStickerID { get; set; }
        public Guid PackLogID { get; set; }
        public Guid StickerId { get; set; }
    }
}
