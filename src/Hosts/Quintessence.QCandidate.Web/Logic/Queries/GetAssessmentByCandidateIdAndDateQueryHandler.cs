using Dapper;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetAssessmentByCandidateIdAndDateQueryHandler : DapperQueryHandler<GetAssessmentByCandidateIdAndDateQuery, AssessmentDto>
    {
        public GetAssessmentByCandidateIdAndDateQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<AssessmentDto> Handle(GetAssessmentByCandidateIdAndDateQuery query, CancellationToken cancellationToken)
        {
            using(var dbConnection = DbConnectionFactory.Create())
            {
                var result = new AssessmentDto();
                await dbConnection.QueryAsync<AssessmentDto>("[QCandidate].[Assessment_GetByCandidateIdAndDate]",
                    new[]
                    {
                        typeof(CustomerDto),
                        typeof(PositionDto),
                        typeof(DayProgramDto),
                        typeof(LocationDto),
                        typeof(ProgramComponentDto),
                        typeof(RoomDto),
                        typeof(UserDto),
                        typeof(UserDto)
                    },
                    obj =>
                    {
                        if(result.Customer == null)
                        {
                            result.Customer = obj[0] as CustomerDto;
                        }

                        if(result.Position == null)
                        {
                            result.Position = obj[1] as PositionDto;
                        }

                        if(result.DayProgram == null)
                        {
                            result.DayProgram = obj[2] as DayProgramDto ?? new DayProgramDto();
                        }

                        if(result.DayProgram.Location == null)
                        {
                            result.DayProgram.Location = obj[3] as LocationDto;
                        }

                        var programComponent = obj[4] as ProgramComponentDto;
                        programComponent.Room = obj[5] as RoomDto;
                        programComponent.LeadAssessor = obj[6] as UserDto;
                        programComponent.CoAssessor = obj[7] as UserDto;

                        AddProgramComponent(result.DayProgram, programComponent);

                        return result;
                    },
                    param: new
                    {
                        candidateId = query.CandidateId,
                        date = query.Date
                    },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id,Id,Date,Id,Id,Id,Id,Id").ConfigureAwait(false);

                return result.Customer != null ? result : null;
            }
        }

        private void AddProgramComponent(DayProgramDto dayProgramDto, ProgramComponentDto programComponentDto)
        {
            var programComponents = dayProgramDto.ProgramComponents?.ToList() ?? new List<ProgramComponentDto>();
            programComponents.Add(programComponentDto);
            dayProgramDto.ProgramComponents = programComponents;
        }
    }
}