using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectRequest
    {
        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public Guid? MainProjectId { get; set; }

        [DataMember]
        public Guid? ProjectCandidateId { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public Guid ProjectTypeId { get; set; }

        [DataMember]
        public Guid? ProjectManagerUserId { get; set; }

        [DataMember]
        public Guid? CustomerAssistantUserId { get; set; }

        [DataMember]
        public int? CrmProjectId { get; set; }

        [DataMember]
        public Guid? CopyProjectId { get; set; }
    }
}
