using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quintessence.QCandidate.Core.Queries;
using System;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class SimulationCombinationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly string _pdfStorageLocation;

        public SimulationCombinationsController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _pdfStorageLocation = configuration.GetValue<string>("PdfStorageLocation");
        }

        [Route("{action}/{simulationCombinationId}/{language}")]
        public async Task<ActionResult> GetPdf(Guid? simulationCombinationId, string language)
        {
            if(simulationCombinationId.HasValue)
            {
                var fileStream = await _mediator.Send(new GetSimulationCombinationPdfByIdAndLanguageQuery(_pdfStorageLocation, simulationCombinationId.Value, language));

                if(fileStream != null)
                {
                    return new FileStreamResult(fileStream, "application/pdf");
                }
            }

            return new EmptyResult();
        }
    }
}