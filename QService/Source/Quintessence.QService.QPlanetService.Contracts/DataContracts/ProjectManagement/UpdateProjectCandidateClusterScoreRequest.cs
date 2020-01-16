using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateClusterScoreRequest : UpdateRequestBase
    {
        [DataMember]
        public List<UpdateProjectCandidateCompetenceScoreRequest> ProjectCandidateCompetenceScores { get; set; }

        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public string Statement { get; set; }
    }
}