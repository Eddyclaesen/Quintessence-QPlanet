using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading.Tasks;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Models.Assessments;
using System.Globalization;

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
            var candidateId = new Guid(candidateIdClaim.Value);
            var assessmentDto = await _mediator.Send(new GetAssessmentByCandidateIdAndDateAndLanguageQuery(candidateId, DateTime.Now, CultureInfo.CurrentCulture.ToString()));

            Assessment assessment = null;
            if (assessmentDto != null)
            {
                var programComponents = await MapProgramComponents(assessmentDto);
                assessment = new Assessment(assessmentDto.Position.Name, assessmentDto.Customer.Name,
                    assessmentDto.DayProgram.Location.Name, assessmentDto.DayProgram.Date,
                    programComponents);
            }

            return View(assessment);
        }

        private async Task<List<ProgramComponent>> MapProgramComponents(AssessmentDto assessment)
        {
            var result = new List<ProgramComponent>();

            //DayProgram is null when no day program was found for that day
            if (assessment.DayProgram?.ProgramComponents != null)
            {
                foreach(var programComponent in assessment.DayProgram.ProgramComponents)
                {
                    var title = programComponent.Description ?? programComponent.Name;
                    var location = programComponent.Room.Name;
                       
                    var assessors = GetAssessorsString(programComponent.LeadAssessor, programComponent.CoAssessor);

                    var programComponentModel = new ProgramComponent(programComponent.Id, title, location, false, assessors, programComponent.Start, programComponent.End, programComponent.QCandidateLayoutId);
                    result.Add(programComponentModel);
                }
            }

            return result;
        }

        private static string GetAssessorsString(UserDto leadAssessor, UserDto coAssessor)
        {
            var assessors = new[] { GetAssessorString(leadAssessor), GetAssessorString(coAssessor) };

            return string.Join("/ ", assessors.Where(a => a != null));
        }

        private static string GetAssessorString(UserDto user)
        {
            if (string.IsNullOrWhiteSpace(user?.FirstName)
               && string.IsNullOrWhiteSpace(user?.LastName))
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}".Trim();
        }
    }
}