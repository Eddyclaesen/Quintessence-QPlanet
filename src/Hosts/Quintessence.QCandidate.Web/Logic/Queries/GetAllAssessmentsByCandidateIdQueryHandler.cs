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
    public class GetAllAssessmentsByCandidateIdQueryHandler : DapperQueryHandler<GetAllAssessmentsByCandidateIdQuery, IEnumerable<AllAssessments>>
    {
        public GetAllAssessmentsByCandidateIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<IEnumerable<AllAssessments>> Handle(GetAllAssessmentsByCandidateIdQuery query, CancellationToken cancellationToken)
        {
            var candidateId = new SqlParameter("candidateId", SqlDbType.UniqueIdentifier) { Value = query.CandidateId };

            var parameters = new SqlParameter[] { candidateId };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[Assessments_GetByCandidateId]", parameters).ToCommandDefinition();

            using (var dbConnection = DbConnectionFactory.Create())
            {
                return await dbConnection.QueryAsync<AllAssessments>(command);
            }
        }
    }
}
