using System;

namespace Quintessence.QPlanet.Infrastructure.Nullable
{
    public static class Extensions
    {
        public static string ToShortDateString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToShortDateString() : string.Empty;
        }
        
        public static string ToString(this DateTime? dateTime, string format)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(format) : string.Empty;
        }
        
        public static string ToString(this decimal? number, string format)
        {
            return number.HasValue ? number.Value.ToString(format) : string.Empty;
        }
    }
}
