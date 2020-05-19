using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Quintessence.QCandidate.Controllers
{
    [Route("[Controller]")]
    public class SimulationCombinationsController : Controller
    {
        private readonly string _pdfStorageLocation;

        public SimulationCombinationsController(IConfiguration configuration)
        {
            _pdfStorageLocation = configuration.GetValue<string>("PdfStorageLocation");
        }

        [Route("{action}/{simulationCombinationId}/{language}")]
        public ActionResult GetPdf(Guid simulationCombinationId, string language)
        {
            var filename = GetPdfFileLocation(simulationCombinationId, language);

            if(System.IO.File.Exists(filename))
            {
                using(var fileStream = new FileStream(filename, FileMode.Open))
                {
                    return new FileStreamResult(fileStream, "application/pdf");
                }
            }

            return new EmptyResult();
        }

        private string GetPdfFileLocation(Guid simulationCombinationId, string language)
        {
            var filename = $"{simulationCombinationId}.pdf";

            return Path.Combine($"{_pdfStorageLocation}_{language}", filename);
        }
    }
}