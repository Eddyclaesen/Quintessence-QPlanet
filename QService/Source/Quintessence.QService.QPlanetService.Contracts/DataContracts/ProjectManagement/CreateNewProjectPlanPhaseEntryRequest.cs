using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    [KnownType(typeof(CreateNewProjectPlanPhaseActivityRequest))]
    [KnownType(typeof(CreateNewProjectPlanPhaseProductRequest))]
    public class CreateNewProjectPlanPhaseEntryRequest
    {
        [DataMember]
        public Guid ProjectPlanPhaseId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }
    }
}