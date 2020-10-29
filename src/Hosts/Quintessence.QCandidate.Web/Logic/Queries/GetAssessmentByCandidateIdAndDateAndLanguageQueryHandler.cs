using Dapper;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetAssessmentByCandidateIdAndDateAndLanguageQueryHandler : DapperQueryHandler<GetAssessmentByCandidateIdAndDateAndLanguageQuery, AssessmentDto>
    {
        public GetAssessmentByCandidateIdAndDateAndLanguageQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<AssessmentDto> Handle(GetAssessmentByCandidateIdAndDateAndLanguageQuery query, CancellationToken cancellationToken)
        {
            using (var dbConnection = DbConnectionFactory.Create())
            {
                var result = new AssessmentDto();

                // This way of working is needed because too many return types would be used otherwise
                await dbConnection.QueryAsync<AssessmentDto>("[QCandidate].[Assessment_GetByCandidateIdAndDateAndLanguage]",
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
                        date = query.Date,
                        language = query.Language
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