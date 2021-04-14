using Dapper;
using Kenze.Infrastructure.Dapper;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Models.Assessments;
using Quintessence.QCandidate.Core.Queries;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetProjectByCandidateIdAndProjectIdQueryHandler : DapperQueryHandler<GetProjectByCandidateIdAndProjectIdQuery, Project>
    {
        public GetProjectByCandidateIdAndProjectIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<Project> Handle(GetProjectByCandidateIdAndProjectIdQuery query, CancellationToken cancellationToken)
        {
            var candidateId = new SqlParameter("candidateId", SqlDbType.UniqueIdentifier) { Value = query.CandidateId };
            var projectId = new SqlParameter("projectId", SqlDbType.UniqueIdentifier) { Value = query.ProjectId };

            var parameters = new SqlParameter[] { candidateId, projectId };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[Project_GetByCandidateIdAndProjectId]", parameters).ToCommandDefinition();

            using (var dbConnection = DbConnectionFactory.Create())
            {
                var result = await dbConnection.QueryAsync<Project>(command);
                return result.FirstOrDefault();
            }
        }
    }
}
