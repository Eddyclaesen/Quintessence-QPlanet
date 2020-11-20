using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Kenze.Infrastructure.Dapper;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetProgramComponentByIdAndLanguageQueryHandler : DapperQueryHandler<GetProgramComponentByIdAndLanguageQuery, ProgramComponentDto>
    {
        public GetProgramComponentByIdAndLanguageQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<ProgramComponentDto> Handle(GetProgramComponentByIdAndLanguageQuery query, CancellationToken cancellationToken)
        {
            var idParameter = new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = query.Id };
            var languageIdParameter = new SqlParameter("languageId", SqlDbType.Int) { Value = query.Language.Id };

            var command = new StoredProcedureCommandDefinition("[dbo].[ProgramComponent_GetByIdAndLanguageId]", idParameter, languageIdParameter).ToCommandDefinition();

            using (var dbConnection = DbConnectionFactory.Create())
            {

                var programComponents = await dbConnection.QueryAsync<ProgramComponentDto, RoomDto, UserDto, UserDto, ProgramComponentDto>(command,
                    (programComponentDto, roomDto, leadAssessorUserDto, coAssessorUserDto) =>
                    {
                        programComponentDto.Room = roomDto;
                        programComponentDto.LeadAssessor = leadAssessorUserDto;
                        programComponentDto.CoAssessor = coAssessorUserDto;

                        return programComponentDto;
                    },
                    splitOn: "Id, Id, Id");


                return programComponents.Single();
            }
        }
    }
}