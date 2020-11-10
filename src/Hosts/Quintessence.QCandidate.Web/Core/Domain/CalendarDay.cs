using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class CalendarDay : ChangeableEntity<Guid>
    {
        public CalendarDay(Guid memoProgramComponentId, DateTime day, string note)
        {
            MemoProgramComponentId = memoProgramComponentId;
            Day = day;
            Note = note;
        }

        public Guid MemoProgramComponentId { get; set; }
        public DateTime Day { get; private set; }
        public string Note { get; private set; }

        public MemoProgramComponent MemoProgramComponent { get; set; }

        public void UpdateNote(string note)
        {
            Note = note;
        }
        
    }
}