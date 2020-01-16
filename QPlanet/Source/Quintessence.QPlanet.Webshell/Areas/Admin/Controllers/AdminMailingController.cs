using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.ViewModel.Inf;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminMailingController : AdminController
    {
        //
        // GET: /Admin/Mailing/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListMailTemplates()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var
                        mailTemplates =
                            this.InvokeService<IInfrastructureQueryService, List<MailTemplateView>>(
                                service => service.ListMailTemplates());
                    return PartialView(mailTemplates);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial:true);
                }
            }
        }

        public ActionResult EditMailTemplate(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var mailTemplate =
                            this.InvokeService<IInfrastructureQueryService, MailTemplateView>(
                                service => service.RetrieveMailTemplate(id));

                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(
                                service => service.ListLanguages());

                    var model = Mapper.Map<EditMailTemplateModel>(mailTemplate);
                    model.MailTemplateTranslations = model.MailTemplateTranslations.OrderBy(mtt => mtt.LanguageId).ToList();
                    model.Languages = languages;

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
        public ActionResult EditMailTemplate(EditMailTemplateModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var updateRequest = Mapper.Map<UpdateMailTemplateRequest>(model);
                        this.InvokeService<IInfrastructureCommandService>(
                            service => service.UpdateMailTemplate(updateRequest));
                    }
                    
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
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
