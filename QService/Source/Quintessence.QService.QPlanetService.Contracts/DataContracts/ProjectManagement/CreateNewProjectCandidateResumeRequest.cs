using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectCandidateResumeRequest
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }
    }
}