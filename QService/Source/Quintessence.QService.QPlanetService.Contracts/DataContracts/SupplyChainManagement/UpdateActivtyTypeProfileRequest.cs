using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateActivtyTypeProfileRequest : UpdateRequestBase
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