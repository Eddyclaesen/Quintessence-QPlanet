using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectFrameworkAgreementController : ProjectController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FrameworkAgreements(int year)
        {
            using(DurationLog.Create())
            {
                try
                {
                    var model =
                        this.InvokeService<IProjectManagementQueryService, List<FrameworkAgreementView>>(
                            service => service.ListFrameworkAgreementsByYear(year));
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ListFrameworkAgreementYears()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var years =
                        this.InvokeService<IProjectManagementQueryService, List<int>>(
                            service => service.ListFrameworkAgreementYears());
                    var min = years.Count > 0 ? years.Min() : DateTime.Now.Year;
                    var max = years.Count > 0 ? years.Max() : DateTime.Now.Year;
                    var concurrentYears = new List<int>();
                    for (int i = min; i <= max; i++)
                    {
                        concurrentYears.Add(i);
                    }

                    concurrentYears = concurrentYears.OrderBy(i => i).ToList();
                    return Json(concurrentYears, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }

            }
        }
    }
}
