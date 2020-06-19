using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Quintessence.QCandidate.Infrastructure.Interfaces;

namespace Quintessence.QCandidate.Logic.Queries
{
    public abstract class DapperQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IDbConnectionFactory DbConnectionFactory;

        protected DapperQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public abstract Task<TResponse> Handle(TRequest query, CancellationToken cancellationToken);
    }
}