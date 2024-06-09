namespace PhotoboothBranchService.Application.Common.Helpers
{
    public static class IpAddressHelper
    {
        public static string GetClientIpAddress(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
