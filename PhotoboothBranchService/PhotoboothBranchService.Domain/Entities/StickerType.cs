﻿using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class StickerType
    {
        public Guid StickerTypeID { get; set; }
        public string StickerTypeName { get; set; } = default!;
        public string RepresentImageURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public virtual ICollection<Sticker> Stickers { get; set; }
    }
}
