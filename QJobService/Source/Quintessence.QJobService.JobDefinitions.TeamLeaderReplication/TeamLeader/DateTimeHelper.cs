using System;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader
{
    public static class DateTimeHelper
    {
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        public static DateTime? UnixTimeStampToDateTime(double? unixTimeStamp)
        {
            DateTime? dtDateTime = null;
            if (unixTimeStamp.HasValue)
                dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp.Value).ToLocalTime();
            return dtDateTime;
        }
    }
}
