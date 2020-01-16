using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Models.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectHomeController : ProjectController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecentProjects(DataTableParameterModel parameters)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListProjectsRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                    var response = this.InvokeService<IProjectManagementQueryService, ListProjectsResponse>(service => service.ListRecentProjects(request));
                    var projects = response.Projects;

                    var model = new
                    {
                        sEcho = parameters.sEcho,
                        iTotalRecords = response.DataTablePaging.TotalRecords,
                        iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                        aaData = projects
                                    .Select(p => new[] { p.Name, p.ProjectType.Name, p.Contact != null ?p.Contact.FullName :"", ((ProjectStatusCodeType)p.StatusCode).ToString(), p.Audit.CreatedOn.ToString("dd/MM/yyyy"), p.Id.ToString() })
                    };

                    return Json(model, JsonRequestBehavior.AllowGet);
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
