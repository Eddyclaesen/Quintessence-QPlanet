using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectProposal;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectProposalController : ProjectController
    {
        public ActionResult Index()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new IndexActionModel();
                    var availableYears = this.InvokeService<IProjectManagementQueryService, List<int>>(service => service.ListProposalYears());
                    var min = availableYears.Count > 0 ? availableYears.Min() : DateTime.Now.Year;
                    var max = availableYears.Count > 0 ? availableYears.Max() : DateTime.Now.Year;
                    var concurrentYears = new List<int>();

                    for (int i = min; i <= max; i++)
                    {
                        concurrentYears.Add(i);
                    }

                    model.ProposalYears = concurrentYears;
                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult ProposalOverview(int year)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var proposals = this.InvokeService<IProjectManagementQueryService, List<ProposalView>>(service => service.ListProposals(year));
                    
                    return PartialView(proposals);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }
    }
}
