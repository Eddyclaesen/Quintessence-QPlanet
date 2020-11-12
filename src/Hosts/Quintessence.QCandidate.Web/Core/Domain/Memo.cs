using System;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class Memo : ChangeableEntity<Guid>
    {
        private Memo()
        {
        }

        public Memo(Guid memoProgramComponentId, int position, Guid originId)
        {
            MemoProgramComponentId = memoProgramComponentId;
            Position = position;
            OriginId = originId;
        }

        public Guid MemoProgramComponentId { get; private set; }
        public int Position { get; private set; }
        public Guid OriginId { get; private set; }

        public void Update(int position)
        {
            Position = position;
        }

    }
}