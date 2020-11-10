using System;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class MemoProgramComponent
    {
        public MemoProgramComponent(Guid id, string functionDescription)
        {
            Id = id;
            FunctionDescription = functionDescription;
        }

        public Guid Id { get; }

        public string FunctionDescription { get; }
    }
}
