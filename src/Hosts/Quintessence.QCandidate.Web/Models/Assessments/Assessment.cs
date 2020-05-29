using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Models.Assessments
{
    public class Assessment
    {
        public Assessment(string positionName, string customerName, string locationName, DateTime date, List<ProgramComponent> programComponents)
        {
            PositionName = positionName;
            CustomerName = customerName;
            LocationName = locationName;
            Date = date;
            ProgramComponents = programComponents;
        }

        public string CustomerName { get; }
        public string PositionName { get; }
        public string LocationName { get; }
        public DateTime Date { get; }
        public IEnumerable<ProgramComponent> ProgramComponents { get; }
    }
}