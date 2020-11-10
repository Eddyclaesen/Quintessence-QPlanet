using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Commands
{
    public class CreateMemoProgramComponentCommand : IRequest<MemoProgramComponent>
    {
        public CreateMemoProgramComponentCommand(Guid simulationCombinationId, Guid qCandidateId, IEnumerable<Memo> memos, IEnumerable<CalendarDay> calendarDays)
        {
            SimulationCombinationId = simulationCombinationId;
            QCandidateId = qCandidateId;
            Memos = memos;
            CalendarDays = calendarDays;
        }

        public Guid SimulationCombinationId { get;  }
        public Guid QCandidateId { get; }
        public IEnumerable<Memo> Memos { get; }
        public IEnumerable<CalendarDay> CalendarDays { get;  }
        
    }
}