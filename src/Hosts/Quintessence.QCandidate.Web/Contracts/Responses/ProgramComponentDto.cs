using System;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class ProgramComponentDto
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoomDto Room { get; set; }
        public UserDto LeadAssessor { get; set; }
        public UserDto CoAssessor { get; set; }
        public Guid? SimulationCombinationId { get; set; }
        public int QCandidateLayoutId { get; set; }

    }
}