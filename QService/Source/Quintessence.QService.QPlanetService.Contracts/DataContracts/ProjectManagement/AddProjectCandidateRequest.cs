using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class AddProjectCandidateRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public int AppointmentId { get; set; }

        [DataMember]
        public Guid CandidateId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
