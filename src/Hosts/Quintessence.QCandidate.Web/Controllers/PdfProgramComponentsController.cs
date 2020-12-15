using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Models.PdfProgramComponents;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class PdfProgramComponentsController : Controller
    {
        private readonly IMediator _mediator;

        public PdfProgramComponentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            //var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var language = User.Claims.SingleOrDefault(c => c.Type == "extension_Language").Value.ToLower();
            var programComponentDto = await _mediator.Send(new GetProgramComponentByIdAndLanguageQuery(id, Language.FromCode(language)));
            
            var pdfExists = await _mediator.Send(new HasSimulationCombinationPdfByIdAndLanguageQuery(programComponentDto.SimulationCombinationId.Value, language));
            
            var programComponentModel = new PdfProgramComponent(
                programComponentDto?.Description ?? programComponentDto?.Name,
                programComponentDto.Start,
                pdfExists,
                $"{Url.Action("GetPdf", "SimulationCombinations", new { simulationCombinationId = programComponentDto?.SimulationCombinationId })}#toolbar=0"
                );

            return View(programComponentModel);
        }
    }
}