using Kenze.Domain;
using Kenze.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quintessence.QCandidate.Core.Domain
{
    public class MemoProgramComponent : ChangeableEntity<Guid>, IAggregateRoot
    {
        private MemoProgramComponent()
        {
        }

        public MemoProgramComponent(Guid id, Guid simulationCombinationId, Guid userId, IEnumerable<Memo> memos)
            : this(id, simulationCombinationId, userId, memos, InitCalendarDays())
        {
        }

        public MemoProgramComponent(Guid id, Guid simulationCombinationId, Guid userId, IEnumerable<Memo> memos, IEnumerable<CalendarDay> calendarDays)
        {
            Id = id;
            SimulationCombinationId = simulationCombinationId;
            UserId = userId;
            Memos = memos.ToList();
            CalendarDays = calendarDays.ToList();
        }

        public Guid SimulationCombinationId { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<Memo> Memos { get; private set; }
        public ICollection<CalendarDay> CalendarDays { get; private set; }

        private static IEnumerable<CalendarDay> InitCalendarDays()
        {
            var calendarDays = new List<CalendarDay>();
            var startDate = new DateTime(2018, 4, 2);
            var weekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

            while ((startDate.Month == 4) && (startDate.Year == 2018))
            {
                if (weekDays.Contains(startDate.DayOfWeek))
                {
                    calendarDays.Add(new CalendarDay(startDate));
                }

                startDate = startDate.AddDays(1);
            }

            return calendarDays;
        }

        public void UpdateMemo(Guid memoId, int position)
        {
            var memo = Memos.Single(m => m.Id == memoId);

            memo.Update(position);
        }
        
        public void UpdateCalendarDay (Guid id, string note)
        {
            var calendarDay = CalendarDays.Single(x => x.Id == id);

            calendarDay.Update(note);
        }
    }
}