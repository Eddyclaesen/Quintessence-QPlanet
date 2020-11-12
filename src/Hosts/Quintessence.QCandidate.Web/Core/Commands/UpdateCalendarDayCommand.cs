using MediatR;
using Quintessence.QCandidate.Core.Domain;
using System;

namespace Quintessence.QCandidate.Core.Commands
{
    public class UpdateCalendarDayCommand : IRequest<MemoProgramComponent>
    {
        public UpdateCalendarDayCommand(Guid memoProgramComponentId, Guid calendarDayId, string note)
        {
            MemoProgramComponentId = memoProgramComponentId;
            CalendarDayId = calendarDayId;
            Note = note;
        }

        public Guid MemoProgramComponentId { get; }
        public Guid CalendarDayId { get; }
        public string Note { get; }
        

    }
}
