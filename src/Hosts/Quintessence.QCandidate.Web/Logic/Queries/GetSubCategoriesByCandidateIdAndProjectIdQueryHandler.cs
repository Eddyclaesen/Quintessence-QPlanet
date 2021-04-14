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

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetSubCategoriesByCandidateIdAndProjectIdQueryHandler : DapperQueryHandler<GetSubCategoriesByCandidateIdAndProjectIdQuery, IEnumerable<SubCategories>>
    {
        public GetSubCategoriesByCandidateIdAndProjectIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<IEnumerable<SubCategories>> Handle(GetSubCategoriesByCandidateIdAndProjectIdQuery query, CancellationToken cancellationToken)
        {
            var candidateId = new SqlParameter("candidateId", SqlDbType.UniqueIdentifier) { Value = query.CandidateId };
            var projectId = new SqlParameter("projectId", SqlDbType.UniqueIdentifier) { Value = query.ProjectId };

            var parameters = new SqlParameter[] { candidateId, projectId };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[Project_GetSubCategoriesByCandidateIdAndProjectId]", parameters).ToCommandDefinition();

            using (var dbConnection = DbConnectionFactory.Create())
            {
                return await dbConnection.QueryAsync<SubCategories>(command);
            }
        }
    }
}
