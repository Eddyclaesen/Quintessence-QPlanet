using System;
using System.Collections.Generic;
using Kenze.Domain;
using Kenze.Domain.Interfaces;

namespace Quintessence.QCandidate.Core.Domain
{
    public class MemoProgramComponent : ChangeableEntity<Guid>, IAggregateRoot
    {
        public MemoProgramComponent(Guid simulationCombinationId, ICollection<Memo> memos, ICollection<CalendarDay> calendarDays)
        {
            SimulationCombinationId = simulationCombinationId;
            Memos = memos;
            CalendarDays = calendarDays;
        }

        public Guid SimulationCombinationId { get; private set; }
        public ICollection<Memo> Memos { get; private set; }
        public ICollection<CalendarDay> CalendarDays { get; private set; }
        
    }
}