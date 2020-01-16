using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Sim;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QPlanet.Webshell.Areas.Workspace.Controllers
{
    public class WorkspaceHomeController : QPlanetControllerBase
    {
        public ActionResult Index()
        {
            using (DurationLog.Create())
            {
                try
                {
                    return View();
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult UserProfile()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());
                    var profile = this.InvokeService<IWorkspaceManagementQueryService, UserProfileView>(service => service.RetrieveUserProfile(GetAuthenticationToken().UserId));

                    var model = Mapper.DynamicMap<EditUserProfileModel>(profile);
                    model.Languages = languages;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        [HttpPost]
        public ActionResult UserProfile(EditUserProfileModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateUserProfileRequest>(model);
                    this.InvokeService<IWorkspaceManagementCommandService>(service => service.UpdateUserProfile(request));
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
