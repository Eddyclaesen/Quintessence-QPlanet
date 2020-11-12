using System;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class CalendarDayDto
    {
        public Guid Id { get; set; }
        public DateTime Day { get; set; }
        public string Note { get; set; }
    }
}