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
            var candidateId = new Guid("CB2ABA03-A397-4DB9-829D-495F26D5E42B");
            var date = new DateTime(2018, 11, 20);
            var assessment = await _mediator.Send(new GetAssesmentByCandidateIdAndDateQuery(candidateId, date))
                .ConfigureAwait(true);
            return View(assessment);
        }
    }
}