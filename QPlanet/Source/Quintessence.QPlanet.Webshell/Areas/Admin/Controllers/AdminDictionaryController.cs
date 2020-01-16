using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Dim;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminDictionary;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminDictionaryController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Quintessences the dictionaries.
        /// </summary>
        /// <returns></returns>
        public ActionResult QuintessenceDictionaries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaries = this.InvokeService<IDictionaryManagementQueryService, List<DictionaryAdminView>>(service => service.ListQuintessenceAdminDictionaries());

                    return PartialView(dictionaries);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        /// <summary>
        /// Customers the dictionaries.
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerDictionaries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaries = this.InvokeService<IDictionaryManagementQueryService, List<DictionaryAdminView>>(service => service.ListCustomerAdminDictionaries());

                    return PartialView(dictionaries);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        /// <summary>
        /// Imports the dictionaries.
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportDictionaries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaries = this.InvokeService<IDictionaryManagementQueryService, List<DictionaryImportFileInfoView>>(service => service.ListImportDictionaries());

                    return PartialView(dictionaries);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        /// <summary>
        /// Edits the dictionary.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult EditDictionary(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //Make sure the dictionary is tiptop
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.ValidateDictionary(id));
                    var dictionary = this.InvokeService<IDictionaryManagementQueryService, DictionaryAdminView>(service => service.RetrieveDictionaryAdmin(id));
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());
                    languages.Sort((a, b) => a.Id.CompareTo(b.Id));

                    var model = Mapper.Map<EditDictionaryModel>(dictionary);
                    model.Languages = languages;

                    model.DictionaryClusters.Sort((a, b) => String.Compare(a.Name, b.Name, StringComparison.InvariantCulture));

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        /// <summary>
        /// Edits the dictionary.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDictionary(EditDictionaryModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateDictionaryRequest>(model);
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.UpdateDictionary(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditDictionaryCluster(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryCluster = this.InvokeService<IDictionaryManagementQueryService, DictionaryClusterAdminView>(service => service.RetrieveDictionaryClusterAdmin(id));

                    var model = Mapper.Map<EditDictionaryClusterModel>(dictionaryCluster);

                    model.DictionaryCompetences.Sort((a, b) => String.Compare(a.Name, b.Name, StringComparison.InvariantCulture));
                    model.DictionaryClusterTranslations.Sort((a, b) => a.LanguageId.CompareTo(b.LanguageId));

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
        public ActionResult EditDictionaryCluster(EditDictionaryClusterModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateDictionaryClusterRequest>(model);
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.UpdateDictionaryCluster(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditDictionaryCompetence(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryCompetence = this.InvokeService<IDictionaryManagementQueryService, DictionaryCompetenceAdminView>(service => service.RetrieveDictionaryCompetenceAdmin(id));

                    var model = Mapper.Map<EditDictionaryCompetenceModel>(dictionaryCompetence);

                    model.DictionaryLevels.Sort((a, b) => String.Compare(a.Name, b.Name, StringComparison.InvariantCulture));
                    model.DictionaryCompetenceTranslations.Sort((a, b) => a.LanguageId.CompareTo(b.LanguageId));

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
        public ActionResult EditDictionaryCompetence(EditDictionaryCompetenceModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateDictionaryCompetenceRequest>(model);
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.UpdateDictionaryCompetence(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditDictionaryLevel(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryLevel = this.InvokeService<IDictionaryManagementQueryService, DictionaryLevelAdminView>(service => service.RetrieveDictionaryLevelAdmin(id));

                    var model = Mapper.Map<EditDictionaryLevelModel>(dictionaryLevel);

                    model.DictionaryIndicators.Sort((a, b) => String.Compare(a.Name, b.Name, StringComparison.InvariantCulture));
                    model.DictionaryLevelTranslations.Sort((a, b) => a.LanguageId.CompareTo(b.LanguageId));

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
        public ActionResult EditDictionaryLevel(EditDictionaryLevelModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateDictionaryLevelRequest>(model);
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.UpdateDictionaryLevel(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditDictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryIndicator = this.InvokeService<IDictionaryManagementQueryService, DictionaryIndicatorAdminView>(service => service.RetrieveDictionaryIndicatorAdmin(id));

                    var model = Mapper.Map<EditDictionaryIndicatorModel>(dictionaryIndicator);

                    model.DictionaryIndicatorTranslations.Sort((a, b) => a.LanguageId.CompareTo(b.LanguageId));

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
        public ActionResult EditDictionaryIndicator(EditDictionaryIndicatorModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateDictionaryIndicatorRequest>(model);
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.UpdateDictionaryIndicator(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddDictionaryCluster(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionary = this.InvokeService<IDictionaryManagementQueryService, DictionaryAdminView>(service => service.RetrieveDictionaryAdmin(id));
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    var model = new CreateNewDictionaryClusterModel();
                    model.DictionaryName = dictionary.Name;
                    model.DictionaryId = id;
                    model.Color = "#FFFFFF";
                    model.ImageName = "SomeImageName";
                    model.Order = (dictionary.DictionaryClusters == null || dictionary.DictionaryClusters.Count == 0) ? 10 : dictionary.DictionaryClusters.Max(dc => dc.Order) + 10;
                    model.Languages = languages;
                    model.LanguageId = languages.Min(l => l.Id);

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
        public ActionResult AddDictionaryCluster(CreateNewDictionaryClusterModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<CreateNewDictionaryClusterRequest>(model);

                    this.InvokeService<IDictionaryManagementCommandService>(service => service.CreateNewDictionaryCluster(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddDictionaryCompetence(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryCluster = this.InvokeService<IDictionaryManagementQueryService, DictionaryClusterAdminView>(service => service.RetrieveDictionaryClusterAdmin(id));
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    var model = new CreateNewDictionaryCompetenceModel();
                    model.DictionaryName = dictionaryCluster.DictionaryName;
                    model.DictionaryClusterName = dictionaryCluster.Name;
                    model.DictionaryClusterId = id;
                    model.Languages = languages;
                    model.LanguageId = languages.Min(l => l.Id);
                    model.Order = (dictionaryCluster.DictionaryCompetences == null || dictionaryCluster.DictionaryCompetences.Count == 0) ? 10 : dictionaryCluster.DictionaryCompetences.Max(dc => dc.Order) + 10;

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
        public ActionResult AddDictionaryCompetence(CreateNewDictionaryCompetenceModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<CreateNewDictionaryCompetenceRequest>(model);

                    this.InvokeService<IDictionaryManagementCommandService>(service => service.CreateNewDictionaryCompetence(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddDictionaryLevel(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryCompetence = this.InvokeService<IDictionaryManagementQueryService, DictionaryCompetenceAdminView>(service => service.RetrieveDictionaryCompetenceAdmin(id));
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    var model = new CreateNewDictionaryLevelModel();
                    model.DictionaryName = dictionaryCompetence.DictionaryName;
                    model.DictionaryClusterName = dictionaryCompetence.DictionaryClusterName;
                    model.DictionaryCompetenceName = dictionaryCompetence.Name;
                    model.DictionaryCompetenceId = id;
                    model.Languages = languages;
                    model.LanguageId = languages.Min(l => l.Id);
                    model.Level = dictionaryCompetence.DictionaryLevels.Count + 1;

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
        public ActionResult AddDictionaryLevel(CreateNewDictionaryLevelModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<CreateNewDictionaryLevelRequest>(model);

                    this.InvokeService<IDictionaryManagementCommandService>(service => service.CreateNewDictionaryLevel(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddDictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryLevel = this.InvokeService<IDictionaryManagementQueryService, DictionaryLevelAdminView>(service => service.RetrieveDictionaryLevelAdmin(id));
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    var model = new CreateNewDictionaryIndicatorModel();
                    model.DictionaryName = dictionaryLevel.DictionaryName;
                    model.DictionaryClusterName = dictionaryLevel.DictionaryClusterName;
                    model.DictionaryCompetenceName = dictionaryLevel.DictionaryCompetenceName;
                    model.DictionaryLevelName = dictionaryLevel.Level.ToString(CultureInfo.InvariantCulture);
                    model.DictionaryLevelId = id;
                    model.Order = (dictionaryLevel.DictionaryIndicators == null || dictionaryLevel.DictionaryIndicators.Count == 0) ? 10 : dictionaryLevel.DictionaryIndicators.Max(dc => dc.Order) + 10;
                    model.Languages = languages;
                    model.LanguageId = languages.Min(l => l.Id);

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
        public ActionResult AddDictionaryIndicator(CreateNewDictionaryIndicatorModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<CreateNewDictionaryIndicatorRequest>(model);

                    this.InvokeService<IDictionaryManagementCommandService>(service => service.CreateNewDictionaryIndicator(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteDictionaryCluster(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.DeleteDictionaryCluster(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteDictionaryCompetence(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.DeleteDictionaryCompetence(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteDictionaryLevel(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.DeleteDictionaryLevel(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteDictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.DeleteDictionaryIndicator(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult ImportDictionary(string name)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionary = this.InvokeService<IDictionaryManagementQueryService, DictionaryImportView>(service => service.AnalyseDictionary(name));

                    var model = new ImportDictionaryActionModel();

                    model.Dictionary = Mapper.Map<EditImportDictionaryModel>(dictionary);

                    model.Contacts = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmContactView>>(service => service.ListContacts());
                    model.Languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    model.Languages.Sort((a, b) => a.Id.CompareTo(b.Id));

                    //Check for missing languages (Severity: information)
                    foreach (var language in model.Languages.Where(language => model.Dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryClusterTranslations).All(dct => dct.LanguageId != language.Id)))
                        model.Messages.Add(new ImportDictionaryMessageActionModel{Severity = ImportDictionaryMessageSeverity.Info,Code = string.Format("MISSLANG{0}", language.Code), Message = string.Format("Dictionary does not have translations for language {0}", language.Name)});

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult ImportDictionary(ImportDictionaryActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<ImportDictionaryRequest>(model.Dictionary);
                    var dictionaryId = this.InvokeService<IDictionaryManagementCommandService, Guid>(service => service.ImportDictionary(request));

                    return RedirectToAction("EditDictionary", new { id = dictionaryId });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult NormalizeDictionaryClusterOrder(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.NormalizeDictionaryClusterOrder(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult NormalizeDictionaryCompetenceOrder(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.NormalizeDictionaryCompetenceOrder(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult NormalizeDictionaryIndicatorOrder(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IDictionaryManagementCommandService>(service => service.NormalizeDictionaryIndicatorOrder(id));
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
