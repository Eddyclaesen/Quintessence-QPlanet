using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Models.NeoProgramComponents;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class NeoProgramComponentsController : Controller
    {
        private readonly IMediator _mediator;

        public NeoProgramComponentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateId = new Guid(candidateIdClaim.Value);
            var language = User.Claims.SingleOrDefault(c => c.Type == "extension_Language").Value.ToLower();

            var programComponentDto = await _mediator.Send(new GetProgramComponentByIdAndLanguageQuery(id, Language.FromCode(language)));

            var pdfExists = await _mediator.Send(new HasNeoPdfByCandidateIdQuery(candidateId));

            var programComponentModel = new NeoProgamComponent(
               programComponentDto?.Description ?? programComponentDto?.Name,
               programComponentDto.Start,
               pdfExists,
               $"{Url.Action("GetPdf", "NeoProgramComponents", new { candidateId = candidateId })}#toolbar=0"
               );

            return View(programComponentModel);
        }

        [Route("{action}/{candidateId}")]
        public async Task<ActionResult> GetPdf(Guid candidateId)
        {
            //var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var language = User.Claims.SingleOrDefault(c => c.Type == "extension_Language").Value.ToLower();
            var fileStream = await _mediator.Send(new GetNeoPdfByCandidateIdQuery(candidateId));

            return fileStream != null
                ? (ActionResult)new FileStreamResult(fileStream, "application/pdf")
                : new EmptyResult();
        }
    }
}
