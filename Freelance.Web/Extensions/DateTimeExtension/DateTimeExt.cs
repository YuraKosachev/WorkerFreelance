using System;


namespace Freelance.Web.Extensions
{
    public static class DateTimeExt
    {
        public static TimeSpan ConvertToTimeSpan(this DateTime dateTime)
        {
            return TimeSpan.Parse(dateTime.ToString("T"));
        }
    }
}