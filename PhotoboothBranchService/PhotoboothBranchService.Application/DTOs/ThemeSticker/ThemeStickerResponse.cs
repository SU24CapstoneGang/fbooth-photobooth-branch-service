﻿using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ThemeSticker
{
    public class ThemeStickerResponse
    {
        public Guid ThemeStickerID { get; set; }
        public string ThemeStickerName { get; set; }
        public string ThemeStickerDescription { get; set; }
        public StatusUse Status { get; set; }
    }
}
