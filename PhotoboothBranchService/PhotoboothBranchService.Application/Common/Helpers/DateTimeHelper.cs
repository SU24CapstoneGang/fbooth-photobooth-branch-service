using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo VietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        private static readonly TimeSpan UtcPlus2Offset = TimeSpan.FromHours(2);
        public static DateTime GetVietnamTimeNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, VietnamTimeZone);
        }

        public static DateTime ConvertToVietnamTime(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, VietnamTimeZone);
        }
        public static DateTime ConvertFromUtcPlus2ToVietnamTime(DateTime utcPlus2DateTime)
        {
            // Convert UTC+2 to UTC
            DateTime utcDateTime = utcPlus2DateTime - UtcPlus2Offset;
            // Convert UTC to Vietnam time
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, VietnamTimeZone);
        }
    }
}
