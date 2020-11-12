using System;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class MemoDto
    {
        public Guid Id { get; set; }
        public int Position { get; set; }
        public Guid MemoProgramId { get; set; }
        public Guid OriginId { get; set; }
        public int OriginPosition { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
    }
}