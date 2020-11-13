using System;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class MemoDto
    {
        public Guid Id { get; set; }  //Memo
        public int Position { get; set; } //Memo
        public Guid MemoProgramId { get; set; } // MemoProgramComponent => nodig ??
        public Guid OriginId { get; set; } //SimulationCombinationMemos
        public int OriginPosition { get; set; } //SimulationCombinationMemos 
        public string Title { get; set; } //SimulationCombinationTranslations
        public string Content { get; set; } //from file
    }
}