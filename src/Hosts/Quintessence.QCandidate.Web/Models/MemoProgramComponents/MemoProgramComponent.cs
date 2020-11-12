using Quintessence.QCandidate.Core.Domain;
using System;
using System.Collections.Generic;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Models.MemoProgramComponents
{
    public class MemoProgramComponent
    {
        public MemoProgramComponent(Guid id, string intro, string functionDescription, string context, List<MemoDto> memos, List<CalendarDayDto> calendarDays)
        {
            Id = id;
            Intro = intro;
            FunctionDescription = functionDescription;
            Context = context;
            CalendarDays = calendarDays;
            Memos = memos;
        }

        public Guid Id { get; }
        public string Intro { get; }
        public string FunctionDescription { get; }
        public string Context { get; }
        public ICollection<MemoDto> Memos { get; }
        public ICollection<CalendarDayDto> CalendarDays { get; }
    }
}
