using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectCandidateResumeFieldRequest
    {
        [DataMember]
        public Guid ProjectCandidateResumeId { get; set; }

        [DataMember]
        public Guid CandidateReportDefinitionFieldId { get; set; }
    }
}