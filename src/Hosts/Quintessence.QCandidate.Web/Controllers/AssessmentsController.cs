using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class AssessmentsController : Controller
    {
        private readonly IMediator _mediator;

        public AssessmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/")]
        public async Task<IActionResult> Get()
        {
            var candidateId = new Guid("9257A17C-A805-40CA-9777-5F6067344B48");
            var date = new DateTime(2018, 10, 22);
            var assessment = await _mediator.Send(new GetAssesmentByCandidateIdAndDateQuery(candidateId, date))
                .ConfigureAwait(true);
            return View(assessment);
        }
    }
}