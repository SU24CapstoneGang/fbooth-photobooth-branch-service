using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ThemeFrame
{
    public class UpdateThemeFrameRequest
    {
        public string ThemeFrameName { get; set; }
        public string ThemeFrameDescription { get; set; }
        public StatusUse Status { get; set; }
    }
}
