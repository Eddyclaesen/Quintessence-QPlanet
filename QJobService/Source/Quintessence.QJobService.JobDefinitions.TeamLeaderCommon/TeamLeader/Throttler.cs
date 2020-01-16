using System;
using System.Threading;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader
{
    public  static class Throttler
    {
        public static DateTime Throttle(DateTime startDateTime, TimeSpan timeFrame)
        {
            TimeSpan delta = DateTime.Now.Subtract(startDateTime);
            int milliSecondsToWait = (int)(timeFrame.TotalMilliseconds - delta.TotalMilliseconds);
            if (milliSecondsToWait > 0)
                Thread.Sleep(milliSecondsToWait);
            return DateTime.Now;
        }
    }
}
