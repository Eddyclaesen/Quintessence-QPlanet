using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Rep;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminReport;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminReportController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region CandidateReportDefinitions

        public ActionResult CandidateReportDefinitions()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var definitions = this.InvokeService<IReportManagementQueryService, List<CandidateReportDefinitionView>>(service => service.ListCandidateReportDefinitions());

                    var model = new CandidateReportDefinitionsActionModel();

                    model.Contacts = definitions
                        .Where(d => d.ContactId.HasValue)
                        .GroupBy(d => new { d.ContactId, d.ContactFullName })
                        .Select(s => new KeyValuePair<int, string>(s.Key.ContactId.Value, s.Key.ContactFullName))
                        .ToDictionary(x => x.Key, y => y.Value);
                    model.Definitions = definitions;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult AddCandidateReportDefinition(int? id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateNewCandidateReportDefinitionModel();
                    if (id.HasValue)
                    {
                        var contact = this.InvokeService<ICustomerRelationshipManagementQueryService, CrmContactView>(service => service.RetrieveContactDetailInformation(id.Value));
                        model.ContactId = contact.Id;
                        model.ContactName = contact.FullName;
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

        [HttpPost]
        public ActionResult AddCandidateReportDefinition(CreateNewCandidateReportDefinitionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewCandidateReportDefinitionRequest>(model);
                    this.InvokeService<IReportManagementCommandService>(service => service.CreateNewCandidateReportDefinition(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditCandidateReportDefinition(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var definition = this.InvokeService<IReportManagementQueryService, CandidateReportDefinitionView>(service => service.RetrieveCandidateReportDefinition(id));

                    var model = Mapper.DynamicMap<EditCandidateReportDefinitionModel>(definition);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditCandidateReportDefinition(EditCandidateReportDefinitionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateCandidateReportDefinitionRequest>(model);
                    this.InvokeService<IReportManagementCommandService>(service => service.UpdateCandidateReportDefinition(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult DeleteCandidateReportDefinition(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IReportManagementCommandService>(service => service.DeleteCandidateReportDefinition(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult AddCandidateReportDefinitionField(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateNewCandidateReportDefinitionFieldModel { CandidateReportDefinitionId = id };
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddCandidateReportDefinitionField(CreateNewCandidateReportDefinitionFieldModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewCandidateReportDefinitionFieldRequest>(model);
                    this.InvokeService<IReportManagementCommandService>(service => service.CreateNewCandidateReportDefinitionField(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditCandidateReportDefinitionField(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var field = this.InvokeService<IReportManagementQueryService, CandidateReportDefinitionFieldView>(service => service.RetrieveCandidateReportDefinitionField(id));

                    var model = Mapper.DynamicMap<EditCandidateReportDefinitionFieldModel>(field);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditCandidateReportDefinitionField(EditCandidateReportDefinitionFieldModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateCandidateReportDefinitionFieldRequest>(model);
                    this.InvokeService<IReportManagementCommandService>(service => service.UpdateCandidateReportDefinitionField(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult DeleteCandidateReportDefinitionField(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IReportManagementCommandService>(service => service.DeleteCandidateReportDefinitionField(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        #endregion

        #region ReportDefinitions

        public ActionResult ReportDefinitions()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var definitions = this.InvokeService<IReportManagementQueryService, List<ReportDefinitionView>>(service => service.ListReportDefinitions(new ListReportDefinitionsRequest()));
                    var reportTypes = this.InvokeService<IReportManagementQueryService, List<ReportTypeView>>(service => service.ListReportTypes());

                    var model = new ReportDefinitionsActionModel();

                    model.ReportTypes = reportTypes;
                    model.Definitions = definitions.OrderBy(d => d.ReportTypeId).Select(Mapper.DynamicMap<EditReportDefinitionModel>).ToList();

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult ReportDefinitions(ReportDefinitionsActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = model.Definitions.Select(Mapper.DynamicMap<UpdateReportDefinitionRequest>).ToList();

                    this.InvokeService<IReportManagementCommandService>(service => service.UpdateReportDefinitions(requests));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult AddReportDefinition()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateNewReportDefinitionModel();

                    model.ReportTypes = this.InvokeService<IReportManagementQueryService, List<ReportTypeView>>(service => service.ListReportTypes());

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddReportDefinition(CreateNewReportDefinitionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewReportDefinitionRequest>(model);

                    this.InvokeService<IReportManagementCommandService>(service => service.CreateNewReportDefinition(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        #endregion

        public ActionResult DeleteReportDefinition(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IReportManagementCommandService>(service => service.DeleteReportDefinition(id));
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
