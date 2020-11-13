using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class MemoProgramComponentDto
    {
        public MemoProgramComponentDto()
        {
            Memos = new List<MemoDto>();
            CalendarDays = new List<CalendarDayDto>();
        }
        public Guid SimulationCombinationId { get; set; }
        public ICollection<MemoDto> Memos { get; set; }
        public ICollection<CalendarDayDto> CalendarDays { get; set; }



    }
}