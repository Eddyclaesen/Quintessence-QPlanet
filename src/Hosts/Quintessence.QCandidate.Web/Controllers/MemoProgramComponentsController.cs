using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using MemoProgramComponent = Quintessence.QCandidate.Models.MemoProgramComponents.MemoProgramComponent;


namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class MemoProgramComponentsController : Controller
    {
        private const string FunctionDescriptionsFolder = "FunctionDescriptions";
        private const string ContextFolder = "Context";
        private const string IntrosFolder = "Intros";
        private const string MemosFolder = "Memos";

        private readonly IMediator _mediator;
        private readonly string _htmlStorageLocation;
        private readonly string _pdfStorageLocation;

        public MemoProgramComponentsController(IMediator mediator, IOptionsMonitor<Settings> optionsMonitor)
        {
            _mediator = mediator;
            _htmlStorageLocation = optionsMonitor.CurrentValue.HtmlStorageLocation;
            _pdfStorageLocation = optionsMonitor.CurrentValue.PdfStorageLocation;
        }

        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            /*For future use:
            id from parameter => ProgramComponent.Id
            var programComponentDto = await _mediator.Send(new GetProgramComponentByIdQuery(id));
            var simulationCombinationId = programComponentDto.SimulationCombinationId.Value;
            */

            // SimulationCombinationId for testing purposes:
            var simulationCombinationId = Guid.Parse("01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5"); 

            var basePath = Path.Combine(_htmlStorageLocation, simulationCombinationId.ToString());
            var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var functionDescription = System.IO.File.ReadAllText(Path.Combine(basePath, FunctionDescriptionsFolder, $"{language.ToUpperInvariant()}.html"));
            
            var context = Path.Combine(_pdfStorageLocation, language.ToUpperInvariant(), $"{simulationCombinationId}.pdf");

            var intro = System.IO.File.ReadAllText(Path.Combine(basePath, IntrosFolder, $"{language.ToUpperInvariant()}.html"));
            
            var memoProgramComponentDto = await _mediator.Send(new GetMemoProgramComponentBySimulationCombinationIdAndLanguageQuery(simulationCombinationId, language));
            var memos = InjectContent(memoProgramComponentDto.Memos.ToList(), simulationCombinationId, language);

            var calendarDays = memoProgramComponentDto.CalendarDays.ToList();

            var model = new MemoProgramComponent(id,intro, functionDescription, context, memos, calendarDays);

            return View(model);
        }
        
        private List<MemoDto> InjectContent(List<MemoDto> memos, Guid simulationCombinationId, string language)
        {
            foreach (var memo in memos)
            {
                var path = Path.Combine(_htmlStorageLocation, simulationCombinationId.ToString(), MemosFolder, $"{memo.OriginPosition}_{language.ToUpperInvariant()}.html");
                memo.Content = System.IO.File.ReadAllText(path);
            }

            return memos;
        }


        //FOR TESTING ONLY
        private List<MemoDto> GetTestMemoDto(Guid id)
        {
            var basePath = Path.Combine(_htmlStorageLocation, "01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5", MemosFolder);
            DirectoryInfo d = new DirectoryInfo(basePath);
            var language = "nl";

            FileInfo[] Files = d.GetFiles($"*{language.ToUpperInvariant()}.html");
            var fileInfosList = new List<MemoDto>();

            foreach (FileInfo file in Files)
            {
                var memoDto = new MemoDto();
                memoDto.Content = System.IO.File.ReadAllText(file.FullName);
                memoDto.Id = Guid.NewGuid();
                memoDto.MemoProgramId = id;
                memoDto.OriginId = Guid.NewGuid();
                var position = file.Name.Substring(0, file.Name.IndexOf("_"));
                memoDto.OriginPosition = Convert.ToInt16(position);
                memoDto.Position = Convert.ToInt16(position);

                memoDto.Title = $"{language.ToUpper()} {file.Name}";

                fileInfosList.Add(memoDto);
            }

            return fileInfosList;
        }

        private List<CalendarDayDto> GetCalendarDays()
        {
            var calendarDays = new List<CalendarDayDto>();

            List<DateTime> weekdays = new List<DateTime>();
            var startDate = new DateTime(2018, 4, 1);
            var weekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

            while ((startDate.Month == 4) && (startDate.Year == 2018))
            {
                if (weekDays.Contains(startDate.DayOfWeek))
                {
                    var calendarDay = new CalendarDayDto();
                    calendarDay.Id = Guid.NewGuid();
                    calendarDay.Day = new DateTime(startDate.Year, startDate.Month, startDate.Day);
                    calendarDays.Add(calendarDay);
                }
                startDate = startDate.AddDays(1);

            }

            return calendarDays;
        }

    }
}
