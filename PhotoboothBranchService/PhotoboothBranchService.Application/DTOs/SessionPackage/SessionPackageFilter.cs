namespace PhotoboothBranchService.Application.DTOs.SessionPackage
{
    public class SessionPackageFilter
    {
        public string? SessionPackageName { get; set; } = default!;
        public string? SessionPackageDescription { get; set; } = default!;
        public decimal? Price { get; set; }
        public short? EmailSendCount { get; set; }
        public short? PrintCount { get; set; }
        public short? Duration { get; set; }
    }
}
