using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetPredecessorCalendarDaysBySimulationIdAndUserIdQuery : IRequest<IEnumerable<CalendarDay>>
    {
        public GetPredecessorCalendarDaysBySimulationIdAndUserIdQuery(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
        public Guid Id { get; }
        public Guid UserId { get;  }
    }
}
