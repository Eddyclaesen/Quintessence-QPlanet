using Quintessence.QCandidate.Core.Domain;
using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class MemoProgramComponent
    {
        public MemoProgramComponent(Guid id, string intro, string functionDescription, string context, List<Memo> memos, List<CalendarDay> calendatrDays)
        {
            Id = id;
            Intro = intro;
            FunctionDescription = functionDescription;
            Context = context;
            CalendarDays = calendatrDays;
            Memos = memos;
        }

        public Guid Id { get; }
        public string Intro { get; }
        public string FunctionDescription { get; }
        public string Context { get; }
        public ICollection<Memo> Memos { get; }
        //public Diction
        public ICollection<CalendarDay> CalendarDays { get; }
    }
}
