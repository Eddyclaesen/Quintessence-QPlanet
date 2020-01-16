using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class CreateProgramComponentRequest
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public Guid? SimulationCombinationId { get; set; }

        [DataMember]
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }

        [DataMember]
        public Guid? ProgramComponentSpecialId { get; set; }

        [DataMember]
        public Guid AssessmentRoomId { get; set; }
    }
}