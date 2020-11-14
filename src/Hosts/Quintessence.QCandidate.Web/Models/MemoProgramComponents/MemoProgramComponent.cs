using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class MemoProgramComponent
    {
        private DateTime _start;

        public MemoProgramComponent(Guid id, string name, DateTime start, string intro, string functionDescription, Guid contextId, IEnumerable<Memo> memos, IEnumerable<CalendarDay> calendarDays)
        {
            Id = id;
            Name = name;
            _start = start;
            Intro = intro;
            FunctionDescription = functionDescription;
            ContextId = contextId;
            CalendarDays = calendarDays;
            Memos = memos;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Intro { get; }
        public string FunctionDescription { get; }
        public Guid ContextId { get; }
        public IEnumerable<Memo> Memos { get; }
        public IEnumerable<CalendarDay> CalendarDays { get; }

        public bool CanShowContent => DateTime.Now.Date == _start.Date.Date && DateTime.Now > _start;
    }
}
