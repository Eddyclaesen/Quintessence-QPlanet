using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class UpdateProgramComponentRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid? LeadAssessorUserId { get; set; }

        [DataMember]
        public Guid? CoAssessorUserId { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}