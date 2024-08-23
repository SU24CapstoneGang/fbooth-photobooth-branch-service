using PhotoboothBranchService.Application.DTOs.BoothPhoto;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class BoothResponse
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public BoothStatus Status { get; set; }
        public Guid BranchID { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<BoothPhotoResponse> BoothPhotos { get; set; } = default!;
    }
}
