using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class MemoProgramComponentsController : Controller
    {
        private readonly IMediator _mediator;

        public MemoProgramComponentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;

            var model = await _mediator.Send(new GetMemoProgramComponentByIdAndLanguageQuery(id, Language.FromCode(language)));

            return View(model);
        }
    }
}
