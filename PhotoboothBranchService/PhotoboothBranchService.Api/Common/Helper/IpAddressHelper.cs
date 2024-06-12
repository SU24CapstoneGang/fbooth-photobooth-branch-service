namespace PhotoboothBranchService.Api.Common.Helper
{
    public static class IpAddressHelper
    {
        public static string GetClientIpAddress(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
