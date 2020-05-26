using System;
using System.Linq;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Helpers
{
    public static class TimeslotHelper
    {
        /// <summary>
        /// Timings:
        /// 8h(start) -> 5
        /// +1 for each minute
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int CalculatePixelOffset(DateTime date)
        {
            var startOfDayPixelOffset = 5;
            var startOfDay = date.Date.AddHours(8);
            var endOfDay = date.Date.AddHours(19);

            if(date < startOfDay)
            {
                date = startOfDay;
            }

            if(date > endOfDay)
            {
                date = endOfDay;
            }

            var offsetFromStartOfDay = (int)(date - startOfDay).TotalMinutes + startOfDayPixelOffset;

            return offsetFromStartOfDay;
        }

        public static string GetTimeString(DateTime start, DateTime end)
        {
            return $"{start:HH}u{start:mm} - {end:HH}u{end:mm}";
        }

        public static string GetAssessorsString(UserDto leadAssessor, UserDto coAssessor)
        {
            var assessors = new[] { GetAssessorString(leadAssessor), GetAssessorString(coAssessor) };

            return string.Join(", ", assessors.Where(a => a != null));
        }

        private static string GetAssessorString(UserDto user)
        {
            if(string.IsNullOrWhiteSpace(user?.FirstName)
               && string.IsNullOrWhiteSpace(user?.LastName))
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}".Trim();
        }
    }
}