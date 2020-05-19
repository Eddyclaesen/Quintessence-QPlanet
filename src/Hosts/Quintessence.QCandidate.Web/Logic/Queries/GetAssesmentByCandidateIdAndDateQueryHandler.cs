using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Kenze.Infrastructure.Dapper;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetAssesmentByCandidateIdAndDateQueryHandler : DapperQueryHandler<GetAssesmentByCandidateIdAndDateQuery, AssessmentDto>
    {
        public GetAssesmentByCandidateIdAndDateQueryHandler(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public override async Task<AssessmentDto> Handle(GetAssesmentByCandidateIdAndDateQuery query, CancellationToken cancellationToken)
        {
            try
            {
                using(var dbConnection = DbConnectionFactory.Create())
                {
                    var candidateIdParameter = new SqlParameter("candidateId", SqlDbType.UniqueIdentifier) { Value = query.CandidateId };
                    var dateParameter = new SqlParameter("date", SqlDbType.Date) { Value = query.Date };

                    var result = new AssessmentDto();
                    await dbConnection.QueryAsync<CustomerDto, PositionDto, LocationDto, ProgramComponentGeneralInfoDto, RoomDto, UserDto, UserDto, AssessmentDto>(new StoredProcedureCommandDefinition("[QCandidate].[Assessment_GetByCandidateIdAndDate]", candidateIdParameter, dateParameter).ToCommandDefinition(),
                        (customerDto, positionDto, locationDto, programComponentGeneralInfoDto, roomDto, leadAssessorDto, coAssessorDto) =>
                        {
                            if(result.Customer == null)
                            {
                                result.Customer = customerDto;
                            }

                            if(result.Position == null)
                            {
                                result.Position = positionDto;
                            }

                            if(result.DayProgram == null)
                            {
                                result.DayProgram = new DayProgramDto
                                {
                                    Date = query.Date,
                                    Location = locationDto,
                                    ProgramComponents = new List<ProgramComponentDto>()
                                };
                            }

                            var programComponent = new ProgramComponentDto
                            {
                                Id = programComponentGeneralInfoDto.Id,
                                Start = programComponentGeneralInfoDto.Start,
                                End = programComponentGeneralInfoDto.End,
                                Name = programComponentGeneralInfoDto.Name,
                                Description = programComponentGeneralInfoDto.Description,
                                SimulationCombinationId = programComponentGeneralInfoDto.SimulationCombinationId,
                                Room = roomDto,
                                LeadAssessor = leadAssessorDto,
                                CoAssessor = coAssessorDto
                            };

                            result.DayProgram.ProgramComponents.Add(programComponent);

                            return result;
                        }, "Id,Id,Id,Id,Id,Id,Id").ConfigureAwait(false);

                    return result;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}