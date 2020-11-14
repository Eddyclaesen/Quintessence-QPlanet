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
    public class GetSimulationCombinationMemosBySimulationCombinationIdQueryHandler : DapperQueryHandler<GetSimulationCombinationMemosBySimulationCombinationIdQuery, IEnumerable<SimulationCombinationMemo>>
    {
        public GetSimulationCombinationMemosBySimulationCombinationIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<IEnumerable<SimulationCombinationMemo>> Handle(GetSimulationCombinationMemosBySimulationCombinationIdQuery query, CancellationToken cancellationToken)
        {
            var simulationCombinationId = new SqlParameter("simulationCombinationId", SqlDbType.UniqueIdentifier) { Value = query.SimulationCombinationId };
            
            var parameters = new SqlParameter[] { simulationCombinationId };

            var command = new StoredProcedureCommandDefinition("[dbo].[SimulationCombinationMemo_GetAllBySimulationCombinationId]", parameters).ToCommandDefinition();

            using (var dbConnection = DbConnectionFactory.Create())
            {
                return await dbConnection.QueryAsync<SimulationCombinationMemo>(command);
            }
        }
    }
}