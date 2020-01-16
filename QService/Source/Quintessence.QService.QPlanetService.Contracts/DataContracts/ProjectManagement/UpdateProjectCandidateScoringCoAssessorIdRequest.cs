using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateScoringCoAssessorIdRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid? ScoringCoAssessorId { get; set; }

        [DataMember]
        public Guid CandidateReportDefinitionId { get; set; }
    }
}