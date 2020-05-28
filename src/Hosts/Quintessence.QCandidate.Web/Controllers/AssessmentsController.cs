using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading.Tasks;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Models.Assessments;
using Quintessence.QCandidate.Helpers;

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
            var events = await MapProgramComponents(assessmentDto);
            var assessmentModel = new AssessmentModel(assessmentDto.Position?.Name, assessmentDto.Customer?.Name,
                assessmentDto.DayProgram?.Location?.Name, assessmentDto.DayProgram?.Date ?? date,
                events);

            return View(assessmentModel);
        }

        private async Task<List<ProgramComponentModel>> MapProgramComponents(AssessmentDto assessment)
        {
            var result = new List<ProgramComponentModel>();
            var languageClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_Language");

            //DayProgram is null when no day program was found for that day
            if(assessment.DayProgram?.ProgramComponents != null)
            {
                foreach(var programComponent in assessment.DayProgram.ProgramComponents)
                {
                    var title = programComponent.Description ?? programComponent.Name;
                    var location = programComponent.Room.Name;
                    var documentName = programComponent.SimulationCombinationId.HasValue
                        ? "PDF"
                        : null;
                    var documentLink = programComponent.SimulationCombinationId.HasValue
                        //&& c.Start.Date == DateTime.Today
                        ? await _mediator.Send(new HasSimulationCombinationPdfByIdAndLanguageQuery(programComponent.SimulationCombinationId.Value, languageClaim?.Value))
                            ? Url.Action("Details", "ProgramComponents", new {id = programComponent.Id})
                            : null
                        : null;
                    var assessors = TimeslotHelper.GetAssessorsString(programComponent.LeadAssessor, programComponent.CoAssessor);
                    var time = TimeslotHelper.GetTimeString(programComponent.Start, programComponent.End);
                    var startPixelOffset = TimeslotHelper.CalculatePixelOffset(programComponent.Start);
                    var endPixelOffset = TimeslotHelper.CalculatePixelOffset(programComponent.End);

                    var programComponentModel = new ProgramComponentModel(title, location, documentName, documentLink, assessors, time, startPixelOffset, endPixelOffset);
                    result.Add(programComponentModel);
                }
            }

            return result;
        }
    }
}