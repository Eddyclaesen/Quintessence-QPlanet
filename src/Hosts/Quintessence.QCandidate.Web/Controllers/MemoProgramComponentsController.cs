using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quintessence.QCandidate.Core.Commands;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Core.Domain;
using System.Linq;

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
            var language = Language.FromCode(HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name);

            var model = await _mediator.Send(new GetMemoProgramComponentByIdAndLanguageQuery(id, language));

            if (model == null)
            {
                var programComponent = await _mediator.Send(new GetProgramComponentByIdAndLanguageQuery(id, language));
                if (programComponent.Start > DateTime.Now || programComponent.Start.Date != DateTime.Now.Date)
                {
                    ViewBag.Name = programComponent.Name;
                    return View("NoAccessYet");
                }
                
                var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
                var candidateId = new Guid(candidateIdClaim.Value);
                await _mediator.Send(new CreateMemoProgramComponentCommand(id, candidateId, programComponent.SimulationCombinationId.Value));

                model = await _mediator.Send(new GetMemoProgramComponentByIdAndLanguageQuery(id, language));
            }

            return View(model);
        }

        [Route("{id}/memos")]
        [HttpPost]
        public async Task<IActionResult> ChangeMemoPosition(Guid id, [FromBody]List<Models.MemoProgramComponents.Memo> memos)
        {
            var dict = new Dictionary<Guid, int>();
            foreach (var memo in memos)
            {
                dict.Add(memo.Id, memo.Position);
            }
            var memosToUpdate = new ChangeMemosPositionCommand(id, dict);
            await _mediator.Send(memosToUpdate);
            return Ok();
        }

        [Route("{id}/calendarDays/{calendarDayId}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCalendarDay(Guid id, Guid calendarDayId, string note)
        {
            var memo = new UpdateCalendarDayCommand(id, calendarDayId, note);
            await _mediator.Send(memo);
            return Ok();
        }

    }
}
