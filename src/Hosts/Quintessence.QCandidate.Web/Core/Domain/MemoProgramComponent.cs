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

        public MemoProgramComponent(Guid id, Guid simulationCombinationId, Guid userId, IEnumerable<Memo> memos, IEnumerable<CalendarDay> calendarDays)
        {
            Id = id;
            SimulationCombinationId = simulationCombinationId;
            UserId = userId;
            Memos = memos.ToList();
            CalendarDays = GetCalendarDays(calendarDays);
        }

        
        private ICollection<CalendarDay> GetCalendarDays(IEnumerable<CalendarDay> calendarDays)
        {
            var newCalendarDays = new List<CalendarDay>();

            if (calendarDays.Any())
            {
                newCalendarDays.AddRange(calendarDays);
            }
            else
            {
                var startDate = new DateTime(2018, 4, 2);
                var weekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

                while ((startDate.Month == 4) && (startDate.Year == 2018))
                {
                    if (weekDays.Contains(startDate.DayOfWeek))
                    {
                        newCalendarDays.Add(new CalendarDay(startDate));
                    }

                    startDate = startDate.AddDays(1);
                }
            }


            return newCalendarDays;
        }

        public void UpdateCalendarDay (Guid id, string note)
        {
            var calendarDay = CalendarDays.Single(x => x.Id == id);
            calendarDay.Update(note);
        }

        public void AddPredecessorMemos(IEnumerable<Memo> predecessorMemos)
        {
            var highestPosition = Memos.OrderByDescending(x => x.Position).First().Position;

            foreach (var predecessorMemo in predecessorMemos)
            {
                var newPosition = highestPosition + predecessorMemo.Position;
                predecessorMemo.Update(newPosition);
                Memos.Add(predecessorMemo);
            }
        }

        public void AddPredecessorCalendarDays(IEnumerable<CalendarDay> predecessorCalendarDays)
        {
            foreach (var calendarDay in predecessorCalendarDays)
            {
                CalendarDays.Add(calendarDay);
            }
        }

        public Guid SimulationCombinationId { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<Memo> Memos { get; private set; }
        public ICollection<CalendarDay> CalendarDays { get; private set; }
        
    }
}