using System;
using System.Collections.Generic;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class MemoProgramComponent
    {
        public MemoProgramComponent(Guid id, string intro, string functionDescription, Guid contextId, IEnumerable<Memo> memos, IEnumerable<CalendarDayDto> calendarDays)
        {
            Id = id;
            Intro = intro;
            FunctionDescription = functionDescription;
            ContextId = contextId;
            CalendarDays = calendarDays;
            Memos = memos;
        }

        public Guid Id { get; }
        public string Intro { get; }
        public string FunctionDescription { get; }
        public Guid ContextId { get; }
        public IEnumerable<Memo> Memos { get; }
        public IEnumerable<CalendarDayDto> CalendarDays { get; }
    }
}
