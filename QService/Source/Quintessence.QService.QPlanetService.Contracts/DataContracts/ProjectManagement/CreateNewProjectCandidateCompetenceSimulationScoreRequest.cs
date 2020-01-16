using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectCandidateCompetenceSimulationScoreRequest
    {
        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public Guid SimulationCombinationId { get; set; }

        [DataMember]
        public Guid ProjectCandidateId { get; set; }
    }
}