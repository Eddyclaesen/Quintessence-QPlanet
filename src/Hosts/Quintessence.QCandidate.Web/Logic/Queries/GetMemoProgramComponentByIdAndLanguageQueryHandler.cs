using Dapper;
using Kenze.Infrastructure.Dapper;
using Kenze.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Models.MemoProgramComponents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarDay = Quintessence.QCandidate.Models.MemoProgramComponents.CalendarDay;
using Memo = Quintessence.QCandidate.Models.MemoProgramComponents.Memo;
using MemoProgramComponent = Quintessence.QCandidate.Models.MemoProgramComponents.MemoProgramComponent;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetMemoProgramComponentByIdAndLanguageQueryHandler : DapperQueryHandler<GetMemoProgramComponentByIdAndLanguageQuery, MemoProgramComponent>
    {
        private const string FunctionDescriptionsFolder = "FunctionDescriptions";
        private const string IntrosFolder = "Intros";
        private const string MemosFolder = "Memos";

        private readonly string _htmlStorageLocation;

        public GetMemoProgramComponentByIdAndLanguageQueryHandler(IDbConnectionFactory dbConnectionFactory, IOptionsMonitor<Settings> optionsMonitor) 
            : base(dbConnectionFactory)
        {
            _htmlStorageLocation = optionsMonitor.CurrentValue.HtmlStorageLocation;
        }

        public override async Task<MemoProgramComponent> Handle(GetMemoProgramComponentByIdAndLanguageQuery query, CancellationToken cancellationToken)
        {
            var memoProgramComponentDto = await GetMemoProgramComponentDto(query);

            var basePath = Path.Combine(_htmlStorageLocation, memoProgramComponentDto.SimulationCombinationId.ToString());

            var intro = System.IO.File.ReadAllText(Path.Combine(basePath, IntrosFolder, $"{query.Language.Code.ToUpperInvariant()}.html"));
            var functionDescription = System.IO.File.ReadAllText(Path.Combine(basePath, FunctionDescriptionsFolder, $"{query.Language.Code.ToUpperInvariant()}.html"));

            var contextId = Guid.Parse("5fa70b90-32d6-48fb-993c-0191d79da1c9");

            return new MemoProgramComponent(
                query.Id,
                intro,
                functionDescription,
                contextId,
                memoProgramComponentDto.Memos.Select(m => new Memo(m.Id, m.Position, m.Title, GetMemoContent(memoProgramComponentDto.SimulationCombinationId, m, query.Language))),
                memoProgramComponentDto.CalendarDays.Select(cd => new CalendarDay(cd.Id, cd.Day, cd.Note))
                );
        }

        private async Task<MemoProgramComponentDto> GetMemoProgramComponentDto(GetMemoProgramComponentByIdAndLanguageQuery query)
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
        private string GetMemoContent(Guid simulationCombinationId, MemoDto memo, Language language)
        {
            var file = Path.Combine(_htmlStorageLocation, simulationCombinationId.ToString(), MemosFolder, $"{memo.OriginPosition}_{language.Code.ToUpperInvariant()}.html");

            return System.IO.File.ReadAllText(file);
        }

        private class MemoProgramComponentDto
        {
            public MemoProgramComponentDto()
            {
                Memos = new List<MemoDto>();
                CalendarDays = new List<CalendarDayDto>();
            }

            public Guid SimulationCombinationId { get; set; }
            public List<MemoDto> Memos { get; set; }
            public List<CalendarDayDto> CalendarDays { get; set; }
        }

        private class CalendarDayDto
        {
            public Guid Id { get; set; }
            public DateTime Day { get; set; }
            public string Note { get; set; }
        }

        private class MemoDto
        {
            public Guid Id { get; set; }
            public int Position { get; set; }
            public int OriginPosition { get; set; }
            public string Title { get; set; }
        }
    }
}