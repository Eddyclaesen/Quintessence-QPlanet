using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.Infrastructure.Validation;
using Quintessence.QPlanet.ViewModel.Fin;
using Quintessence.QPlanet.Webshell.Areas.Finance.Models.FinanceHome;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Infrastructure.ModelBinders;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Fin;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QPlanet.Infrastructure.Logging;

namespace Quintessence.QPlanet.Webshell.Areas.Finance.Controllers
{
    public class FinanceHomeController : FinanceController
    {
        public ActionResult Index()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var user = GetAuthenticationToken().User;

                    var model = new IndexActionModel();
                    if (user.RoleCode == "CUSTA") //Show invoicing for KA
                    {
                        model.CustomerAssistantId = user.Id;
                        model.CustomerAssistants =
                            this.InvokeService<ISecurityQueryService, List<UserView>>(
                                service => service.ListCustomerAssistants());
                        model.Date = DateTime.Now;
                    }
                    else if (user.RoleCode == "ACCOUNT") //Show invoicing for accounting dept.
                    {
                        model.Date = DateTime.Now;
                    }
                    else //Proma or Admin
                    {
                        model.ProjectManagers = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListUsers()).OrderBy(u => u.FullName).ToList();
                        model.ProjectManagerId = user.Id;
                        model.Date = DateTime.Now;
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

        [HttpPost]
        public ActionResult FilterCustomerAssistantInvoicing(FilterCustomerAssistantInvoicingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    model.Date = model.Date.SetTime(23, 59, 59);
                    var request = Mapper.DynamicMap<ListCustomerAssistantInvoicingRequest>(model);
                    var entries =
                        this.InvokeService<IFinanceManagementQueryService, List<InvoicingBaseEntryView>>(
                            service =>
                            service.ListCustomerAssistantInvoicingEntries(request));

                    var invoicingEntries = CastInvoicingViewsToEditModels(entries);

                    var viewModel = new ListCustomerAssistantInvoicingModel
                        {
                            InvoicingEntries = invoicingEntries.OrderBy(e => e.InvoiceStatusCode).ToList()
                        };
                    return PartialView("ListCustomerAssistantInvoicingEntries", viewModel);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult FilterProjectManagerInvoicing(FilterProjectManagerInvoicingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    model.Date = model.Date.SetTime(23, 59, 59);
                    var request = Mapper.DynamicMap<ListProjectManagerInvoicingRequest>(model);
                    var entries =
                        this.InvokeService<IFinanceManagementQueryService, List<InvoicingBaseEntryView>>(
                            service =>
                            service.ListProjectManagerInvoicingEntries(request));

                    var invoicingEntries = CastInvoicingViewsToEditModels(entries);

                    var viewModel = new ListProjectManagerInvoicingModel
                    {
                        InvoicingEntries = invoicingEntries.OrderBy(e => e.InvoiceStatusCode).ToList()
                    };
                    return PartialView("ListProjectManagerInvoicingEntries", viewModel);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult FilterAccountantInvoicing(FilterAccountantInvoicingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    model.Date = model.Date.SetTime(23, 59, 59);
                    var request = Mapper.DynamicMap<ListAccountantInvoicingRequest>(model);
                    var entries =
                        this.InvokeService<IFinanceManagementQueryService, List<AccountantInvoicingBaseEntryView>>(
                            service =>
                            service.ListAccountantInvoicingEntries(request));

                    var invoicingEntries = CastAccountantInvoicingViewsToEditModels(entries);

                    var groupedEntries = invoicingEntries.OrderBy(e => e.InvoiceStatusCode).GroupBy(ie => ie.CrmProjectName);

                    var groupedEntryModels = groupedEntries.Select(groupedEntry => new GroupedAccountantInvoicingEntryModel()
                        {
                            CrmProjectName = groupedEntry.Key,
                            InvoicingEntries = groupedEntry.ToList()
                        }).OrderBy(ge => ge.CrmProjectName).ToList();

                    var viewModel = new ListAccountantInvoicingModel
                    {
                        GroupedInvoicingEntries = groupedEntryModels
                    };
                    return PartialView("ListAccountantInvoicingEntries", viewModel);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult SaveCustomerAssistantInvoicingEntries(ListCustomerAssistantInvoicingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = CastInvoicingEditModelsToUpdateCustomerAssistantRequests(model.InvoicingEntries.Where(ie => ie.IsDiry));
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateCustomerAssistantInvoicingEntries(requests));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult SaveProjectManagerInvoicingEntries(ListProjectManagerInvoicingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = CastInvoicingEditModelsToUpdateProjectManagerRequests(model.InvoicingEntries.Where(ie => ie.IsDiry));
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectManagerInvoicingEntries(requests));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult SaveAccountantInvoicingEntry([ModelBinder(typeof(EditAccountantInvoicingEntryModelModelBinder))]EditAccountantInvoicingBaseEntryModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //Do casting to appropriate request.
                    //project candidate
                    var projectCandidateInvoicingEntry = model as EditAccountantProjectCandidateInvoicingEntryModel;
                    if (projectCandidateInvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantProjectCandidateInvoicingRequest>(projectCandidateInvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantProjectCandidateInvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingProjectCandidateEntryView>(
                            service => service.RetrieveAccountantProjectCandidateInvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantProjectCandidateInvoicingModel", updatedModel);
                    }

                    //project candidate category type 1
                    var projectCandidateCategoryType1InvoicingEntry = model as EditAccountantProjectCandidateCategoryType1InvoicingEntryModel;
                    if (projectCandidateCategoryType1InvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantProjectCandidateCategoryType1InvoicingRequest>(projectCandidateCategoryType1InvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantProjectCandidateCategoryType1InvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingProjectCandidateCategoryType1EntryView>(
                            service => service.RetrieveAccountantProjectCandidateCategoryType1InvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantProjectCandidateCategoryType1InvoicingModel", updatedModel);
                    }

                    //project candidate category type 2
                    var projectCandidateCategoryType2InvoicingEntry = model as EditAccountantProjectCandidateCategoryType2InvoicingEntryModel;
                    if (projectCandidateCategoryType2InvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantProjectCandidateCategoryType2InvoicingRequest>(projectCandidateCategoryType2InvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantProjectCandidateCategoryType2InvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingProjectCandidateCategoryType2EntryView>(
                            service => service.RetrieveAccountantProjectCandidateCategoryType2InvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantProjectCandidateCategoryType2InvoicingModel", updatedModel);
                    }

                    //project candidate category type 3
                    var projectCandidateCategoryType3InvoicingEntry = model as EditAccountantProjectCandidateCategoryType3InvoicingEntryModel;
                    if (projectCandidateCategoryType3InvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantProjectCandidateCategoryType3InvoicingRequest>(projectCandidateCategoryType3InvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantProjectCandidateCategoryType3InvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingProjectCandidateCategoryType3EntryView>(
                            service => service.RetrieveAccountantProjectCandidateCategoryType3InvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantProjectCandidateCategoryType3InvoicingModel", updatedModel);
                    }

                    //project product
                    var projectProductInvoicingEntry = model as EditAccountantProjectProductInvoicingEntryModel;
                    if (projectProductInvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantProjectProductInvoicingRequest>(projectProductInvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantProjectProductInvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingProjectProductEntryView>(
                            service => service.RetrieveAccountantProjectProductInvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantProjectProductInvoicingModel", updatedModel);
                    }

                    //Acdc project fixed price
                    var acdcProjectFixedPriceInvoicingEntry = model as EditAccountantAcdcProjectFixedPriceInvoicingEntryModel;
                    if (acdcProjectFixedPriceInvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantAcdcProjectFixedPriceInvoicingRequest>(acdcProjectFixedPriceInvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantAcdcProjectFixedPriceInvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingAcdcProjectFixedPriceEntryView>(
                            service => service.RetrieveAccountantAcdcProjectFixedPriceInvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantAcdcProjectFixedPriceInvoicingModel", updatedModel);
                    }

                    //Consultancy project fixed price
                    var consultancyProjectFixedPriceInvoicingEntry = model as EditAccountantConsultancyProjectFixedPriceInvoicingEntryModel;
                    if (consultancyProjectFixedPriceInvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantConsultancyProjectFixedPriceInvoicingRequest>(consultancyProjectFixedPriceInvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantConsultancyProjectFixedPriceInvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingConsultancyProjectFixedPriceEntryView>(
                            service => service.RetrieveAccountantConsultancyProjectFixedPriceInvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantConsultancyProjectFixedPriceInvoicingModel", updatedModel);
                    }

                    //Productsheet entry
                    var productSheetEntryInvoicingEntry = model as EditAccountantProductSheetEntryInvoicingEntryModel;
                    if (productSheetEntryInvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantProductSheetEntryInvoicingRequest>(productSheetEntryInvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantProductSheetEntryInvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingProductSheetEntryView>(
                            service => service.RetrieveAccountantProductSheetEntryInvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantProductSheetEntryInvoicingModel", updatedModel);
                    }

                    //Timesheet entry
                    var timesheetEntryInvoicingEntry = model as EditAccountantTimesheetEntryInvoicingEntryModel;
                    if (timesheetEntryInvoicingEntry != null)
                    {
                        var request = Mapper.DynamicMap<UpdateAccountantTimesheetEntryInvoicingRequest>(timesheetEntryInvoicingEntry);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAccountantInvoicingEntry(request));
                        var updatedModel = Mapper.DynamicMap<EditAccountantTimesheetEntryInvoicingEntryModel>(this.InvokeService<IFinanceManagementQueryService, AccountantInvoicingTimesheetEntryView>(
                            service => service.RetrieveAccountantTimesheetEntryInvoicingEntry(model.Id)));
                        return PartialView("EditorTemplates/EditAccountantTimesheetEntryInvoicingModel", updatedModel);
                    }

                    throw new InvalidCastException("Cannot cast to appropriate model.");
                }
                catch (Exception exception)
                {
                    var faultException = exception as FaultException<ValidationContainer>;
                    if (faultException != null && faultException.Detail.FaultDetail.FaultEntries.OfType<VersionMismatchFaultEntry>().Any())
                    {
                        //Version mismatch
                        LogManager.LogError(exception);
                        Response.StatusCode = (int) HttpStatusCode.Conflict;
                        return PartialView("AccountantInvoicingVersionMismatch");
                    }
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }              

        #region Poposal methods

        [HttpPost]
        public ActionResult AddProposalForProjectCandidate(AddProposalForProjectCandidateModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateProjectCandidateProposalRequest()
                    {
                        ProjectCandidateId = model.ProjectCandidateId,
                        ProposalId = proposalId
                        
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateProjectCandidateProposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForProjectCandidateCategoryType1(AddProposalForProjectCandidateCategoryType1Model model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateProjectCandidateDetailType1ProposalRequest()
                    {
                        ProjectCandidateDetailType1Id = model.ProjectCandidateCategoryType1Id,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateProjectCandidateCategoryType1Proposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForProjectCandidateCategoryType2(AddProposalForProjectCandidateCategoryType2Model model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateProjectCandidateDetailType2ProposalRequest()
                    {
                        ProjectCandidateDetailType2Id = model.ProjectCandidateCategoryType2Id,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateProjectCandidateCategoryType2Proposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForProjectCandidateCategoryType3(AddProposalForProjectCandidateCategoryType3Model model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateProjectCandidateDetailType3ProposalRequest()
                    {
                        ProjectCandidateDetailType3Id = model.ProjectCandidateCategoryType3Id,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateProjectCandidateCategoryType3Proposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForProjectProduct(AddProposalForProjectProductModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateProjectProductProposalRequest()
                    {
                        ProjectProductId = model.ProjectProductId,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateProjectProductProposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForTimeSheetEntry(AddProposalForTimeSheetEntryModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateTimeSheetEntryProposalRequest()
                    {
                        TimeSheetEntryId = model.TimeSheetEntryId,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateTimeSheetEntryProposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForProductSheetEntry(AddProposalForProductSheetEntryModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateProductSheetEntryProposalRequest()
                    {
                        ProductSheetEntryId = model.ProductSheetEntryId,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateProductSheetEntryProposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForAcdcProjectFixedPrice(AddProposalForAcdcProjectFixedPriceModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateAcdcProjectFixedPriceProposalRequest()
                    {
                        AcdcProjectFixedPriceId = model.AcdcProjectFixedPriceId,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateAcdcProjectFixedPriceProposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProposalForConsultancyProjectFixedPrice(AddProposalForConsultancyProjectFixedPriceModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProposalRequest = CreateNewProposalRequest(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProposal(createNewProposalRequest));

                    var request = new UpdateConsultancyProjectFixedPriceProposalRequest()
                    {
                        ConsultancyProjectFixedPriceId = model.ConsultancyProjectFixedPriceId,
                        ProposalId = proposalId
                    };
                    this.InvokeService<IProjectManagementCommandService, Guid>(service => service.UpdateConsultancyProjectFixedPriceProposal(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        private CreateNewProposalRequest CreateNewProposalRequest(AddProposalModel model)
        {
            return new CreateNewProposalRequest()
            {
                ContactId = model.ContactId,
                //Name = model.ProjectName + " - " + (string.IsNullOrEmpty(model.CandidateFullName) ? model.CandidateFullName + " - " : "") + model.ProductName,
                Name = model.ProjectName + " - " + model.ProductName + " " + model.CandidateFullName,
                Deadline = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddDays(-1), //Last day of the previous month
                DateReceived = DateTime.Now,
                Description = string.Empty,
                StatusCode = ProposalStatusType.Won,
                FinalBudget = model.InvoiceAmount,
                DateWon = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 27)).AddMonths(-1)
                
            };
        }

        #endregion

        #region Helper methods

        private static List<EditInvoicingBaseEntryModel> CastInvoicingViewsToEditModels(IEnumerable<InvoicingBaseEntryView> entries)
        {
            //Do casting to appropriate edit models.
            var invoicingEntries = new List<EditInvoicingBaseEntryModel>();
            foreach (var entry in entries)
            {
                //project candidate
                var projectCandidateInvoicingEntry = entry as InvoicingProjectCandidateEntryView;
                if (projectCandidateInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditProjectCandidateInvoicingEntryModel>(projectCandidateInvoicingEntry));
                }

                //project candidate category type 1
                var projectCandidateCategoryType1InvoicingEntry = entry as InvoicingProjectCandidateCategoryType1EntryView;
                if (projectCandidateCategoryType1InvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditProjectCandidateCategoryType1InvoicingEntryModel>(projectCandidateCategoryType1InvoicingEntry));
                }

                //project candidate category type 2
                var projectCandidateCategoryType2InvoicingEntry = entry as InvoicingProjectCandidateCategoryType2EntryView;
                if (projectCandidateCategoryType2InvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditProjectCandidateCategoryType2InvoicingEntryModel>(projectCandidateCategoryType2InvoicingEntry));
                }

                //project candidate category type 3
                var projectCandidateCategoryType3InvoicingEntry = entry as InvoicingProjectCandidateCategoryType3EntryView;
                if (projectCandidateCategoryType3InvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditProjectCandidateCategoryType3InvoicingEntryModel>(projectCandidateCategoryType3InvoicingEntry));
                }

                //project product
                var projectProductInvoicingEntry = entry as InvoicingProjectProductEntryView;
                if (projectProductInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditProjectProductInvoicingEntryModel>(projectProductInvoicingEntry));
                }

                //ACDC project fixed price
                var acdcProjectFixedPriceInvoicingEntry = entry as InvoicingAssessmentDevelopmentProjectFixedPriceEntryView;
                if (acdcProjectFixedPriceInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAssessmentDevelopmentProjectFixedPriceInvoicingEntryModel>(acdcProjectFixedPriceInvoicingEntry));
                }

                //Consultancy project fixed price
                var consultancyProjectFixedPriceInvoicingEntry = entry as InvoicingConsultancyProjectFixedPriceEntryView;
                if (consultancyProjectFixedPriceInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditConsultancyProjectFixedPriceInvoicingEntryModel>(consultancyProjectFixedPriceInvoicingEntry));
                }

                //Productsheet entry
                var productSheetEntryInvoicingEntry = entry as InvoicingProductSheetEntryView;
                if (productSheetEntryInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditProductSheetEntryInvoicingEntryModel>(productSheetEntryInvoicingEntry));
                }

                //Timesheet entry
                var timesheetEntryInvoicingEntry = entry as InvoicingTimesheetEntryView;
                if (timesheetEntryInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditTimesheetEntryInvoicingEntryModel>(timesheetEntryInvoicingEntry));
                }
            }

            return invoicingEntries;
        }

        private static IEnumerable<EditAccountantInvoicingBaseEntryModel> CastAccountantInvoicingViewsToEditModels(IEnumerable<AccountantInvoicingBaseEntryView> entries)
        {
            //Do casting to appropriate edit models.
            var invoicingEntries = new List<EditAccountantInvoicingBaseEntryModel>();
            foreach (var entry in entries)
            {
                //project candidate
                var projectCandidateInvoicingEntry = entry as AccountantInvoicingProjectCandidateEntryView;
                if (projectCandidateInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantProjectCandidateInvoicingEntryModel>(projectCandidateInvoicingEntry));
                }

                //project candidate category type 1
                var projectCandidateCategoryType1InvoicingEntry = entry as AccountantInvoicingProjectCandidateCategoryType1EntryView;
                if (projectCandidateCategoryType1InvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantProjectCandidateCategoryType1InvoicingEntryModel>(projectCandidateCategoryType1InvoicingEntry));
                }

                //project candidate category type 2
                var projectCandidateCategoryType2InvoicingEntry = entry as AccountantInvoicingProjectCandidateCategoryType2EntryView;
                if (projectCandidateCategoryType2InvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantProjectCandidateCategoryType2InvoicingEntryModel>(projectCandidateCategoryType2InvoicingEntry));
                }

                //project candidate category type 3
                var projectCandidateCategoryType3InvoicingEntry = entry as AccountantInvoicingProjectCandidateCategoryType3EntryView;
                if (projectCandidateCategoryType3InvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantProjectCandidateCategoryType3InvoicingEntryModel>(projectCandidateCategoryType3InvoicingEntry));
                }

                //project product
                var projectProductInvoicingEntry = entry as AccountantInvoicingProjectProductEntryView;
                if (projectProductInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantProjectProductInvoicingEntryModel>(projectProductInvoicingEntry));
                }

                //Acdc project fixed price
                var acdcProjectFixedPriceInvoicingEntry = entry as AccountantInvoicingAcdcProjectFixedPriceEntryView;
                if (acdcProjectFixedPriceInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantAcdcProjectFixedPriceInvoicingEntryModel>(acdcProjectFixedPriceInvoicingEntry));
                }

                //Consultancy project fixed price
                var consultancyProjectFixedPriceInvoicingEntry = entry as AccountantInvoicingConsultancyProjectFixedPriceEntryView;
                if (consultancyProjectFixedPriceInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantConsultancyProjectFixedPriceInvoicingEntryModel>(consultancyProjectFixedPriceInvoicingEntry));
                }

                //Productsheet entry
                var productSheetEntryInvoicingEntry = entry as AccountantInvoicingProductSheetEntryView;
                if (productSheetEntryInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantProductSheetEntryInvoicingEntryModel>(productSheetEntryInvoicingEntry));
                }

                //Timesheet entry
                var timesheetEntryInvoicingEntry = entry as AccountantInvoicingTimesheetEntryView;
                if (timesheetEntryInvoicingEntry != null)
                {
                    invoicingEntries.Add(Mapper.DynamicMap<EditAccountantTimesheetEntryInvoicingEntryModel>(timesheetEntryInvoicingEntry));
                }
            }

            return invoicingEntries;
        }

        private static List<UpdateInvoicingBaseRequest> CastInvoicingEditModelsToUpdateProjectManagerRequests(IEnumerable<EditInvoicingBaseEntryModel> entries)
        {
            //Do casting to appropriate edit models.
            var updateRequests = new List<UpdateInvoicingBaseRequest>();
            foreach (var entry in entries)
            {
                //project candidate
                var projectCandidateInvoicingEntry = entry as EditProjectCandidateInvoicingEntryModel;
                if (projectCandidateInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerProjectCandidateInvoicingRequest>(projectCandidateInvoicingEntry));
                }

                //project candidate category type 1
                var projectCandidateCategoryType1InvoicingEntry = entry as EditProjectCandidateCategoryType1InvoicingEntryModel;
                if (projectCandidateCategoryType1InvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerProjectCandidateCategoryType1InvoicingRequest>(projectCandidateCategoryType1InvoicingEntry));
                }

                //project candidate category type 2
                var projectCandidateCategoryType2InvoicingEntry = entry as EditProjectCandidateCategoryType2InvoicingEntryModel;
                if (projectCandidateCategoryType2InvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerProjectCandidateCategoryType2InvoicingRequest>(projectCandidateCategoryType2InvoicingEntry));
                }

                //project candidate category type 3
                var projectCandidateCategoryType3InvoicingEntry = entry as EditProjectCandidateCategoryType3InvoicingEntryModel;
                if (projectCandidateCategoryType3InvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerProjectCandidateCategoryType3InvoicingRequest>(projectCandidateCategoryType3InvoicingEntry));
                }

                //project product
                var projectProductInvoicingEntry = entry as EditProjectProductInvoicingEntryModel;
                if (projectProductInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerProjectProductInvoicingRequest>(projectProductInvoicingEntry));
                }

                //Acdc project fixed price
                var acdcProjectFixedPriceInvoicingEntry = entry as EditAssessmentDevelopmentProjectFixedPriceInvoicingEntryModel;
                if (acdcProjectFixedPriceInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerAcdcProjectFixedPriceInvoicingRequest>(acdcProjectFixedPriceInvoicingEntry));
                }

                //Acdc project fixed price
                var consultancyProjectFixedPriceInvoicingEntry = entry as EditConsultancyProjectFixedPriceInvoicingEntryModel;
                if (consultancyProjectFixedPriceInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerAcdcProjectFixedPriceInvoicingRequest>(consultancyProjectFixedPriceInvoicingEntry));
                }


                //Productsheet entry
                var productSheetEntryInvoicingEntry = entry as EditProductSheetEntryInvoicingEntryModel;
                if (productSheetEntryInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerProductSheetEntryInvoicingRequest>(productSheetEntryInvoicingEntry));
                }

                //Timesheet entry
                var timesheetEntryInvoicingEntry = entry as EditTimesheetEntryInvoicingEntryModel;
                if (timesheetEntryInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateProjectManagerTimesheetEntryInvoicingRequest>(timesheetEntryInvoicingEntry));
                }
            }

            return updateRequests;
        }
        
        private static List<UpdateInvoicingBaseRequest> CastInvoicingEditModelsToUpdateCustomerAssistantRequests(IEnumerable<EditInvoicingBaseEntryModel> entries)
        {
            //Do casting to appropriate edit models.
            var updateRequests = new List<UpdateInvoicingBaseRequest>();
            foreach (var entry in entries)
            {
                //project candidate
                var projectCandidateInvoicingEntry = entry as EditProjectCandidateInvoicingEntryModel;
                if (projectCandidateInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantProjectCandidateInvoicingRequest>(projectCandidateInvoicingEntry));
                }

                //project candidate category type 1
                var projectCandidateCategoryType1InvoicingEntry = entry as EditProjectCandidateCategoryType1InvoicingEntryModel;
                if (projectCandidateCategoryType1InvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantProjectCandidateCategoryType1InvoicingRequest>(projectCandidateCategoryType1InvoicingEntry));
                }

                //project candidate category type 2
                var projectCandidateCategoryType2InvoicingEntry = entry as EditProjectCandidateCategoryType2InvoicingEntryModel;
                if (projectCandidateCategoryType2InvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantProjectCandidateCategoryType2InvoicingRequest>(projectCandidateCategoryType2InvoicingEntry));
                }

                //project candidate category type 3
                var projectCandidateCategoryType3InvoicingEntry = entry as EditProjectCandidateCategoryType3InvoicingEntryModel;
                if (projectCandidateCategoryType3InvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantProjectCandidateCategoryType3InvoicingRequest>(projectCandidateCategoryType3InvoicingEntry));
                }

                //project product
                var projectProductInvoicingEntry = entry as EditProjectProductInvoicingEntryModel;
                if (projectProductInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantProjectProductInvoicingRequest>(projectProductInvoicingEntry));
                }

                //Acdc project fixed price
                var acdcProjectFixedPriceInvoicingEntry = entry as EditAssessmentDevelopmentProjectFixedPriceInvoicingEntryModel;
                if (acdcProjectFixedPriceInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantAcdcProjectFixedPriceInvoicingRequest>(acdcProjectFixedPriceInvoicingEntry));
                }

                //Consultancy project fixed price
                var consultancyProjectFixedPriceInvoicingEntry = entry as EditConsultancyProjectFixedPriceInvoicingEntryModel;
                if (consultancyProjectFixedPriceInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantConsultancyProjectFixedPriceInvoicingRequest>(consultancyProjectFixedPriceInvoicingEntry));
                }

                //Productsheet entry
                var productSheetEntryInvoicingEntry = entry as EditProductSheetEntryInvoicingEntryModel;
                if (productSheetEntryInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantProductSheetEntryInvoicingRequest>(productSheetEntryInvoicingEntry));
                }

                //Timesheet entry
                var timesheetEntryInvoicingEntry = entry as EditTimesheetEntryInvoicingEntryModel;
                if (timesheetEntryInvoicingEntry != null)
                {
                    updateRequests.Add(Mapper.DynamicMap<UpdateCustomerAssistantTimesheetEntryInvoicingRequest>(timesheetEntryInvoicingEntry));
                }
            }

            return updateRequests;
        }

        #endregion

    }
}