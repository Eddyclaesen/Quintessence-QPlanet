using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quintessence.QCandidate.Core.Queries;

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
            var programComponent = await _mediator.Send(new GetProgramComponentByIdQuery(id));

            ViewBag.CanShowPdf = true; /*(programComponent != null
                                     && programComponent.Start.Date == DateTime.Today);*/

            return View(programComponent);
        }
    }
}