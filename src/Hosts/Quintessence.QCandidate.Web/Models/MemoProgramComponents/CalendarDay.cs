using System;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class CalendarDay
    {
        public CalendarDay(Guid id, DateTime day, string note)
        {
            Id = id;
            Day = day;
            Note = note;
        }

        public Guid Id { get; }
        public DateTime Day { get; }
        public string Note { get; }
    }
}
