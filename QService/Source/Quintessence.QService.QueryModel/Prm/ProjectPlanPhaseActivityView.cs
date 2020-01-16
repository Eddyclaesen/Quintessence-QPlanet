using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectPlanPhaseActivityView : ProjectPlanPhaseEntryView
    {
        [DataMember]
        public Guid ActivityId { get; set; }

        [DataMember]
        public string ActivityName { get; set; }

        [DataMember]
        public Guid ProfileId { get; set; }

        [DataMember]
        public string ProfileName { get; set; }

        [DataMember]
        public decimal Duration { get; set; }

        [DataMember]
        public decimal DayRate { get; set; }

        [DataMember]
        public decimal HalfDayRate { get; set; }

        [DataMember]
        public decimal HourlyRate { get; set; }

        [DataMember]
        public decimal IsolatedHourlyRate { get; set; }

        [DataMember]
        public string Notes { get; set; }

    }
}