using MediatR;
using Quintessence.QCandidate.Core.Domain;
using System;
using System.Collections.Generic;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetPredecessorMemosBySimulationCombinationIdAndUserIdQuery : IRequest<IEnumerable<Memo>>
    {
        public GetPredecessorMemosBySimulationCombinationIdAndUserIdQuery(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
        public Guid Id { get; }
        public Guid UserId { get;  }
    }
}
