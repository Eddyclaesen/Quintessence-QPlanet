using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminJobController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JobDefinitions()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var jobdefinitions = this.InvokeService<IInfrastructureQueryService, List<JobDefinitionView>>(service => service.ListJobDefinitions());
                    
                    return PartialView(jobdefinitions);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult ScheduleJob(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IInfrastructureCommandService>(service => service.ScheduleJob(id));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
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
