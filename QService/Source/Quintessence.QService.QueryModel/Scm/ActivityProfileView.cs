using System;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityProfileView : ViewEntityBase
    {
        [DataMember]
        public Guid ActivityId { get; set; }

        [DataMember]
        public ActivityView Activity { get; set; }

        [DataMember]
        public Guid ProfileId { get; set; }

        [DataMember]
        public string ProfileName { get; set; }

        [DataMember]
        public decimal DayRate { get; set; }

        [DataMember]
        public decimal HalfDayRate { get; set; }

        [DataMember]
        public decimal HourlyRate { get; set; }

        [DataMember]
        public decimal IsolatedHourlyRate { get; set; }
    }
}
