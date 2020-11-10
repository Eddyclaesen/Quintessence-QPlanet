using MediatR;
using Quintessence.QCandidate.Core.Domain;
using System;

namespace Quintessence.QCandidate.Core.Commands
{
    public class UpdateCalendarDayCommand : IRequest<MemoProgramComponent>
    {
        public UpdateCalendarDayCommand(Guid memoProgramComponentId, Guid calendarDayId, string text)
        {
            MemoProgramComponentId = memoProgramComponentId;
            CalendarDayId = calendarDayId;
            Text = text;
        }

        public Guid MemoProgramComponentId { get; }
        public Guid CalendarDayId { get; }
        public string Text { get; }
        

    }
}
