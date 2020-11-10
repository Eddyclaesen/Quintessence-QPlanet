using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Commands
{
    public class CreateMemoProgramComponentCommand : IRequest<MemoProgramComponent>
    {
        public CreateMemoProgramComponentCommand(Guid simulationCombinationId, Guid userId, List<Memo> memos, List<CalendarDay> calendarDays)
        {
            SimulationCombinationId = simulationCombinationId;
            UserId = userId;
            Memos = memos;
            CalendarDays = calendarDays;
        }

        public Guid SimulationCombinationId { get;  }
        public Guid UserId { get; }
        public List<Memo> Memos { get; }
        public List<CalendarDay> CalendarDays { get;  }
        
    }
}