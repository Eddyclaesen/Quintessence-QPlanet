using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.ViewModel.Crm;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Scm;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Sof;
using Quintessence.QPlanet.Webshell.Infrastructure.ModelBinders;
using System.IO;
using System.Web;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectConsultancyController : ProjectDetailControllerBase
    {
        #region Edit

        public override ActionResult Edit(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectView =
                        this.InvokeService<IProjectManagementQueryService, ConsultancyProjectView>(
                            service => service.RetrieveConsultancyProjectDetail(id));
                    var contactDetailView =
                        this.InvokeService<ICustomerRelationshipManagementQueryService, ContactDetailView>(
                            service => service.RetrieveContactDetail(projectView.ContactId));

                    var projectModel = Mapper.Map<EditProjectConsultancyModel>(projectView);

                    projectModel.ContactDetail = Mapper.Map<ContactDetailModel>(contactDetailView);
                    projectModel.ContactDetail.ProjectId = projectView.Id;

                    projectModel.IsCurrentUserProjectManager = projectView.ProjectManagerId == GetAuthenticationToken().UserId;
                    projectModel.IsCurrentUserCustomerAssistant = projectView.CustomerAssistantId == GetAuthenticationToken().UserId;

                    var possibleStatusses =
                        this.InvokeService<IProjectManagementQueryService, List<ProjectStatusCodeViewType>>(
                            service => service.ListPossibleStatusses(projectModel.StatusCode));

                    projectModel.ProjectStatusses =
                        possibleStatusses.Select(
                            s => new ProjectStatusTypeSelectListItemModel { Id = (int)s, Name = s.ToString() }).ToList();

                    return View(projectModel);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(EditProjectConsultancyModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateProjectRequest = Mapper.Map<UpdateConsultancyProjectRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateConsultancyProject(updateProjectRequest));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult ActivityProfiles(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListActivitiesRequest { ProjectId = id };
                    var activities = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityView>>(service => service.ListActivities(request));
                    var model = activities.Select(Mapper.DynamicMap<EditActivityModel>).OrderByDescending(a => a.AuditCreatedOn).ToList();
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        [HttpPost]
        public ActionResult ActivityProfiles(List<EditActivityModel> model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = model.Select(Mapper.Map<UpdateActivityRequest>).ToList();
                    this.InvokeService<ISupplyChainManagementCommandService>(
                        service => service.UpdateActivities(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        public ActionResult AddActivity(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new AddActivityActionModel();
                    var activityTypes =
                        this.InvokeService<ISupplyChainManagementQueryService, List<ActivityTypeView>>(
                            service => service.ListActivityTypes());
                    model.ActivityTypes = activityTypes.Select(at => new ActivityTypeSelectListItemModel(at)).ToList();
                    model.ProjectId = id;
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        [HttpPost]
        public ActionResult AddActivity(AddActivityActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewActivityRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(
                        service => service.CreateNewActivity(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        public ActionResult AddActivityProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new AddActivityProfileActionModel();
                    var activityTypeProfiles = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityTypeProfileView>>(service =>service.ListActivityTypeProfiles(new ListActivityTypeProfilesRequest { ActivityId = id }));

                    activityTypeProfiles.Sort((a, b) => String.Compare(a.ProfileName, b.ProfileName, StringComparison.Ordinal));

                    model.ProfileSelectListItems = activityTypeProfiles.Select(at => new ActivityTypeProfileSelectListItemModel(at)).ToList();
                    model.Profiles = activityTypeProfiles;
                    model.ActivityId = id;

                    var selectedProfile = activityTypeProfiles.FirstOrDefault();

                    if (selectedProfile != null)
                    {
                        model.ProfileId = selectedProfile.ProfileId;
                        model.DayRate = selectedProfile.DayRate;
                        model.HalfDayRate = selectedProfile.HalfDayRate;
                        model.HourlyRate = selectedProfile.HourlyRate;
                        model.IsolatedHourlyRate = selectedProfile.IsolatedHourlyRate;
                    }

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        [HttpPost]
        public ActionResult AddActivityProfile(AddActivityProfileActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewActivityProfileRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.CreateNewActivityProfile(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteActivityProfile(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(
                        service => service.DeleteActivityProfile(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteActivity(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.DeleteActivity(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.DeleteProduct(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult Products(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var products = this.InvokeService<ISupplyChainManagementQueryService, List<ProductView>>(service => service.ListProducts(id));
                    var model = products.Select(Mapper.DynamicMap<EditProductModel>).ToList();
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        [HttpPost]
        public ActionResult Products(List<EditProductModel> model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = model.Select(Mapper.Map<UpdateProductRequest>).ToList();
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateProducts(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        public ActionResult AddProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new AddProductActionModel();
                    model.ProductTypes = this.InvokeService<ISupplyChainManagementQueryService, List<ProductTypeView>>(service => service.ListProductTypes());
                    model.ProductTypeSelectListItems = model.ProductTypes.Select(at => new ProductTypeSelectListItemModel(at)).ToList();
                    model.ProjectId = id;

                    var selectedProductType = model.ProductTypes.FirstOrDefault();
                    if (selectedProductType != null)
                    {
                        model.Name = selectedProductType.Name;
                        model.ProductTypeId = selectedProductType.Id;
                        model.UnitPrice = selectedProductType.UnitPrice;
                    }
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProductRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.CreateNewProduct(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }

        #endregion

        #region EditProjectPlan

        public ActionResult EditProjectPlan(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new EditProjectPlanActionModel();
                    model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));
                    model.IsCurrentUserProjectManager = model.Project.ProjectManagerId == GetAuthenticationToken().UserId;
                    model.IsCurrentUserCustomerAssistant = model.Project.CustomerAssistantId == GetAuthenticationToken().UserId;
                    
                    var request = new RetrieveProjectPlanDetailRequest { ProjectPlanId = model.Project.ProjectPlanId };
                    var projectPlan = this.InvokeService<IProjectManagementQueryService, ProjectPlanView>(service => service.RetrieveProjectPlanDetail(request));
                    model.ProjectPlan = Mapper.Map<EditProjectPlanModel>(projectPlan);

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult ProjectPlanPhases(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));

                    var request = new RetrieveProjectPlanDetailRequest { ProjectId = id };
                    var projectPlan = this.InvokeService<IProjectManagementQueryService, ProjectPlanView>(service => service.RetrieveProjectPlanDetail(request));

                    var model = new ProjectPlanPhasesActionModel();
                    model.Project = project;
                    model.ProjectPlanPhases = projectPlan.ProjectPlanPhases.Select(Mapper.Map<EditProjectPlanPhaseModel>).OrderByDescending(ppp => ppp.EndDate).ToList();
                    
                    if (model.Project.PricingModelType == PricingModelType.FixedPrice)
                    {
                        var fixedPrices = this.InvokeService<IProjectManagementQueryService, List<ProjectFixedPriceView>>(service => service.ListProjectFixedPrices(id));
                        model.ProjectFixedPrices = fixedPrices.OrderByDescending(fp => fp.Deadline).ToList();
                    }

                    model.ProjectPlanPhases.ForEach(ppp => ppp.ProjectPlanPhaseEntries.Sort((entry1, entry2) => entry1.Deadline.CompareTo(entry2.Deadline)));

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult CreateProjectPlanPhaseActivity(Guid id, Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhase = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseView>(service => service.RetrieveProjectPlanPhase(id));

                    var request = new ListActivitiesRequest { ProjectId = projectId };

                    var model = new CreateProjectPlanPhaseActivityModel();
                    model.Activities = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityView>>(service => service.ListActivities(request));

                    if (model.Activities != null && model.Activities.Count > 0)
                    {
                        var activity = model.Activities.SelectMany(a => a.ActivityProfiles).FirstOrDefault();
                        if (activity != null)
                            model.ActivityProfileId = activity.Id;
                    }

                    model.Deadline = projectPlanPhase.EndDate;
                    model.ProjectPlanPhaseId = id;

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
        public ActionResult CreateProjectPlanPhaseActivity(CreateProjectPlanPhaseActivityModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProjectPlanPhaseActivityRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateNewProjectPlanPhaseEntry(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult UpdateProjectPlanPhaseActivity(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhaseActivity = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseActivityView>(service => service.RetrieveProjectPlanPhaseActivity(id));
                    var model = Mapper.Map<EditProjectPlanPhaseActivityModel>(projectPlanPhaseActivity);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectPlanPhaseActivity(EditProjectPlanPhaseActivityModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectPlanPhaseActivityRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectPlanPhaseEntry(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult UpdateProjectPlanPhaseProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhaseProduct = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseProductView>(service => service.RetrieveProjectPlanPhaseProduct(id));
                    var model = Mapper.Map<EditProjectPlanPhaseProductModel>(projectPlanPhaseProduct);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectPlanPhaseProduct(EditProjectPlanPhaseProductModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectPlanPhaseProductRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectPlanPhaseEntry(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult UpdateProjectPlanPhaseActivityDeadline(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhaseActivity = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseActivityView>(service => service.RetrieveProjectPlanPhaseActivity(id));
                    var model = Mapper.Map<EditProjectPlanPhaseEntryDeadlineModel>(projectPlanPhaseActivity);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectPlanPhaseActivityDeadline(EditProjectPlanPhaseEntryDeadlineModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectPlanPhaseEntryDeadlineRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectPlanPhaseEntryDeadline(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult UpdateProjectPlanPhaseProductDeadline(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhaseProduct = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseProductView>(service => service.RetrieveProjectPlanPhaseProduct(id));
                    var model = Mapper.Map<EditProjectPlanPhaseEntryDeadlineModel>(projectPlanPhaseProduct);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectPlanPhaseProductDeadline(EditProjectPlanPhaseEntryDeadlineModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectPlanPhaseEntryDeadlineRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectPlanPhaseEntryDeadline(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteProjectPlanPhaseActivity(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectPlanPhaseEntry(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteProjectPlanPhaseProduct(Guid id, Guid entryId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectPlanPhaseProduct(id, entryId));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult CreateProjectPlanPhaseProduct(Guid id, Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhase = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseView>(service => service.RetrieveProjectPlanPhase(id));

                    var model = new CreateProjectPlanPhaseProductModel();
                    model.ProjectId = projectId;

                    model.Products = this.InvokeService<ISupplyChainManagementQueryService, List<ProductView>>(service => service.ListProducts(projectId));

                    if (model.Products != null && model.Products.Count > 0)
                    {
                        var product = model.Products.FirstOrDefault();
                        if (product != null)
                        {
                            model.ProductId = product.Id;
                            model.UnitPrice = product.UnitPrice;
                            model.Quantity = 1;
                            model.TotalPrice = model.UnitPrice * model.Quantity;
                            model.ProductName = string.Format("{0} ({1})", product.Name, product.ProductTypeName);
                        }
                    }

                    model.Deadline = projectPlanPhase.EndDate;
                    model.ProjectPlanPhaseId = id;

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
        public ActionResult CreateProjectPlanPhaseProduct(CreateProjectPlanPhaseProductModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProjectPlanPhaseProductRequest>(model);
                    var identity = IdentityHelper.RetrieveIdentity(HttpContext);
                    var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationToken(new Guid(identity.Ticket.UserData)));
                    request.UserId = token.UserId;
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateNewProjectPlanPhaseEntry(request));                    

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult AddProjectPlanPhase(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new EditProjectPlanPhaseModel();
                    model.ProjectPlanId = id;

                    var today = DateTime.Now;
                    while (today.DayOfWeek != DayOfWeek.Monday)
                    {
                        today = today.AddDays(-1);
                    }

                    model.StartDate = today;

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
        public ActionResult AddProjectPlanPhase(EditProjectPlanPhaseModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProjectPlanPhaseRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateNewProjectPlanPhase(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditProjectPlanPhase(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhase = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseView>(service => service.RetrieveProjectPlanPhase(id));
                    var model = Mapper.DynamicMap<EditProjectPlanPhaseModel>(projectPlanPhase);

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
        public ActionResult EditProjectPlanPhase(EditProjectPlanPhaseModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectPlanPhaseRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectPlanPhase(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteProjectPlanPhase(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectPlanPhase(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        #endregion

        #region EditActivityDetail

        public ActionResult ActivityDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new ActivityDetailActionModel();
                    model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));
                    model.IsCurrentUserProjectManager = model.Project.ProjectManagerId == GetAuthenticationToken().UserId;
                    model.IsCurrentUserCustomerAssistant = model.Project.CustomerAssistantId == GetAuthenticationToken().UserId;

                    model.Activities = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityView>>(service => service.ListActivities(new ListActivitiesRequest { ProjectId = id }));

                    model.Activities.Sort((a, b) => a.ActivityTypeName.CompareTo(b.ActivityTypeName));

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditActivityDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var activityDetail = this.InvokeService<ISupplyChainManagementQueryService, ActivityDetailView>(service => service.RetrieveActivityDetail(id));

                    switch (activityDetail.GetType().Name)
                    {
                        case "ActivityDetailCoachingView":
                            var coachingModel = Mapper.Map<EditActivityDetailCoachingModel>(activityDetail);
                            
                            return PartialView("EditActivityDetailCoaching", coachingModel);

                        case "ActivityDetailConsultingView":
                            var consultingModel = Mapper.Map<EditActivityDetailConsultingModel>(activityDetail);
                            return PartialView("EditActivityDetailConsulting", consultingModel);

                        case "ActivityDetailSupportView":
                            var supportModel = Mapper.Map<EditActivityDetailSupportModel>(activityDetail);
                            return PartialView("EditActivityDetailSupport", supportModel);

                        case "ActivityDetailTrainingView":
                            var trainingModel = Mapper.Map<EditActivityDetailTrainingModel>(activityDetail);
                            trainingModel.ActivityDetailTrainingLanguages = trainingModel.ActivityDetailTrainingLanguages.OrderBy(adtl => adtl.LanguageId).ToList();

                            var trainingTypes = this.InvokeService<ISupplyChainManagementQueryService, List<TrainingTypeView>>(service => service.ListTrainingTypes());

                            foreach (var trainingType in trainingTypes)
                            {
                                var editActivityDetailTrainingTypeModel = new EditActivityDetailTrainingTypeModel
                                    {
                                        Id = trainingType.Id,
                                        Name = trainingType.Name,
                                        IsChecked =
                                            ((ActivityDetailTrainingView)activityDetail).ActivityDetailTrainingTypes.Any(
                                                att => att.Id == trainingType.Id)
                                    };
                                trainingModel.TrainingTypes.Add(editActivityDetailTrainingTypeModel);
                            }

                            return PartialView("EditActivityDetailTraining", trainingModel);

                        case "ActivityDetailWorkshopView":
                            var workshopModel = Mapper.Map<EditActivityDetailWorkshopModel>(activityDetail);
                            return PartialView("EditActivityDetailWorkshop", workshopModel);

                        default:
                            var activityDetailGenericView = Mapper.Map<EditActivityDetailModel>(activityDetail);
                            return PartialView("EditActivityDetailGeneric", activityDetailGenericView);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityDetailTraining(EditActivityDetailTrainingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateRequest = Mapper.Map<UpdateActivityDetailTrainingRequest>(model);
                    updateRequest.SelectedTrainingTypeIds = model.TrainingTypes.Where(adtt => adtt.IsChecked).Select(adtt => adtt.Id).ToList();
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityDetailTraining(updateRequest));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityDetailCoaching(EditActivityDetailCoachingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateRequest = Mapper.Map<UpdateActivityDetailCoachingRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityDetailCoaching(updateRequest));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityDetailSupport(EditActivityDetailSupportModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateRequest = Mapper.Map<UpdateActivityDetailSupportRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityDetailSupport(updateRequest));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityDetailConsulting(EditActivityDetailConsultingModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateRequest = Mapper.Map<UpdateActivityDetailConsultingRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityDetailConsulting(updateRequest));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityDetailWorkshop(EditActivityDetailWorkshopModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateRequest = Mapper.Map<UpdateActivityDetailWorkshopRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityDetailWorkshop(updateRequest));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        #endregion

        #region Timesheet

        public ActionResult Timesheet(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new TimesheetActionModel();
                    model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));
                    model.IsCurrentUserProjectManager = model.Project.ProjectManagerId == GetAuthenticationToken().UserId;
                    model.IsCurrentUserCustomerAssistant = model.Project.CustomerAssistantId == GetAuthenticationToken().UserId;

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult Timesheets(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var timesheets = this.InvokeService<IProjectManagementQueryService, List<TimesheetEntryView>>(service => service.ListAllProjectTimesheets(id));
                    return PartialView(timesheets);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        //public ActionResult Productsheets(Guid id)
        //{
        //    using (DurationLog.Create())
        //    {
        //        try
        //        {
        //            var productsheets = this.InvokeService<IProjectManagementQueryService, List<ProductsheetEntryView>>(service => service.ListProjectUserProductsheets(id));
        //            return PartialView(productsheets);
        //        }
        //        catch (Exception exception)
        //        {
        //            LogManager.LogError(exception);
        //            return HandleError(exception);
        //        }
        //    }
        //}

        public ActionResult EditTimesheet(Guid id, int? year, int? month, Guid? userId, bool isProjectManager = false)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new EditTimesheetActionModel();
                    model.IsProjectManager = isProjectManager;
                    model.UserId = userId;
                    model.ProjectId = id;
                    model.Year = year.GetValueOrDefault(DateTime.Now.Year);
                    model.Month = month.GetValueOrDefault(DateTime.Now.Month);

                    if (userId == null)
                    {
                        var identity = IdentityHelper.RetrieveIdentity(HttpContext);
                        var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationToken(new Guid(identity.Ticket.UserData)));

                        userId = token.UserId;
                    }

                    var registeredEntries = this.InvokeService<IProjectManagementQueryService, List<TimesheetEntryView>>(
                            service => service.RetrieveProjectTimesheet(id, userId, year.GetValueOrDefault(DateTime.Now.Year), month.GetValueOrDefault(DateTime.Now.Month), isProjectManager));

                    var indices = this.InvokeService<IProjectManagementQueryService, List<ProjectPriceIndexView>>(service => service.ListProjectPriceIndices(id));

                    var request = new ListTimesheetUnregisteredEntriesRequest
                        {
                            ProjectId = id,
                            UserId = userId,
                            MonthDate = new DateTime(year.GetValueOrDefault(DateTime.Now.Year), month.GetValueOrDefault(DateTime.Now.Month), 1),
                            IsProjectManager = isProjectManager
                        };

                    var unregisteredEntries = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmTimesheetUnregisteredEntryView>>(
                        service => service.ListTimesheetUnregisteredEntries(request));

                    var projectPlan = this.InvokeService<IProjectManagementQueryService, ProjectPlanView>(service => service.RetrieveProjectPlanDetail(new RetrieveProjectPlanDetailRequest { ProjectId = id }));
                    var activities = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityView>>(service => service.ListActivities(new ListActivitiesRequest { ProjectId = id }));
                    
                    model.ProjectPlan = projectPlan;
                    model.Entries = registeredEntries.OrderBy(e => e.Date).Select(Mapper.DynamicMap<EditTimesheetEntryModel>).ToList();
                    model.ActivityProfiles = activities.SelectMany(a => a.ActivityProfiles).ToList();
                    model.ProjectPriceIndices = indices;

                    model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));
                    
                    model.UnregisteredEntries = new List<CreateNewTimesheetEntryModel>();
                    foreach (var unregisteredEntry in unregisteredEntries.OrderBy(e => e.StartDate))
                    {
                        model.UnregisteredEntries.Add(new CreateNewTimesheetEntryModel
                            {
                                UserId = userId.Value,
                                User = unregisteredEntry.User,
                                ProjectId = id,
                                ActivityProfileId = null,
                                AppointmentId = unregisteredEntry.Id,
                                Duration = Math.Round((decimal)(unregisteredEntry.EndDate - unregisteredEntry.StartDate).TotalSeconds / 3600, 2),
                                InvoiceAmount = 0,
                                Date = unregisteredEntry.StartDate.Date,
                                InvoiceStatusCode = (int)InvoiceStatusType.Draft,
                                Description = unregisteredEntry.Description,
                                Category = unregisteredEntry.Category
                            });
                    }

                    if (isProjectManager)
                    {
                        model.Entries = model.Entries.OrderBy(x => x.User.FullName).ThenBy(x => x.Date).ToList();
                        model.UnregisteredEntries = model.UnregisteredEntries.OrderBy(x => x.User.FullName).ThenBy(x => x.Date).ToList();
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
        public ActionResult EditTimesheet(EditTimesheetActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new SaveTimesheetEntriesRequest();

                    if (model.Entries != null)
                        request.UpdateTimesheetEntries = model.Entries
                            .Select(Mapper.DynamicMap<UpdateTimesheetEntryRequest>).ToList();

                    if (model.UnregisteredEntries != null)
                        request.CreateTimesheetEntries = model.UnregisteredEntries
                            .Where(e => e.ActivityProfileId.HasValue)
                            .Select(Mapper.DynamicMap<CreateNewTimesheetEntryRequest>).ToList();

                    this.InvokeService<IProjectManagementCommandService>(service => service.SaveTimesheetEntries(request));

                    return RedirectToAction("EditTimesheet", new { id = model.ProjectId, year = model.Year, month = model.Month, userId = model.UserId, isProjectManager = model.IsProjectManager });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        //public ActionResult EditProductsheet(Guid id, int? year, int? month)
        //{
        //    using (DurationLog.Create())
        //    {
        //        try
        //        {
        //            var registeredEntries = this.InvokeService<IProjectManagementQueryService, List<ProductsheetEntryView>>(service => service.RetrieveProjectProductsheet(id, year, month));

        //            var indices = this.InvokeService<IProjectManagementQueryService, List<ProjectPriceIndexView>>(service => service.ListProjectPriceIndices(id));

        //            var activeProjectPlanPhases = this.InvokeService<IProjectManagementQueryService, List<ProjectPlanPhaseView>>(service => service.ListActiveProjectPlanPhases(id, year, month));

        //            var projectPlan = this.InvokeService<IProjectManagementQueryService, ProjectPlanView>(service => service.RetrieveProjectPlanDetail(new RetrieveProjectPlanDetailRequest { ProjectId = id }));

        //            var model = new EditProductsheetActionModel();
        //            model.ProjectId = id;
        //            model.Year = year.GetValueOrDefault(DateTime.Now.Year);
        //            model.Month = month.GetValueOrDefault(DateTime.Now.Month);

        //            model.ProjectPlan = projectPlan;
        //            model.Entries = registeredEntries.OrderBy(e => e.Date).Select(Mapper.DynamicMap<EditProductsheetEntryModel>).ToList();
        //            model.ProjectPriceIndices = indices;

        //            model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));

        //            var identity = IdentityHelper.RetrieveIdentity(HttpContext);
        //            var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationToken(new Guid(identity.Ticket.UserData)));

        //            foreach (var activeProjectPlanPhase in activeProjectPlanPhases)
        //            {
        //                var projectPlanPhaseProducts = this.InvokeService<IProjectManagementQueryService, List<ProjectPlanPhaseProductView>>(service => service.ListProjectPlanPhaseProducts(activeProjectPlanPhase.Id));

        //                foreach (var projectPlanPhaseProductGroup in projectPlanPhaseProducts.GroupBy(pppp => pppp.ProductId))
        //                {
        //                    var firstProjectPlanPhasePoduct = projectPlanPhaseProductGroup.FirstOrDefault();

        //                    if (firstProjectPlanPhasePoduct == null)
        //                        continue;

        //                    var remaining = projectPlanPhaseProductGroup.Sum(p => p.Quantity) - registeredEntries
        //                                                                                                        .Where(e => e.ProductId == firstProjectPlanPhasePoduct.ProductId
        //                                                                                                            && e.ProjectPlanPhaseId == firstProjectPlanPhasePoduct.ProjectPlanPhaseId)
        //                                                                                                        .Sum(e => e.Quantity);

        //                    if (remaining <= 0)
        //                        continue;

        //                    var createNewProductsheetEntryModel = Mapper.DynamicMap<CreateNewProductsheetEntryModel>(firstProjectPlanPhasePoduct);

        //                    var firstOfMonth = new DateTime(year.GetValueOrDefault(), month.GetValueOrDefault(), 1);

        //                    createNewProductsheetEntryModel.UserId = token.UserId;
        //                    createNewProductsheetEntryModel.ProjectId = id;
        //                    createNewProductsheetEntryModel.InvoiceStatusCode = (int)InvoiceStatusType.Draft;
        //                    createNewProductsheetEntryModel.ProjectPlanPhaseName = activeProjectPlanPhase.Name;
        //                    createNewProductsheetEntryModel.Total = projectPlanPhaseProductGroup.Sum(p => p.Quantity);
        //                    createNewProductsheetEntryModel.Remaining = remaining;
        //                    createNewProductsheetEntryModel.Quantity = createNewProductsheetEntryModel.Remaining;
        //                    createNewProductsheetEntryModel.InvoiceAmount = remaining * createNewProductsheetEntryModel.UnitPrice;

        //                    var remarksBuilder = new StringBuilder();
        //                    for (int i = 0; i < projectPlanPhaseProductGroup.Count(); i++)
        //                    {
        //                        remarksBuilder.AppendLine(projectPlanPhaseProductGroup.ElementAt(i).Notes);
        //                        if(i < (projectPlanPhaseProductGroup.Count() - 1))
        //                        {
        //                            //remarksBuilder.AppendLine("<br/>");
        //                        }

        //                    }
        //                    createNewProductsheetEntryModel.InvoiceRemarks = remarksBuilder.ToString();

        //                    createNewProductsheetEntryModel.Date = activeProjectPlanPhase.StartDate > firstOfMonth
        //                                                                ? activeProjectPlanPhase.StartDate
        //                                                                : firstOfMonth;
        //                    model.ProjectPlanPhaseProducts.Add(createNewProductsheetEntryModel);
        //                }
        //            }

        //            return View(model);
        //        }
        //        catch (Exception exception)
        //        {
        //            LogManager.LogError(exception);
        //            return HandleError(exception);
        //        }
        //    }
        //}

        //[HttpPost]
        //public ActionResult EditProductsheet(EditProductsheetActionModel model)
        //{
        //    using (DurationLog.Create())
        //    {
        //        try
        //        {
        //            var saveProductsheetEntriesRequest = new SaveProductsheetEntriesRequest();

        //            if (model.ProjectPlanPhaseProducts != null)
        //                saveProductsheetEntriesRequest.CreateProductsheetEntries = model.ProjectPlanPhaseProducts
        //                    .Where(pppp => pppp.IsChecked)
        //                    .Select(Mapper.DynamicMap<CreateNewProductsheetEntryRequest>)
        //                    .ToList();

        //            if (model.Entries != null)
        //                saveProductsheetEntriesRequest.UpdateProductsheetEntries = model.Entries
        //                    .Select(Mapper.DynamicMap<UpdateProductsheetEntryRequest>)
        //                    .ToList();

        //            this.InvokeService<IProjectManagementCommandService>(service => service.SaveProductsheetEntries(saveProductsheetEntriesRequest));

        //            return RedirectToAction("EditProductsheet", new { id = model.ProjectId, year = model.Year, month = model.Month });
        //        }
        //        catch (Exception exception)
        //        {
        //            LogManager.LogError(exception);
        //            return HandleError(exception);
        //        }
        //    }
        //}

        public ActionResult ProjectPlanPhaseActivityProfiles(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPlanPhase = this.InvokeService<IProjectManagementQueryService, ProjectPlanPhaseView>(service => service.RetrieveProjectPlanPhaseDetail(id));
                    var activities = this.InvokeService<ISupplyChainManagementQueryService, List<ActivityView>>(service => service.ListActivities(new ListActivitiesRequest { ProjectPlanId = projectPlanPhase.ProjectPlanId }));

                    var phaseActivities = projectPlanPhase.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseActivityView>();

                    return PartialView(activities.SelectMany(a => a.ActivityProfiles).Where(ap => phaseActivities.Any(pppa => pppa.ActivityId == ap.ActivityId && pppa.ProfileId == ap.ProfileId)));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult DeleteTimesheetEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteTimesheetEntry(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteProductsheetEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProductsheetEntry(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        #endregion

        #region ProjectTimesheet

        public ActionResult ProjectTimesheet(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new ProjectTimesheetActionModel();
                    model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));
                    model.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    model.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);

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
        public ActionResult ProjectTimesheet(ProjectTimesheetsActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (model.Timesheets == null || model.Timesheets.Count == 0)
                        return RedirectToAction("ProjectTimesheet", new { id = model.ProjectId });
                        
                    var request = new SaveTimesheetEntriesRequest();

                    request.UpdateTimesheetEntries = model.Timesheets.SelectMany(t => t.Entries).Select(Mapper.DynamicMap<UpdateTimesheetEntryRequest>).ToList();

                    this.InvokeService<IProjectManagementCommandService>(service => service.SaveTimesheetEntries(request));

                    return RedirectToAction("ProjectTimesheet", new { id = model.ProjectId });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }


        public ActionResult ProjectTimesheets(Guid id, [ModelBinder(typeof(CustomDateBinder))]DateTime dateFrom, [ModelBinder(typeof(CustomDateBinder))]DateTime dateTo)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //var dateFrom = new DateTime(dateFromYear, dateFromMonth, dateFromDay);
                    //var dateTo = new DateTime(dateToYear, dateToMonth, dateToDay);
                    var timesheets = this.InvokeService<IProjectManagementQueryService, List<TimesheetEntryView>>(service => service.ListProjectTimesheets(id, dateFrom, dateTo));

                    var model = new ProjectTimesheetsActionModel();
                    model.ProjectId = id;
                    model.Timesheets = new List<EditTimesheetModel>();
                    foreach (var userTimesheets in timesheets.Where(ts => CheckStatus(ts.Status)).OrderBy(ts => ts.User.FullName).GroupBy(ts => ts.User))
                    {
                        var timesheet = new EditTimesheetModel();

                        timesheet.User = userTimesheets.Key;
                        timesheet.Entries = userTimesheets.Select(Mapper.DynamicMap<EditTimesheetEntryModel>).ToList();

                        model.Timesheets.Add(timesheet);
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

        private bool CheckStatus(InvoiceStatusType status)
        {
            switch (status)
            {
                case InvoiceStatusType.Draft:
                    return false;
                default:
                    return true;
            }
        }

        #endregion

        public FileResult Download(string FileName, string FullPath)
        {
            //return File("~/Uploads/" + DirectoryName + "/" + FileName, System.Net.Mime.MediaTypeNames.Application.Octet);
            return File(FullPath, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
        }

        public ActionResult UploadFile(Guid id)
        {
            var filePath = Path.Combine(Server.MapPath("~/Uploads/"), id.ToString());

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            DirectoryInfo dirinfo = new DirectoryInfo(filePath);
            List<FileInfo> filelist = dirinfo.GetFiles().ToList();

            return PartialView(filelist);
        }

        [HttpPost]
        public ActionResult UploadFile(Guid id, IEnumerable<HttpPostedFileBase> files)
        {          
            var filePath = Path.Combine(Server.MapPath("~/Uploads/"), id.ToString());

            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {                    

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    
                    file.SaveAs(Path.Combine(filePath, Path.GetFileName(file.FileName)));
                }
            }

            DirectoryInfo dirinfo = new DirectoryInfo(filePath);
            List<FileInfo> filelist = dirinfo.GetFiles().ToList();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult AddActivityDetailTrainingCandidate(Guid activityDetailTrainingId, int appointmentId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var activityDetailTraining = this.InvokeService<ISupplyChainManagementQueryService, ActivityDetailView>(service => service.RetrieveActivityDetail(activityDetailTrainingId));
                    var appointment = this.InvokeService<ICustomerRelationshipManagementQueryService, CrmAppointmentView>(service => service.RetrieveCrmAppointment(appointmentId));

                    var model = new EditActivityDetailTrainingCandidateModel();

                    model.ActivityDetailTrainingName = activityDetailTraining.Name;
                    model.AppointmentDescription = appointment.Description;

                    model.ActivityDetailTrainingId = activityDetailTrainingId;
                    model.CrmAppointmentId = appointmentId;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult CopyActivity(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.CopyActivity(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult GenerateProjectPlanReport(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Excel,
                        ReportName = "/QPlanet/ProjectReporting/ReportingProjectPlanDetail",
                        Parameters = new Dictionary<string, string>
                            {
                                {"ProjectPlanId", id.ToString()}
                            }
                    };

                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}.xls", id));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditActivityDetailGeneric(EditActivityDetailModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateActivityDetailRequest = Mapper.Map<UpdateActivityDetailRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateActivityDetail(updateActivityDetailRequest));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ProductDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new ProductDetailActionModel();
                    model.Project = (ConsultancyProjectView)this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));
                    model.IsCurrentUserProjectManager = model.Project.ProjectManagerId == GetAuthenticationToken().UserId;
                    model.IsCurrentUserCustomerAssistant = model.Project.CustomerAssistantId == GetAuthenticationToken().UserId;

                    model.Products = this.InvokeService<ISupplyChainManagementQueryService, List<ProductView>>(service => service.ListProducts(id));

                    model.Products.Sort((a, b) => String.Compare(a.ProductTypeName, b.ProductTypeName, StringComparison.Ordinal));

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var product = this.InvokeService<ISupplyChainManagementQueryService, ProductView>(service => service.RetrieveProduct(id));

                    var model = Mapper.Map<EditProductModel>(product);

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
        public ActionResult EditProduct(EditProductModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateProductRequest = Mapper.Map<UpdateProductRequest>(model);
                    this.InvokeService<ISupplyChainManagementCommandService>(service => service.UpdateProduct(updateProductRequest));
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