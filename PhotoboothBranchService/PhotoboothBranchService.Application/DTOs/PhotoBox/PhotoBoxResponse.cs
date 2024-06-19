using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PhotoBox
{
    public class PhotoBoxResponse
    {
        public Guid PhotoBoxID { get; set; }
        public int boxLength { get; set; }
        public int boxWidth { get; set; }
        public int CoordinatesX { get; set; }
        public int CoordinatesY { get; set; }
        public Guid LayoutID { get; set; }
    }
}
