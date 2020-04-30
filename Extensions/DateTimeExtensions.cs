using System;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string str)
            => DateTimeOffset.Parse(str).DateTime;
    }
}