using System;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectFrameworkAgreementDetailController : ProjectController
    {
        public ActionResult Create()
        {
            var model = new CreateFrameworkAgreementModel
                {
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date.AddYears(1).AddMinutes(-1)
                };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateFrameworkAgreementModel model)
        {
            using(DurationLog.Create())
            {
                try
                {
                    if(ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<CreateFrameworkAgreementRequest>(model);
                        var frameworkAgreementId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateFrameworkAgreement(request));
                        return RedirectToAction("Edit", new { id = frameworkAgreementId });
                    }
                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult Edit(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var frameworkAgreement = this.InvokeService<IProjectManagementQueryService, FrameworkAgreementView>(service => service.RetrieveFrameworkAgreement(id));

                    var model = Mapper.DynamicMap<EditFrameworkAgreementModel>(frameworkAgreement);

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(EditFrameworkAgreementModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateFrameworkAgreementRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateFrameworkAgreement(request));

                    return RedirectToAction("Edit", new { id = model.Id });
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
