using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Dim;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminProject;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Infrastructure.Enums;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminProjectController : AdminController
    {
        #region Index
        public ActionResult Index()
        {
            try
            {
                var model = new IndexModel
                    {
                        ProjectRolesForQuintessence =
                            this.InvokeService<IProjectManagementQueryService, List<ProjectRoleView>>(
                                service => service.ListProjectRolesForQuintessence()),
                        ProjectRolesForContacts =
                            this.InvokeService<IProjectManagementQueryService, List<ProjectRoleView>>(
                                service => service.ListProjectRolesForContacts())
                    };
                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        public ActionResult Subcategories()
        {
            try
            {
                var subcategories = this.InvokeService<IProjectManagementQueryService, List<ProjectTypeCategoryView>>(
                    service => service.ListSubcategories());
                return PartialView("Subcategories", subcategories);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }
        #endregion

        #region EditForQuintessence
        public ActionResult EditForQuintessence(Guid id)
        {
            try
            {
                var projectRole = this.InvokeService<IProjectManagementQueryService, ProjectRoleView>(
                    service => service.RetrieveProjectRole(id));

                var model = Mapper.Map<EditProjectRoleModel>(projectRole);

                return View("EditProjectRole", model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditForQuintessence(EditProjectRoleModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.Map<UpdateProjectRoleRequest>(model);

                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectRole(request));

                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                    return View("EditProjectRole", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }
        #endregion

        #region CreateForQuintessence
        public ActionResult CreateForQuintessence()
        {
            ViewBag.ShowDictionaryLevels = false;
            return View("EditProjectRole", new EditProjectRoleModel());
        }

        [HttpPost]
        public ActionResult CreateForQuintessence(EditProjectRoleModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<CreateProjectRoleRequest>(model);
                        var projectRoleId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateProjectRole(request));

                        return RedirectToAction("EditForQuintessence", new { id = projectRoleId });
                    }
                    return View("EditProjectRole", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => RedirectToAction("CreateForQuintessence"));
                }
            }
        }
        #endregion

        #region EditForContact
        public ActionResult EditForContact(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectRole = this.InvokeService<IProjectManagementQueryService, ProjectRoleView>(service => service.RetrieveProjectRole(id));

                    var model = Mapper.Map<EditProjectRoleModel>(projectRole);
                    model.IsContactRequired = true;

                    return View("EditProjectRole", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditForContact(EditProjectRoleModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.Map<UpdateProjectRoleRequest>(model);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectRole(request));

                        return RedirectToAction("EditForContact", model);
                    }
                    return View("EditProjectRole", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => RedirectToAction("EditForContact", new { id = model.Id }));
                }
            }
        }
        #endregion

        #region CreateForContact
        public ActionResult CreateForContact()
        {
            return View("EditProjectRole", new EditProjectRoleModel { IsContactRequired = true });
        }

        [HttpPost]
        public ActionResult CreateForContact(EditProjectRoleModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<CreateProjectRoleRequest>(model);

                        var projectRoleId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateProjectRole(request));

                        return RedirectToAction("EditForContact", new { id = projectRoleId });
                    }
                    return View("EditProjectRole", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => RedirectToAction("CreateForContact"));
                }
            }
        }
        #endregion

        #region Index ProjectRoleDictionaryLevels
        public ActionResult ProjectRoleDictionaryLevels(Guid id)
        {
            try
            {
                var token = GetAuthenticationToken();
                var request = new ListProjectRoleDictionaryIndicatorsRequest
                    {
                        ProjectRoleId = id,
                        LanguageId = token.User.LanguageId
                    };

                var projectRoleDictionaryLevels = this.InvokeService<IProjectManagementQueryService, List<ProjectRoleDictionaryLevelView>>(service => service.ListProjectRoleDictionaryIndicators(request));

                return PartialView(projectRoleDictionaryLevels
                    .OrderBy(e => e.DictionaryName)
                    .ThenBy(e => e.DictionaryClusterOrder)
                    .ThenBy(e => e.DictionaryCompetenceOrder)
                    .ThenBy(e => e.DictionaryLevelLevel)
                    .ThenBy(e => e.DictionaryIndicatorOrder));
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }
        #endregion

        #region UnlinkProjectRoleDictionaryLevel
        public ActionResult UnlinkProjectRoleDictionaryLevel(Guid projectRoleId, Guid dictionaryLevelId)
        {
            try
            {
                this.InvokeService<IProjectManagementCommandService>(
                            service => service.UnlinkProjectRoleDictionaryLevel(projectRoleId, dictionaryLevelId));
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }
        #endregion

        #region List all dictionary levels
        public ActionResult DictionaryLevels(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListDetailedDictionariesRequest { TillIndicators = true, ProjectRoleId = id };

                    var detailedDictionaries =
                            this.InvokeService<IDictionaryManagementQueryService, List<DictionaryView>>(
                            service => service.ListDetailedDictionaries(request)).OrderBy(d => d.Name);

                    var model = new DictionaryLevelsModel
                        {
                            ProjectRoleId = id,
                            Dictionaries = detailedDictionaries.OrderBy(d => d.Name).Select(Mapper.DynamicMap<DictionaryModel>).ToList()
                        };

                    model.Dictionaries.ForEach(dictionary =>
                        {
                            dictionary.DictionaryClusters.Sort((a, b) => a.Order.CompareTo(b.Order));
                            dictionary.DictionaryClusters.ForEach(
                                cluster =>
                                {
                                    cluster.DictionaryCompetences.Sort((a, b) => a.Order.CompareTo(b.Order));
                                    cluster.DictionaryCompetences.ForEach(competence =>
                                        {
                                            competence.DictionaryLevels.Sort((a, b) => a.Level.CompareTo(b.Level));
                                            competence.DictionaryLevels.ForEach(l =>
                                                {
                                                    l.DictionaryIndicators.Sort((a, b) => a.Order.CompareTo(b.Order));
                                                });
                                        });
                                });
                        });
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }

        }

        public ActionResult MarkDictionaryIndicatorAsStandard(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.MarkDictionaryIndicatorAsStandard(projectRoleId, dictionaryIndicatorId));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult MarkDictionaryIndicatorAsDistinctive(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.MarkDictionaryIndicatorAsDistinctive(projectRoleId, dictionaryIndicatorId));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteProjectRoleDictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectRoleDictionaryIndicator(projectRoleId, dictionaryIndicatorId));
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

        #region Autocomplete for contacts
        public ActionResult SearchContact(string term)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var contacts = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmContactView>>
                        (
                            service => service.SearchContacts(new SearchContactRequest { CustomerName = term }));

                    var contactJson = contacts.Select(u => new { label = u.FullName, value = u.Id }).ToList();

                    var total = contactJson.Count;

                    var limitedList = contactJson.Take(10).ToList();

                    if (total > 10)
                        limitedList.Add(new { label = string.Format("... Showing 10 of {0}", total), value = 0 });

                    var result = Json(limitedList, JsonRequestBehavior.AllowGet);
                    return result;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }
        #endregion

        #region LinkProjectRoleDictionaryLevels
        [HttpPost]
        public ActionResult LinkProjectRoleDictionaryLevels(FormCollection form)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //Somehow, parsing to a DictionaryLevelsModel didn't work. To save time, use FormCollection to determine dictionary level id's
                    var selectedLevelIds = new List<Guid>();
                    var projectRoleId = new Guid(form["ProjectRoleId"]);

                    for (int i = 0; i < form.AllKeys.Length; i++)
                    {
                        if (form.AllKeys[i].EndsWith("IsChecked"))
                        {
                            var isChecked = form[form.AllKeys[i]] == "true" || form[form.AllKeys[i]] == "true,false";

                            if (isChecked)
                            {
                                selectedLevelIds.Add(new Guid(form[form.AllKeys[i - 1]]));
                            }
                        }
                    }

                    this.InvokeService<IProjectManagementCommandService>(service => service.LinkProjectRole2DictionaryLevels(projectRoleId, selectedLevelIds));
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

        #region CopyProjectRole
        public ActionResult CopyProjectRole(Guid id)
        {
            var model = new CopyProjectRoleModel
                {
                    ProjectRoleId = id
                };
            return PartialView("CopyProjectRole", model);
        }

        [HttpPost]
        public ActionResult CopyProjectRole(CopyProjectRoleModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = new CopyProjectRoleRequest
                            {
                                ProjectRoleToCopyId = model.ProjectRoleId,
                                ContactId = model.ContactId
                            };
                        var projectRoleCopyId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CopyProjectRole(request));

                        return Json(new { projectRoleCopyId }, JsonRequestBehavior.AllowGet);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }

        }

        #endregion

        #region Create subcategory
        public ActionResult CreateSubcategory()
        {
            var model = new EditSubcategoryModel();
            ViewBag.ViewMode = ViewMode.Create;
            model.CrmTaskId = 0;
            model.Execution = 0;
            model.Color = "0";
            return View("EditSubcategory", model);
        }

        [HttpPost]
        public ActionResult CreateSubcategory(EditSubcategoryModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    ViewBag.ViewMode = ViewMode.Create;
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<CreateSubcategoryRequest>(model);

                        var subcategoryId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateSubcategory(request));

                        return RedirectToAction("EditSubcategory", new { id = subcategoryId });
                    }
                    return View("EditSubcategory", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => RedirectToAction("CreateSubcategory"));
                }
            }
        }
        #endregion

        #region Edit subcategory
        public ActionResult EditSubcategory(Guid id)
        {
            try
            {
                using (DurationLog.Create())
                {
                    var model = Mapper.Map<EditSubcategoryModel>(this.InvokeService<IProjectManagementQueryService, ProjectTypeCategoryView>(service => service.RetrieveProjectTypeCategory(new RetrieveProjectTypeCategoryRequest { Id = id })));

                    ViewBag.ViewMode = ViewMode.Edit;
                    model.Color = model.Color ?? "0";
                    model.CrmTaskId = model.CrmTaskId ?? 0;
                    model.Execution = model.Execution ?? 0;
                    return View("EditSubcategory", model);
                }

            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditSubcategory(EditSubcategoryModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    ViewBag.ViewMode = ViewMode.Edit;
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<UpdateSubcategoryRequest>(model);

                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateSubcategory(request));
                    }
                    return View("EditSubcategory", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, () => RedirectToAction("CreateSubcategory"));
                }
            }
        }
        #endregion

        #region ProjectTypeCategoryUnitPrices
        public ActionResult ProjectTypeCategoryUnitPrices()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response =
                        this.InvokeService<IProjectManagementQueryService, ProjectTypeCategoryUnitPriceOverviewResponse>(
                            service => service.ListProjectTypeCategoryUnitPriceOverview());

                    var model = new ProjectTypeCategoryUnitPricesActionModel();
                    model.ProjectTypeMainCategories = response.ProjectTypeCategories.Where(ptc => ptc.IsMain).Select(Mapper.DynamicMap<EditProjectTypeCategoryModel>).ToList();
                    model.ProjectTypeSubCategories = response.ProjectTypeCategories.Where(ptc => !ptc.IsMain).Select(Mapper.DynamicMap<EditProjectTypeCategoryModel>).ToList();

                    model.ProjectTypeCategoryLevels = response.ProjectTypeCategoryLevels;

                    foreach (var level in response.ProjectTypeCategoryLevels)
                    {
                        model.ProjectTypeMainCategories.ForEach(ptc => ptc.ProjectTypeCategoryLevels.Add(Mapper.DynamicMap<EditProjectTypeCategoryLevelModel>(level)));
                        model.ProjectTypeSubCategories.ForEach(ptc => ptc.ProjectTypeCategoryLevels.Add(Mapper.DynamicMap<EditProjectTypeCategoryLevelModel>(level)));
                    }

                    foreach (var mainCategory in model.ProjectTypeMainCategories)
                    {
                        foreach (var level in mainCategory.ProjectTypeCategoryLevels)
                        {
                            var unitPrice = response.ProjectTypeCategoryUnitPrices.FirstOrDefault(ptcup => ptcup.ProjectTypeCategoryId == mainCategory.Id && ptcup.ProjectTypeCategoryLevelId == level.Id);
                            level.ProjectTypeCategoryUnitPrice = unitPrice != null ? Mapper.Map<EditProjectTypeCategoryUnitPriceModel>(unitPrice) : new EditProjectTypeCategoryUnitPriceModel();
                        }
                    }

                    foreach (var subCategory in model.ProjectTypeSubCategories)
                    {
                        foreach (var level in subCategory.ProjectTypeCategoryLevels)
                        {
                            var unitPrice = response.ProjectTypeCategoryUnitPrices.FirstOrDefault(ptcup => ptcup.ProjectTypeCategoryId == subCategory.Id && ptcup.ProjectTypeCategoryLevelId == level.Id);
                            level.ProjectTypeCategoryUnitPrice = unitPrice != null ? Mapper.Map<EditProjectTypeCategoryUnitPriceModel>(unitPrice) : new EditProjectTypeCategoryUnitPriceModel();
                        }
                    }

                    return PartialView("ProjectTypeCategoryUnitPrices", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        [HttpPost]
        public ActionResult ProjectTypeCategoryUnitPrices(ProjectTypeCategoryUnitPricesActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {

                    var requests = new List<UpdateProjectTypeCategoryUnitPriceRequest>();
                    model.ProjectTypeMainCategories.ForEach(ptmc => ptmc.ProjectTypeCategoryLevels.ForEach(ptcl => requests.Add(new UpdateProjectTypeCategoryUnitPriceRequest
                            {
                                UnitPrice = ptcl.ProjectTypeCategoryUnitPrice.UnitPrice,
                                ProjectTypeCategoryId = ptmc.Id,
                                ProjectTypeCategoryLevelId = ptcl.Id
                            })));

                    model.ProjectTypeSubCategories.ForEach(ptsc => ptsc.ProjectTypeCategoryLevels.ForEach(ptcl => requests.Add(new UpdateProjectTypeCategoryUnitPriceRequest
                    {
                        UnitPrice = ptcl.ProjectTypeCategoryUnitPrice.UnitPrice,
                        ProjectTypeCategoryId = ptsc.Id,
                        ProjectTypeCategoryLevelId = ptcl.Id
                    })));

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectTypeCategoryUnitPrices(requests));

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
    }
}
