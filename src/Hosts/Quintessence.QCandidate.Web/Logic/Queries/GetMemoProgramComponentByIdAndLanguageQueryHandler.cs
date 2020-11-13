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
    public class GetMemoProgramComponentByIdAndLanguageQueryHandler : DapperQueryHandler<GetMemoProgramComponentByIdAndLanguageQuery, MemoProgramComponentDto>
    {
        public GetMemoProgramComponentByIdAndLanguageQueryHandler(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public override async Task<MemoProgramComponentDto> Handle(GetMemoProgramComponentByIdAndLanguageQuery query, CancellationToken cancellationToken)
        {
            var idParameter = new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = query.Id };
            var languageParameter = new SqlParameter("languageId", SqlDbType.Int) { Value = query.Language.Id };

            var parameters = new SqlParameter[] { idParameter, languageParameter };

            var command = new StoredProcedureCommandDefinition("[QCandidate].[MemoProgramComponent_GetByIdAndLanguage]", parameters).ToCommandDefinition();

            MemoProgramComponentDto result = null;

            using (var dbConnection = DbConnectionFactory.Create())
            {
                var memoProgramComponents =
                    await dbConnection.QueryAsync<MemoProgramComponentDto, MemoDto, CalendarDayDto, MemoProgramComponentDto>(command,
                        (memoProgramComponentDto, memoDto, calendarDayDto) =>
                        {
                            if (result == null)
                            {
                                result = memoProgramComponentDto;
                            }

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