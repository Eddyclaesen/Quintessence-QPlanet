using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectPlanPhaseActivityRequest : CreateNewProjectPlanPhaseEntryRequest
    {
        [DataMember]
        public Guid ActivityProfileId { get; set; }

        [DataMember]
        public decimal Duration { get; set; }

        [DataMember]
        public string Notes { get; set; }
    }
}