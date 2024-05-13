using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Frame
    {
        [Required]
        public Guid FrameID { get; set; } = default!;
        [Required]
        public string FrameName { get; set; } =default!;
        [Required]
        public string FrameURL { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public virtual List<EffectsPack> EffectsPacks { get; set; }
    }
}
