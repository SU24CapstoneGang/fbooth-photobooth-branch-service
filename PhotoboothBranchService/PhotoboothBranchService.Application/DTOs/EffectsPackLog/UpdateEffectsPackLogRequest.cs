using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.EffectsPackLog
{
    public class UpdateEffectsPackLogRequest
    {
        public Guid PictureID { get; set; }
        public Guid FrameID { get; set; }
        public Guid FilterID { get; set; }
    }

}
