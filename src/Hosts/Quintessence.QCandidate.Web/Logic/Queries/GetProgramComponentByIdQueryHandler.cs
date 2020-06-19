using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
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
            using(var dbConnection = DbConnectionFactory.Create())
            {
                var result = new ProgramComponentDto();
                await dbConnection.QueryAsync<ProgramComponentDto>("[QCandidate].[ProgramComponent_GetById]",
                    new[]
                    {
                        typeof(ProgramComponentDto),
                        typeof(RoomDto),
                        typeof(UserDto),
                        typeof(UserDto)
                    },
                    obj =>
                    {
                        result = obj[0] as ProgramComponentDto;

                        if(result == null)
                        {
                            return null;
                        }

                        result.Room = obj[1] as RoomDto;
                        result.LeadAssessor = obj[2] as UserDto;
                        result.CoAssessor = obj[3] as UserDto;

                        return result;
                    },
                    param: new
                    {
                        id = query.Id,
                    },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id,Id,Id").ConfigureAwait(false);

                return result;
            }
        }
    }
}