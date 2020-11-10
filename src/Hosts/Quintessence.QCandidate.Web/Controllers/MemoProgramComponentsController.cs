using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quintessence.QCandidate.Core.Commands;
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

        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {

            return View();
        }

        //create memo programcomponent
        [Route("Create")]
        public async Task<IActionResult> CreateProgramComponent()
        {
            var memos = new List<Memo>();
            var calendarDays = new List<CalendarDay>();

            var creatememo = new CreateMemoProgramComponentCommand(new Guid("68CF2F62-BC82-408F-A752-E1FD901CB52B"), new Guid("7E6A3147-E23F-487D-AD4E-8608C199EF07"), memos, calendarDays);

            await _mediator.Send(creatememo);

            return null;
        }

        //update memo position
        [Route("Change")]
        public async Task<IActionResult> ChangeMemoPosition()
        {

            var dict = new Dictionary<Guid, int>();

            dict.Add(new Guid("356335EA-02BE-484E-BD90-E570DC36FA1D"), 10);
            dict.Add(new Guid("B398AA3E-76A5-4FF4-88DB-28AB306E105F"), 11);
            dict.Add(new Guid("359A3295-F1A5-41B2-89AD-6CA611604100"), 12);
            dict.Add(new Guid("F76CD772-ABC5-426E-B518-9B397A46A09E"), 13);
            dict.Add(new Guid("D0FCA5F5-6376-46BF-93CA-74E37A306199"), 14);

            var memo = new ChangeMemosOrderCommand(new Guid("1DDA8169-E8BD-490C-8990-77703DB22654"), dict);

            await _mediator.Send(memo);

            return null;

        }



        //update text calendarday
        [Route("Update")]
        public async Task<IActionResult> UpdateCalendarDay()
        {
            var memo = new UpdateCalendarDayCommand(new Guid("1DDA8169-E8BD-490C-8990-77703DB22654"), new Guid("6674FE8E-043F-4500-8633-DB2051BDCEEA"), "Test via MediatR");

            await _mediator.Send(memo);

            return null;
        }
    }
}
