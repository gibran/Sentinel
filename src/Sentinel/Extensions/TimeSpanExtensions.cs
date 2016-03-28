using System;

namespace Sentinel.Extensions
{
    public static class TimeSpanExtensions
    {
        public static int GetTotalSeconds(this TimeSpan that)
        {
            return that.TotalSeconds <= 0 ? 1 : (int)that.TotalSeconds;
        }
    }
}