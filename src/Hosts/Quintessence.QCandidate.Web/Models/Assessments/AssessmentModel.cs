using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Models.Assessments
{
    public class AssessmentModel
    {
        public AssessmentModel(string positionName, string customerName, string locationName, DateTime date, List<ProgramComponentModel> events)
        {
            PositionName = positionName;
            CustomerName = customerName;
            LocationName = locationName;
            Date = date;
            Events = events;
        }

        public string CustomerName { get; }
        public string PositionName { get; }
        public string LocationName { get; }
        public DateTime Date { get; }
        public List<ProgramComponentModel> Events { get; }
    }
}