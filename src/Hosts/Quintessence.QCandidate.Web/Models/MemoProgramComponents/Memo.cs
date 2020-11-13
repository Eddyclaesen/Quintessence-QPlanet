using System;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class Memo
    {
        public Memo(Guid id, int position, string title, string content)
        {
            Id = id;
            Position = position;
            Title = title;
            Content = content;
        }

        public Guid Id { get; }
        public int Position { get; }
        public string Title { get; }
        public string Content { get; }
    }
}
