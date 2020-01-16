using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCategoryDetailSimulationInformationRequest
    {
        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public string SimulationRemarks { get; set; }

        [DataMember]
        public Guid? SimulationContextId { get; set; }
    }
}