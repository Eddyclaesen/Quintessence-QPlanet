using System;

namespace Quintessence.QPlanet.Infrastructure
{
    public static class Extensions
    {
        public static double ToUnixTimestamp(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            return (date - epoch).TotalSeconds;
        }
    }
}
