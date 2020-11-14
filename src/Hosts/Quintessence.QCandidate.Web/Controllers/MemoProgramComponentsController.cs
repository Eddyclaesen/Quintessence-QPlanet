using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Queries;
using MemoProgramComponent = Quintessence.QCandidate.Models.MemoProgramComponents.MemoProgramComponent;
using Quintessence.QCandidate.Core.Domain;
using Memo = Quintessence.QCandidate.Models.MemoProgramComponents.Memo;
using CalendarDay = Quintessence.QCandidate.Models.MemoProgramComponents.CalendarDay;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class MemoProgramComponentsController : Controller
    {
        private const string FunctionDescriptionsFolder = "FunctionDescriptions";
        private const string IntrosFolder = "Intros";
        private const string MemosFolder = "Memos";

        private readonly IMediator _mediator;
        private readonly string _htmlStorageLocation;

        public MemoProgramComponentsController(IMediator mediator, IOptionsMonitor<Settings> optionsMonitor)
        {
            _mediator = mediator;
            _htmlStorageLocation = optionsMonitor.CurrentValue.HtmlStorageLocation;
        }

        [HttpGet]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;

            var memoProgramComponentDto = await _mediator.Send(new GetMemoProgramComponentByIdAndLanguageQuery(id, Language.FromCode(language)));

            var basePath = Path.Combine(_htmlStorageLocation, memoProgramComponentDto.SimulationCombinationId.ToString());

            var intro = System.IO.File.ReadAllText(Path.Combine(basePath, IntrosFolder, $"{language.ToUpperInvariant()}.html"));
            var functionDescription = System.IO.File.ReadAllText(Path.Combine(basePath, FunctionDescriptionsFolder, $"{language.ToUpperInvariant()}.html"));

            var contextId = Guid.Parse("5fa70b90-32d6-48fb-993c-0191d79da1c9");
 
            var model = new MemoProgramComponent(
                id, 
                intro, 
                functionDescription, 
                contextId, 
                memoProgramComponentDto.Memos.Select(m => new Memo(m.Id, m.Position, m.Title, GetMemoContent(memoProgramComponentDto.SimulationCombinationId, m, language))), 
                memoProgramComponentDto.CalendarDays.Select(cd => new CalendarDay(cd.Id, cd.Day, cd.Note))
                );


            return View(model);
        }
        private string GetMemoContent(Guid simulationCombinationId, MemoDto memo, string language)
        {
            var file = Path.Combine(_htmlStorageLocation, simulationCombinationId.ToString(), MemosFolder, $"{memo.OriginPosition}_{language.ToUpperInvariant()}.html");
            
            return System.IO.File.ReadAllText(file);
        }

    }
}
