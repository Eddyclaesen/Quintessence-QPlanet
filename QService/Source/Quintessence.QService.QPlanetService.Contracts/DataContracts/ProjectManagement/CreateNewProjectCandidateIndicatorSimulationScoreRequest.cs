using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectCandidateIndicatorSimulationScoreRequest
    {
        [DataMember]
        public Guid DictionaryIndicatorId { get; set; }

        [DataMember]
        public Guid SimulationCombinationId { get; set; }

        [DataMember]
        public Guid ProjectCandidateId { get; set; }
    }
}