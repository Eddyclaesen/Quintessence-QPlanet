using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Kenze.Domain;
using Kenze.Domain.Interfaces;

namespace Quintessence.QCandidate.Core.Domain
{
    public class MemoProgramComponent : ChangeableEntity<Guid>, IAggregateRoot
    {
        public MemoProgramComponent(Guid simulationCombinationId, Guid userId, List<Memo> memos, List<CalendarDay> calendarDays)
        {
            SimulationCombinationId = simulationCombinationId;
            UserId = userId;
            Memos = memos;
            CalendarDays = calendarDays;
        }

        public MemoProgramComponent()
        {
            //EFCore
        }

        public Guid SimulationCombinationId { get; private set; }
        public Guid UserId { get; private set; }
        public List<Memo> Memos { get; private set; }
        public List<CalendarDay> CalendarDays { get; private set; }
        
    }
}