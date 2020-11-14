using System;

namespace Quintessence.QCandidate.Core.Domain
{
    public class SimulationCombinationMemo
    {
        private SimulationCombinationMemo()
        {
        }

        public Guid Id { get; private set; }

        public Guid SimulationCombinationId { get; private set; }

        public int Position { get; private set; }
    }
}