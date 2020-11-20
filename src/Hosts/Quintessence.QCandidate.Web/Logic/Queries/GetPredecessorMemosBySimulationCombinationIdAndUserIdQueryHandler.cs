using Dapper;
using Kenze.Infrastructure.Dapper;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetPredecessorMemosBySimulationCombinationIdAndUserIdQueryHandler : DapperQueryHandler<GetPredecessorMemosBySimulationCombinationIdAndUserIdQuery, IEnumerable<Memo>>
    {
        public GetPredecessorMemosBySimulationCombinationIdAndUserIdQueryHandler(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public override async Task<IEnumerable<Memo>> Handle(GetPredecessorMemosBySimulationCombinationIdAndUserIdQuery andUserIdQuery, CancellationToken cancellationToken)
        {
            var simulationCombinationId = new SqlParameter("simulationCombinationId", SqlDbType.UniqueIdentifier) { Value = andUserIdQuery.Id };
            var userId = new SqlParameter("userId", SqlDbType.UniqueIdentifier) { Value = andUserIdQuery.UserId };

            var parameters = new SqlParameter[] { simulationCombinationId, userId };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[Memos_GetAllFromPredecessorBySimulationCombinationIdAndUserId]", parameters).ToCommandDefinition();
            using (var dbConnection = DbConnectionFactory.Create())
            {
                return await dbConnection.QueryAsync<Memo>(command);
            }

        }
    }
}
