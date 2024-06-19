using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Theme
{
    public class UpdateThemeRequest
    {
        public string ThemeFrameName { get; set; }
        public string ThemeDescription { get; set; }
    }
}
