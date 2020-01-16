using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateCompetenceSimulationScoreRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public List<UpdateProjectCandidateIndicatorSimulationScoreRequest> Indicators { get; set; }
    }
}