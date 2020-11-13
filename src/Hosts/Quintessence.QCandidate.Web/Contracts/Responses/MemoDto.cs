using System;

namespace Quintessence.QCandidate.Contracts.Responses
{
    public class MemoDto
    {
        public Guid Id { get; set; } 
        public int Position { get; set; } 
        public int OriginPosition { get; set; } 
        public string Title { get; set; } 

    }
}