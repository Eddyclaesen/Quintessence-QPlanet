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

        [Route("{id}/memos")]
        [HttpPost]
        public async Task<IActionResult> ChangeMemoPosition(Guid id, List<Memo> memos)
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


        private string GetMemoContent(Guid simulationCombinationId, MemoDto memo, string language)
        {
            var file = Path.Combine(_htmlStorageLocation, simulationCombinationId.ToString(), MemosFolder, $"{memo.OriginPosition}_{language.ToUpperInvariant()}.html");
            
            return System.IO.File.ReadAllText(file);
        }

    }
}
