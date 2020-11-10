using System;
using System.ComponentModel.DataAnnotations.Schema;
using Kenze.Domain;

namespace Quintessence.QCandidate.Core.Domain
{
    public class Memo : ChangeableEntity<Guid>
    {
        public Memo(Guid memoProgramComponentId, int position, Guid originId)
        {
            MemoProgramComponentId = memoProgramComponentId;
            Position = position;
            OriginId = originId;
        }

        public Guid MemoProgramComponentId { get; set; }
        public int Position { get; private set; }
        public Guid OriginId { get; private set; }
        [ForeignKey("MemoProgramComponentId")]
        public MemoProgramComponent MemoProgramComponent { get; set; }

        public void UpdatePosition(int position)
        {
            Position = position;
        }

    }
}