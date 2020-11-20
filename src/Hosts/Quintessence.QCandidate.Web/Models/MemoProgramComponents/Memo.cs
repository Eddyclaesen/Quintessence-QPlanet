using System;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class Memo
    {
        public Memo(Guid id, int position, string title, string content, bool hasPredecessorOrigin = false)
        {
            Id = id;
            Position = position;
            Title = title;
            Content = content;
            HasPredecessorOrigin = hasPredecessorOrigin;
        }

        public Guid Id { get; }
        public int Position { get; }
        public string Title { get; }
        public string Content { get; }
        public bool HasPredecessorOrigin { get; }
    }
}
