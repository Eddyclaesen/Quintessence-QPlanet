using System;
using System.Globalization;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader
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

        public static DateTime? CombineDateAndTimeStringsToDateTime(string date, string time)
        {
            DateTime? dtDateTime = null;
            if (!String.IsNullOrEmpty(date))
            {
                DateTime tmpDate;
                if (!String.IsNullOrEmpty(time))
                {
                    if (DateTime.TryParseExact(String.Format("{0} {1}", date, time), "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out tmpDate))
                        dtDateTime = tmpDate;
                }
                else if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tmpDate))
                    dtDateTime = tmpDate;
            }
            return dtDateTime;
        }
    }
}
