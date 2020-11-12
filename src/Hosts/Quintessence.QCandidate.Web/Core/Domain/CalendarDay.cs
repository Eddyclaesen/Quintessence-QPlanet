using System;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class CalendarDay : ChangeableEntity<Guid>
    {
        private CalendarDay()
        {
        }
        
        public CalendarDay(DateTime day)
        {
            Day = day;
        }

        public CalendarDay(Guid memoProgramComponentId, DateTime day, string note)
        {
            MemoProgramComponentId = memoProgramComponentId;
            Day = day;
            Note = note;
        }

        public Guid MemoProgramComponentId { get; private set; }
        public DateTime Day { get; private set; }
        public string Note { get; private set; }

        public void Update(string note)
        {
            Note = note;
        }
        
    }
}