using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Areas.Workspace.Models.WorkspaceTimesheet;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Workspace.Controllers
{
    public class WorkspaceTimesheetController : QPlanetControllerBase
    {
        public ActionResult Index()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var user = GetAuthenticationToken().User;

                    var model = new IndexActionModel();
                    model.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
                    model.Users = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListUsers()).OrderBy(u => u.FullName).ToList();
                    model.UserId = user.Id;

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult Timesheets(int year, int month, Guid userId, bool isProjectManager)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new TimesheetsActionModel();

                    model.UserId = userId;
                    model.RegisteredTimesheets = this.InvokeService<IProjectManagementQueryService, List<TimesheetEntryView>>(service => service.ListUserTimesheets(year, month, userId, isProjectManager));

                    var listTimesheetUnregisteredEntriesRequest = new ListTimesheetUnregisteredEntriesRequest { MonthDate = new DateTime(year, month, 1), UserId = userId, IsProjectManager = isProjectManager };
                    model.UnregisteredTimesheets = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmTimesheetUnregisteredEntryView>>(service => service.ListTimesheetUnregisteredEntries(listTimesheetUnregisteredEntriesRequest));

                    if (isProjectManager)
                    {
                        model.IsProjectManager = true;
                        var unregisteredProjects = model.UnregisteredTimesheets.Select(x => x.Project).ToList();
                        var registeredProjects = model.RegisteredTimesheets.Select(x => x.Project).ToList();
                        var allProjects = unregisteredProjects.Union(registeredProjects);
                        var distinctProjects = allProjects.Distinct();
                        model.PromaProjects = distinctProjects.ToList();
                        model.Year = year;
                        model.Month = month;

                    }

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }
    }
}