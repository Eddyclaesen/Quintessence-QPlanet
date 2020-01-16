using System;
using System.Collections.Generic;
using System.Linq;

namespace Quintessence.QService.Core.Performance
{
    public static class Extensions
    {
        public static IEnumerable<TEntity> SkipTake<TEntity>(this IEnumerable<TEntity> entities, PagingInfo pagingInfo)
        {
            return entities.Skip(pagingInfo.Page).Take(pagingInfo.PageLength);
        }

        public static void ExecuteForType<TAsType>(this object entity, Action<TAsType> action) where TAsType : class
        {
            var asType = entity as TAsType;

            if (asType != null)
                action(asType);
        }

        public static DateTime AddWorkdays(this DateTime date, int workdays)
        {
            DateTime temp = date;
            while (workdays > 0)
            {
                temp = temp.AddDays(1);
                if (temp.DayOfWeek < DayOfWeek.Saturday && temp.DayOfWeek > DayOfWeek.Sunday)
                    workdays--;
            }
            return temp;
        }

        public static int DifferenceInWorkdays(this DateTime date, DateTime compareDate)
        {
            var workdayCount = 0;
            int diff;
            DateTime temp;
            if (date > compareDate)
            {
                diff = (date - compareDate).Days;
                temp = compareDate;
                for (int i = 0; i < diff; i++)
                {
                    temp = temp.AddDays(1);
                    if (temp.DayOfWeek < DayOfWeek.Saturday && temp.DayOfWeek > DayOfWeek.Sunday)
                        workdayCount++;
                }
            }
            else
            {
                diff = (compareDate - date).Days;
                temp = date;
                for (int i = 0; i < diff; i++)
                {
                    temp = temp.AddDays(1);
                    if (temp.DayOfWeek < DayOfWeek.Saturday && temp.DayOfWeek > DayOfWeek.Sunday)
                        workdayCount++;
                }
            }

            return workdayCount;
        }

        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds)
        {
            var timeSpan = new TimeSpan(hours, minutes, seconds);
            return date.Date + timeSpan;
        }

        public static string ToInitials(this string name)
        {
            var initials = name.Split(' ', '-').ToList();
            var initialsString = "";
            initials.ForEach(i => initialsString += i.Substring(0, 1));
            return initialsString;
        }
    }
}
