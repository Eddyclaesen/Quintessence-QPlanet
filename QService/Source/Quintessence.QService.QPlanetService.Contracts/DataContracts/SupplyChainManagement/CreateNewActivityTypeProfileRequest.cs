using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateNewActivityTypeProfileRequest
    {
        [DataMember]
        public Guid ActivityTypeId { get; set; }

        [DataMember]
        public Guid ProfileId { get; set; }

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