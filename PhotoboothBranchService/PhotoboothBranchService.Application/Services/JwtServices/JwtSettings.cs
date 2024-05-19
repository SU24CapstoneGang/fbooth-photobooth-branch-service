namespace PhotoboothBranchService.Application.Services.JwtServices
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string SecretKey { get; init; } = null!;
        public int ExpirationDays { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}
