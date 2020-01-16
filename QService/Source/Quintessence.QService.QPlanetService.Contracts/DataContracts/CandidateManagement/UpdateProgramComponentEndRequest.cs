using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class UpdateProgramComponentEndRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int MinuteDelta { get; set; }
    }
}