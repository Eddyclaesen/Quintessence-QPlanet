using Microsoft.AspNetCore.Mvc;
using System;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class AssessmentsController : Controller
    {
        [Route("/")]
        [Route("/{candidateId}/{date}")]
        public IActionResult Get(Guid candidateId, DateTime date)
        {
            return View();
        }
    }
}