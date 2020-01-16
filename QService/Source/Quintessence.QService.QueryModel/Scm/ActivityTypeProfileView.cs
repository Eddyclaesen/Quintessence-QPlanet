using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityTypeProfileView : ViewEntityBase
    {
        [DataMember]
        public Guid ActivityTypeId { get; set; }

        [DataMember]
        public ActivityTypeView ActivityType { get; set; }

        [DataMember]
        public string ActivityTypeName { get; set; }

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
