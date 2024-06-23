namespace PhotoboothBranchService.Application.Common.Helpers
{
    public static class GuidAlphanumericConverter
    {
        public static string GuidToAlphanumeric(Guid guid)
        {
            return guid.ToString("N"); // Remove hyphens from GUID
        }

        public static Guid AlphanumericToGuid(string alphanumeric)
        {
            // Ensure the string is in the correct format before converting
            if (Guid.TryParseExact(alphanumeric, "N", out Guid guid))
            {
                return guid;
            }
            else
            {
                throw new ArgumentException("Invalid alphanumeric string format.");
            }
        }
    }
}
