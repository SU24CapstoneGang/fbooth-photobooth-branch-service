using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ThemeFilter
{
    public class UpdateThemeFilterRequest
    {
        public string ThemeFilterName { get; set; }
        public string ThemeFilterDescription { get; set; }
        public StatusUse Status { get; set; }
    }
}
