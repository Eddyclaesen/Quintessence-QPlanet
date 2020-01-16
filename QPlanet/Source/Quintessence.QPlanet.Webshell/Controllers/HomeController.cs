﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Dom;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Controllers
{
    [QPlanetAuthenticateController]
    public class HomeController : QPlanetControllerBase
    {
        public ActionResult Index()
        {
            var token = GetAuthenticationToken();

            var assessorsQBToday = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(2, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Today.Day)));
            var assessorsQBTomorrow = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(2, DateTime.Today.AddDays(1)));

            var assessorsQGToday = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Today.Day)));
            var assessorsQGTomorrow = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(3, DateTime.Today.AddDays(1)));

            var assessorsEXToday = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(4, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Today.Day)));
            var assessorsEXTomorrow = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(4, DateTime.Today.AddDays(1)));

            var assessorsToday = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Today.Day)));
            var assessorsTomorrow = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(1, DateTime.Today.AddDays(1)));

            assessorsToday.AddRange(assessorsQBToday);
            assessorsToday.AddRange(assessorsQGToday);
            assessorsToday.AddRange(assessorsEXToday);

            assessorsTomorrow.AddRange(assessorsQBTomorrow);
            assessorsTomorrow.AddRange(assessorsQGTomorrow);
            assessorsTomorrow.AddRange(assessorsEXTomorrow);

            bool alreadyExistsToday = assessorsToday.Any(x => x.AssessorId == token.UserId);
            bool alreadyExistsTomorrow = assessorsTomorrow.Any(x => x.AssessorId == token.UserId);

            if (alreadyExistsToday)
            {
                Session["Today"] = string.Format("https://www.qplanet.be/Candidate/ProgramDetail/GenerateDayplan/{0}/{1}/{2}/{3}/Program.pdf", token.UserId.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
            }
            else Session["Today"] = "na";

            if (alreadyExistsTomorrow)
            {
                Session["Tomorrow"] = string.Format("https://www.qplanet.be/Candidate/ProgramDetail/GenerateDayplan/{0}/{1}/{2}/{3}/Program.pdf", token.UserId.ToString(), DateTime.Today.AddDays(1).Year.ToString(), DateTime.Today.AddDays(1).Month.ToString(), DateTime.Today.AddDays(1).Day.ToString());
            }
            else Session["Tomorrow"] = "na";

            return View();
        }

        public ActionResult UserProjectCandidates()
        {
            using (DurationLog.Create())
            {
                try
                {                    
                    var request = new ListUserProjectCandidatesRequest
                    {
                        StartDate = DateTime.Now.Date,
                        EndDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1)
                    };
                    var userProjectCandidates = this.InvokeService<IProjectManagementQueryService, List<ProjectCandidateView>>(service => service.ListUserProjectCandidates(request));

                    return PartialView(userProjectCandidates);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult UserPreviousProjectCandidates()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListUserProjectCandidatesRequest
                    {
                        StartDate = DateTime.Now.Date.AddDays(-7),
                        EndDate = DateTime.Now.Date 
                    };
                    var userProjectCandidates = this.InvokeService<IProjectManagementQueryService, List<ProjectCandidateView>>(service => service.ListUserProjectCandidates(request));

                    return PartialView(userProjectCandidates);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult StickyBlogEntries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var blogEntries = this.InvokeService<IDocumentManagementQueryService, List<BlogEntryView>>(service => service.ListStickyBlogEntries());

                    return PartialView(blogEntries
                        .Where(be => be.Expires == null || be.Expires > DateTime.Now)
                        .OrderByDescending(be => be.Created));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult BlogEntries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var blogEntries = this.InvokeService<IDocumentManagementQueryService, List<BlogEntryView>>(service => service.ListBlogEntries());

                    return PartialView(blogEntries
                        .Where(be => be.Expires == null || be.Expires > DateTime.Now)
                        .OrderByDescending(be => be.Created));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }
    }
}
