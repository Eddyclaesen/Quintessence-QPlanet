using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;
using Quintessence.QCandidate.Models.MemoProgramComponents;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class MemoProgramComponentsController : Controller
    {
        private const string FunctionDescriptionsFolder = "FunctionDescriptions";

        private readonly IMediator _mediator;
        private readonly string _htmlStorageLocation;

        public MemoProgramComponentsController(IMediator mediator, IOptionsMonitor<Settings> optionsMonitor)
        {
            _mediator = mediator;
            _htmlStorageLocation = optionsMonitor.CurrentValue.HtmlStorageLocation;
        }

        [Route("{action}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            id = Guid.Parse("01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5");

            var basePath = Path.Combine(_htmlStorageLocation, id.ToString());
            var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var functionDescription = System.IO.File.ReadAllText(Path.Combine(basePath, FunctionDescriptionsFolder, $"{language.ToUpperInvariant()}.html"));

            var model = new MemoProgramComponent(id, functionDescription);

            return View(model);
        }
    }
}
