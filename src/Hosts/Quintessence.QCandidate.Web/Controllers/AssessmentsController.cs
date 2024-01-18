using Dapper;
using Kenze.Domain;
using Kenze.Infrastructure.Dapper;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Commands;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Models.Assessments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Controllers
{
    public class AssessmentsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AssessmentsController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _configuration = config;
        }

        public IConfigurationRoot Configuration { get; }

        public async Task<IActionResult> Get()
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateLanguage = User.Claims.SingleOrDefault(c => c.Type == "extension_Language").Value.ToLower();
            var culturedlanguage = CultureInfo.CurrentCulture.ToString();
            var candidateId = new Guid(candidateIdClaim.Value);

            var assessmentDto = await _mediator.Send(new GetAssessmentByCandidateIdAndDateAndLanguageQuery(candidateId, DateTime.Now, candidateLanguage));           

            Assessment assessment = null;
            if (assessmentDto != null)
            {
                TempData["Location"] = assessmentDto.DayProgram.Location.Name;
                TempData.Keep("Location");
                var programComponents = await MapProgramComponents(assessmentDto);
                assessment = new Assessment(assessmentDto.Position.Name, assessmentDto.Customer.Name,
                    assessmentDto.DayProgram.Location.Name, assessmentDto.DayProgram.Date,
                    programComponents);
            }

            return View(assessment);
        }

        private async Task<List<ProgramComponent>> MapProgramComponents(AssessmentDto assessment)
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateId = new Guid(candidateIdClaim.Value);

            var result = new List<ProgramComponent>();
            //var language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
            var language = User.Claims.SingleOrDefault(c => c.Type == "extension_Language").Value.ToLower();

            //DayProgram is null when no day program was found for that day
            if (assessment.DayProgram?.ProgramComponents != null)
            {
                bool lunch = assessment.DayProgram.ProgramComponents.Any(item => (item.Description ?? item.Name).Contains("Lunch"));

                foreach (var programComponent in assessment.DayProgram.ProgramComponents.OrderBy(pc => pc.Start))
                {

                    var title = programComponent.Name ?? programComponent.Description;
                    var description = programComponent.Description;
                    var location = programComponent.Room.Name;

                    var assessors = GetAssessorsString(programComponent.LeadAssessor, programComponent.CoAssessor);
                    QCandidateLayout qCandidateLayout = Enumeration.FromId<QCandidateLayout>(programComponent.QCandidateLayoutId);

                    var showDetailsLink = qCandidateLayout != QCandidateLayout.None && programComponent.Start.Date == DateTime.Now.Date;

                    if (qCandidateLayout == QCandidateLayout.Pdf)
                    {
                        showDetailsLink = await _mediator.Send(new HasSimulationCombinationPdfByIdAndLanguageQuery(programComponent.SimulationCombinationId.Value, language));
                    }              
                    
                    if (title.Contains("Debriefing"))
                    {
                        showDetailsLink = await _mediator.Send(new HasNeoPdfByCandidateIdQuery(candidateId));
                    }

                    if (title.Contains("Intro") && lunch)
                    {
                        showDetailsLink = true;
                    }

                    if (description != null && description.Contains("http"))
                    {
                        showDetailsLink = true;
                    }

                    var programComponentModel = new ProgramComponent(programComponent.Id, title, description, location, showDetailsLink, assessors, programComponent.Start, programComponent.End, qCandidateLayout);
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

        public async Task<ActionResult> GetAllAssessments()
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateId = new Guid(candidateIdClaim.Value);

            var allAssessments = await _mediator.Send(new GetAllAssessmentsByCandidateIdQuery(candidateId));
         
            return PartialView(allAssessments);
        }

        public async Task<ActionResult> GetProject(Guid projectId)
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateId = new Guid(candidateIdClaim.Value);

            var project = await _mediator.Send(new GetProjectByCandidateIdAndProjectIdQuery(candidateId, projectId));

            return View(project);
        }

        public async Task<ActionResult> GetSubCategories(Guid projectId)
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateId = new Guid(candidateIdClaim.Value);

            var subCategories = await _mediator.Send(new GetSubCategoriesByCandidateIdAndProjectIdQuery(candidateId, projectId));

            return PartialView(subCategories);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCheckbox(bool check, Guid projectId)
        {
            var candidateIdClaim = User.Claims.SingleOrDefault(c => c.Type == "extension_QPlanet_CandidateId");
            var candidateId = new Guid(candidateIdClaim.Value);

            var connectionString = _configuration.GetConnectionString("QPlanet");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var consent = new SqlParameter("check", SqlDbType.Bit) { Value = check };
                var candidateid = new SqlParameter("candidateid", SqlDbType.UniqueIdentifier) { Value = candidateId };
                var projectid = new SqlParameter("projectid", SqlDbType.UniqueIdentifier) { Value = projectId };

                var parameters = new SqlParameter[] { consent, candidateid, projectid };
                var command = new StoredProcedureCommandDefinition("[QCandidate].[SetConsentByCandidateIdAndProjectId]", parameters).ToCommandDefinition();

                var result = await connection.ExecuteAsync(command);
            }

            return Ok();
        }
    }
}