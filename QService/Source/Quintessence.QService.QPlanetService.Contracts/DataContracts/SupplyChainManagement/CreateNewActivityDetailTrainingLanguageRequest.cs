using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateNewActivityDetailTrainingLanguageRequest
    {
        [DataMember]
        public Guid ActivityDetailTrainingId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public int SessionQuantity { get; set; }
    }
}