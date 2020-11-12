using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using MemoProgramComponent = Quintessence.QCandidate.Models.MemoProgramComponents.MemoProgramComponent;


namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class MemoProgramComponentsController : Controller
    {
        private const string FunctionDescriptionsFolder = "FunctionDescriptions";

        private readonly IMediator _mediator;
        private readonly string _htmlStorageLocation;

        public MemoProgramComponentsController(IMediator mediator, IOptionsMonitor<Settings> optionsMonitor)
        {
            _mediator = mediator;
            _htmlStorageLocation = optionsMonitor.CurrentValue.HtmlStorageLocation;
        }

        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            //id from parameter => ProgramComponent.Id
            var programComponentDto = await _mediator.Send(new GetProgramComponentByIdQuery(id));
            //var simulationCombinationId = programComponentDto.SimulationCombinationId.Value;


            var simulationCombinationId = Guid.Parse("01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5"); // SimulationCombinationId

            var basePath = Path.Combine(_htmlStorageLocation, simulationCombinationId.ToString());
            var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var functionDescription = System.IO.File.ReadAllText(Path.Combine(basePath, FunctionDescriptionsFolder, $"{language.ToUpperInvariant()}.html"));

            var model = new MemoProgramComponent(id,null, functionDescription, null,new List<Memo>(), new List<CalendarDay>() );

            return View(model);
        }
    }
}
