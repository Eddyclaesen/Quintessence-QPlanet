using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateResumeRequest : UpdateRequestBase
    {
        [DataMember]
        public int AdviceId{ get; set; }

        [DataMember]
        public string Reasoning { get; set; }

        [DataMember]
        public List<UpdateProjectCandidateResumeFieldRequest> ProjectCandidateResumeFields { get; set; }
    }
}