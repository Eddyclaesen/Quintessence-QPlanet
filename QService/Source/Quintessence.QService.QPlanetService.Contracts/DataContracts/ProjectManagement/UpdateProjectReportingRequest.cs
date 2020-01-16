using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectReportingRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid? CandidateReportDefinitionId { get; set; }

        [DataMember]
        public int CandidateScoreReportTypeId { get; set; }
    }
}