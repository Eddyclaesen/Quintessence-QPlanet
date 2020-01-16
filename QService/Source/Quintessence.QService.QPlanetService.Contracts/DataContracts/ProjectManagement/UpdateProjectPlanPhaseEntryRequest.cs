using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    [KnownType(typeof(UpdateProjectPlanPhaseActivityRequest))]
    [KnownType(typeof(UpdateProjectPlanPhaseProductRequest))]
    public class UpdateProjectPlanPhaseEntryRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }
    }
}