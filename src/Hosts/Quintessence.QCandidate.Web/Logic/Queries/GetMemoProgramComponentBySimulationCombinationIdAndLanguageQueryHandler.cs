using Dapper;
using Kenze.Infrastructure.Dapper;
using Kenze.Infrastructure.Interfaces;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetMemoProgramComponentBySimulationCombinationIdAndLanguageQueryHandler : DapperQueryHandler<GetMemoProgramComponentBySimulationCombinationIdAndLanguageQuery, MemoProgramComponentDto>
    {
        public GetMemoProgramComponentBySimulationCombinationIdAndLanguageQueryHandler(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public override async Task<MemoProgramComponentDto> Handle(GetMemoProgramComponentBySimulationCombinationIdAndLanguageQuery andLanguageQuery, CancellationToken cancellationToken)
        {
            var idParameter = new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = andLanguageQuery.Id };
            var languageParameter = new SqlParameter("language", SqlDbType.Char) { Value = andLanguageQuery.Language };

            var parameters = new SqlParameter[] { idParameter, languageParameter };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[MemoProgramComponent_GetBySimulationCombinationIdAndLanguage]", parameters).ToCommandDefinition();

            var result = new MemoProgramComponentDto();
            result.SimulationCombinationId = andLanguageQuery.Id;

            using (var dbConnection = DbConnectionFactory.Create())
            {
                var memoProgramComponents =
                    await dbConnection.QueryAsync<MemoProgramComponentDto, MemoDto, CalendarDayDto, MemoProgramComponentDto>(command,
                        (memoProgramComponentDto, memoDto, calendarDayDto) =>
                        {
                            if (!result.Memos.Any(x => x.Id == memoDto.Id))
                            {
                                result.Memos.Add(memoDto);
                            }

                            if (!result.CalendarDays.Any(x => x.Id == calendarDayDto.Id))
                            {
                                result.CalendarDays.Add(calendarDayDto);
                            }
                            
                            return result;
                        },
                        splitOn: "Id, Id, Id");
                
                return result;
            }
        }
    }
}