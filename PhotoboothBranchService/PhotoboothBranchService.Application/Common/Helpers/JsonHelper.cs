using Microsoft.Extensions.Configuration;

namespace PhotoboothBranchService.Application.Common.Helpers
{
    public static class JsonHelper
    {
        public static string GetFromAppSettings(string key)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            return configuration[key];
        }
    }
}
