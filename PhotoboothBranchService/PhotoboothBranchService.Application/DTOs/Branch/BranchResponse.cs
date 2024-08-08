using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.BranchPhoto;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class BranchResponse
    {
        public Guid BranchID { get; set; }
        public string BranchName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Town { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public BranchStatus Status { get; set; } = default!;
        public TimeSpan OpeningTime { get; set; } // Thêm giờ mở cửa
        public TimeSpan ClosingTime { get; set; } // Thêm giờ đóng cửa
        public ICollection<BoothResponse> Booths { get; set; } = default!;
        public ICollection<BranchPhotoResponse> BranchPhotos { get; set; } = default!;
    }
}
