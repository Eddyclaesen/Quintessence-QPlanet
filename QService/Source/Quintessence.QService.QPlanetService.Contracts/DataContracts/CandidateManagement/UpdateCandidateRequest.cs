using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class UpdateCandidateRequest : UpdateRequestBase
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public bool HasQCandidateAccess { get; set; }
        [DataMember]
        public Guid? QCandidateUserId { get; set; }
    }
}
