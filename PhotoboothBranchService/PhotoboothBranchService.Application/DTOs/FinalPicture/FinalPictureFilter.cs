using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.FinalPicture
{
    public class FinalPictureFilter
    {
        public string? PictureURl { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastModified { get; set; }
        public PhotoPrivacy? PicturePrivacy { get; set; }
        public int? PrintQuantity { get; set; }
        public float? PictureCost { get; set; }
        public Guid? LayoutID { get; set; }
        public Guid? PrintPricingID { get; set; }
    }
}
