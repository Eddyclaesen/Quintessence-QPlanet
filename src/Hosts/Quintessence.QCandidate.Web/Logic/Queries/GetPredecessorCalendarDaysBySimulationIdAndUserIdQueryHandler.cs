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
    public class GetPredecessorCalendarDaysBySimulationIdAndUserIdQueryHandler : DapperQueryHandler<GetPredecessorCalendarDaysBySimulationIdAndUserIdQuery, IEnumerable<CalendarDay>>
    {
        public GetPredecessorCalendarDaysBySimulationIdAndUserIdQueryHandler(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public override async Task<IEnumerable<CalendarDay>> Handle(GetPredecessorCalendarDaysBySimulationIdAndUserIdQuery query, CancellationToken cancellationToken)
        {
            var simulationCombinationId = new SqlParameter("simulationCombinationId", SqlDbType.UniqueIdentifier) { Value = query.Id };
            var userId = new SqlParameter("userId", SqlDbType.UniqueIdentifier) { Value = query.UserId };

            var parameters = new SqlParameter[] { simulationCombinationId, userId };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[GetPredecessorCalendarDays_GetBySimulationCombinationIdAndUserId]", parameters).ToCommandDefinition();
            using (var dbConnection = DbConnectionFactory.Create())
            {
                return await dbConnection.QueryAsync<CalendarDay>(command);
            }
        }
    }
}
