using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class UpdateProgramComponentStartRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int MinuteDelta { get; set; }
    }
}