using Kenze.Domain;
using Kenze.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Core.Domain
{
    public class MemoProgramComponent : ChangeableEntity<Guid>, IAggregateRoot
    {
        private MemoProgramComponent()
        {
        }

        public MemoProgramComponent(Guid simulationCombinationId, Guid userId, List<Memo> memos)
        {
            SimulationCombinationId = simulationCombinationId;
            UserId = userId;
            Memos = memos;
            CalendarDays = GetCalendarDays();
        }

        private ICollection<CalendarDay> GetCalendarDays()
        {
            var calendarDays = new List<CalendarDay>();
            var startDate = new DateTime(2018,4,2);
            var weekDays = new List<DayOfWeek> {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

            while ((startDate.Month == 4) && (startDate.Year == 2018))
            {
                var calendarDay = new CalendarDay(Guid.Empty, startDate, null);
                if (weekDays.Contains(startDate.DayOfWeek))
                {
                    calendarDays.Add(calendarDay);
                }

                startDate = startDate.AddDays(1);
            }

            return calendarDays;
        }

        public Guid SimulationCombinationId { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<Memo> Memos { get; private set; }
        public ICollection<CalendarDay> CalendarDays { get; private set; }
        
    }
}