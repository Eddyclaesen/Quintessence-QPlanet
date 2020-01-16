using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class RetrieveProjectPlanDetailRequest
    {
        [DataMember]
        public Guid? ProjectPlanId { get; set; }

        [DataMember]
        public Guid? ProjectId { get; set; }
    }
}
