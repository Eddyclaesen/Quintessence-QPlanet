using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Core.Queries;
using System;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class SimulationCombinationsController : Controller
    {
        private readonly IMediator _mediator;

        public SimulationCombinationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{action}/{simulationCombinationId}")]
        public async Task<ActionResult> GetPdf(Guid simulationCombinationId)
        {
            //TODO: Determine language
            var language = "NL";
            var fileStream = await _mediator.Send(new GetSimulationCombinationPdfByIdAndLanguageQuery(simulationCombinationId, language));

            return fileStream != null
                ? (ActionResult) new FileStreamResult(fileStream, "application/pdf")
                : new EmptyResult();
        }
    }
}