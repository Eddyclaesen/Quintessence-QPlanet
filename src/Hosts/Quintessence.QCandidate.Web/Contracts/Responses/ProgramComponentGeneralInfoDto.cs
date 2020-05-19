using System;

namespace Quintessence.QCandidate.Contracts.Responses
{
    internal class ProgramComponentGeneralInfoDto
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? SimulationCombinationId { get; set; }
    }
}