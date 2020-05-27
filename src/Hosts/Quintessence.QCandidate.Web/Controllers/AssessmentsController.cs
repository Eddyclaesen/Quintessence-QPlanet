using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading.Tasks;
using Quintessence.QCandidate.Models;

namespace Quintessence.QCandidate.Controllers
{
    public class AssessmentsController : Controller
    {
        private readonly IMediator _mediator;

        public AssessmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Get()
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            Guid.TryParse(candidateIdClaim?.Value, out var candidateId);

            //TODO: Use current date
            var date = new DateTime(2018, 10, 22);
            var assessmentDto = await _mediator.Send(new GetAssessmentByCandidateIdAndDateQuery(candidateId, date));
            var assessmentModel = new AssessmentModel(_mediator, Url, assessmentDto);

            return View(assessmentModel);
        }
    }
}