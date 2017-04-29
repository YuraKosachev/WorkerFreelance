using System;


namespace Freelance.Web.Extensions
{
    public static class DateTimeExt
    {
        public static TimeSpan ConvertToTimeSpan(this DateTime dateTime)
        {
            return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
    }
}