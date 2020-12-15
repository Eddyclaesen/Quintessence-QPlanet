using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Core.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class ContextsController : Controller
    {
        private readonly IMediator _mediator;

        public ContextsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{action}/{contextId}")]
        public async Task<ActionResult> GetPdf(Guid contextId)
        {
            //var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var language = User.Claims.SingleOrDefault(c => c.Type == "extension_Language").Value.ToLower();
            var fileStream = await _mediator.Send(new GetContextPdfByIdAndLanguageQuery(contextId, language));

            return fileStream != null
                ? (ActionResult)new FileStreamResult(fileStream, "application/pdf")
                : new EmptyResult();
        }
    }
}