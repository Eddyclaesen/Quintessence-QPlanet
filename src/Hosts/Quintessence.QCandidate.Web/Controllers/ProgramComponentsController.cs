using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Models;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class ProgramComponentsController : Controller
    {
        private readonly IMediator _mediator;

        public ProgramComponentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var programComponentDto = await _mediator.Send(new GetProgramComponentByIdQuery(id));
            var programComponentModel = new ProgramComponent
            {
                Title = programComponentDto?.Description ?? programComponentDto?.Name,
                CanShowPdf = programComponentDto.Start.Date == DateTime.Now.Date,
                PdfUrl = $"{Url.Action("GetPdf", "SimulationCombinations", new { simulationCombinationId = programComponentDto?.SimulationCombinationId })}#toolbar=0"
            };

            return View(programComponentModel);
        }
    }
}