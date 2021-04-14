using System;
using System.IO;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetNeoPdfByCandidateIdQuery : IRequest<FileStream>
    {
        public GetNeoPdfByCandidateIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}