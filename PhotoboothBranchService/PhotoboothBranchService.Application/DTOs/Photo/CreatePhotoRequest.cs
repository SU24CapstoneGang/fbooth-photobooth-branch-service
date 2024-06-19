using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Photo
{
    public class CreatePhotoRequest
    {
        public PhotoVersion Version { get; set; }
        public Guid PhotoSessionID { get; set; }
        public Guid FrameID { get; set; }
    }
}
