using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Infrastructure.Dapper;
using Quintessence.QCandidate.Infrastructure.Interfaces;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetProgramComponentByIdQueryHandler : DapperQueryHandler<GetProgramComponentByIdQuery, ProgramComponentDto>
    {
        public GetProgramComponentByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<ProgramComponentDto> Handle(GetProgramComponentByIdQuery query, CancellationToken cancellationToken)
        {
            var idParameter = new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = query.Id };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[ProgramComponent_GetById]", idParameter).ToCommandDefinition();

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