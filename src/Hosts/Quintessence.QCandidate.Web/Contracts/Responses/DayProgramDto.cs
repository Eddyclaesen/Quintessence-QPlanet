using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class DayProgramDto
    {
        public DateTime Date { get; set; }
        public LocationDto Location { get; set; }
        public IEnumerable<ProgramComponentDto> ProgramComponents { get; set; }
    }
}