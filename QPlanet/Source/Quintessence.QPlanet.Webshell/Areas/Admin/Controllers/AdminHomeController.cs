using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.ObjectBuilder2;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Rep;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminHome;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Translations()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var reportParameters =
                        this.InvokeService<IReportManagementQueryService, List<ReportParameterView>>(
                            service => service.ListReportParameters());

                    return PartialView(reportParameters);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddTranslation()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    var model = new CreateNewReportParameterModel();
                    model.Values = languages.Select(l => new CreateNewReportParameterValueModel { LanguageId = l.Id, LanguageName = l.Name }).ToList();

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddTranslation(CreateNewReportParameterModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<CreateNewReportParameterRequest>(model);
                    this.InvokeService<IReportManagementCommandService>(service => service.CreateNewReportParameter(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditTranslation(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());
                    var reportParameter = this.InvokeService<IReportManagementQueryService, ReportParameterView>(service => service.RetrieveReportParameter(id));

                    var languageDictionary = languages.ToDictionary(l => l.Id, l => l.Name);
                    
                    var model = Mapper.Map<EditReportParameterModel>(reportParameter);
                    model.ReportParameterValues
                        .Where(rpv => languageDictionary.ContainsKey(rpv.LanguageId))
                        .ForEach(rpv => rpv.LanguageName = languageDictionary[rpv.LanguageId]);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditTranslation(EditReportParameterModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateReportParameterRequest>(model);
                    this.InvokeService<IReportManagementCommandService>(service => service.UpdateReportParameter(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteTranslation(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IReportManagementCommandService>(service => service.DeleteReportParameter(id));

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
