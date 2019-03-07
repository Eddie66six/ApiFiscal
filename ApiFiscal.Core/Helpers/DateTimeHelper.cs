using System;

namespace ApiFiscal.Core.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ToUserTimeZone(this DateTime time, string timeZoneId = null)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId ?? "Argentina Standard Time");
            return time.ToTimeZoneTime(timeZoneInfo);
        }
        private static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }
    }
}