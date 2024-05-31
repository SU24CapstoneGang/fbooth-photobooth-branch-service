﻿using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.FinalPicture
{
    public class FinalPictureResponse
    {
        public Guid PictureID { get; set; }
        public string PictureURl { get; set; }
        public DateTime CreateDate { get; set; }
        public PhotoPrivacy PicturePrivacy { get; set; }
    }
}
