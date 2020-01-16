using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateProjectView
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public Guid SubProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string ProjectTypeName { get; set; }
    }
}