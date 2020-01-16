using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Tools;
using Quintessence.QPlanet.ViewModel.Sim;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminSimulation;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Models.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminSimulationController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimulationSets(DataTableParameterModel parameters)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListSimulationSetsRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                    var response = this.InvokeService<ISimulationManagementQueryService, ListSimulationSetsResponse>(service => service.ListSimulationSets(request));
                    var simulationSets = response.SimulationSets;

                    var model = new
                    {
                        sEcho = parameters.sEcho,
                        iTotalRecords = response.DataTablePaging.TotalRecords,
                        iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                        aaData = simulationSets
                            .OrderBy(ss => ss.Name)
                            .Select(u => new[] { u.Name, u.Id.ToString() })
                            .ToList()
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

        public ActionResult SimulationDepartments(DataTableParameterModel parameters)
        {
            try
            {
                var request = new ListSimulationDepartmentsRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                var response = this.InvokeService<ISimulationManagementQueryService, ListSimulationDepartmentsResponse>(service => service.ListSimulationDepartments(request));
                var simulationDepartments = response.SimulationDepartments;

                var model = new
                {
                    sEcho = parameters.sEcho,
                    iTotalRecords = response.DataTablePaging.TotalRecords,
                    iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                    aaData = simulationDepartments
                        .OrderBy(ss => ss.Name)
                        .Select(u => new[] { u.Name, u.Id.ToString() })
                        .ToList()
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult SimulationLevels(DataTableParameterModel parameters)
        {
            try
            {
                var request = new ListSimulationLevelsRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                var response = this.InvokeService<ISimulationManagementQueryService, ListSimulationLevelsResponse>(service => service.ListSimulationLevels(request));
                var simulationLevels = response.SimulationLevels;

                var model = new
                {
                    sEcho = parameters.sEcho,
                    iTotalRecords = response.DataTablePaging.TotalRecords,
                    iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                    aaData = simulationLevels
                        .OrderBy(ss => ss.Name)
                        .Select(u => new[] { u.Name, u.Id.ToString() })
                        .ToList()
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult Simulations(DataTableParameterModel parameters)
        {
            try
            {
                var request = new ListSimulationsRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                var response = this.InvokeService<ISimulationManagementQueryService, ListSimulationsResponse>(service => service.ListSimulations(request));
                var simulations = response.Simulations;

                var model = new
                {
                    sEcho = parameters.sEcho,
                    iTotalRecords = response.DataTablePaging.TotalRecords,
                    iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                    aaData = simulations
                        .OrderBy(ss => ss.Name)
                        .Select(u => new[] { u.Name, u.Id.ToString() })
                        .ToList()
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult SimulationMatrix(DataTableParameterModel parameters)
        {
            try
            {
                var request = new ListSimulationMatrixEntriesRequest { DataTablePaging = Mapper.Map<DataTablePaging>(parameters) };
                var response = this.InvokeService<ISimulationManagementQueryService, ListSimulationMatrixEntriesResponse>(service => service.ListSimulationMatrixEntries(request));
                var simulationMatrixEntries = response.SimulationMatrixEntries;

                var model = new
                {
                    sEcho = parameters.sEcho,
                    iTotalRecords = response.DataTablePaging.TotalRecords,
                    iTotalDisplayRecords = response.DataTablePaging.TotalDisplayRecords,
                    aaData = simulationMatrixEntries.OrderBy(ss => ss.SimulationSetName)
                        .ThenBy(ss => ss.SimulationDepartmentName)
                        .ThenBy(ss => ss.SimulationLevelName)
                        .ThenBy(ss => ss.SimulationName)
                        .Select(u => new[] { u.SimulationSetName, u.SimulationDepartmentName, u.SimulationLevelName, u.SimulationName, u.Preparation.ToString(CultureInfo.InvariantCulture), u.Execution.ToString(CultureInfo.InvariantCulture), u.LanguageNames, u.Id.ToString() })
                        .ToList()
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteSimulationSet(Guid id)
        {
            try
            {
                this.InvokeService<ISimulationManagementCommandService>(service => service.DeleteSimulationSet(id));
                return new HttpStatusCodeResult(200);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteSimulationDepartment(Guid id)
        {
            try
            {
                this.InvokeService<ISimulationManagementCommandService>(service => service.DeleteSimulationDepartment(id));
                return new HttpStatusCodeResult(200);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteSimulationLevel(Guid id)
        {
            try
            {
                this.InvokeService<ISimulationManagementCommandService>(service => service.DeleteSimulationLevel(id));
                return new HttpStatusCodeResult(200);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteSimulation(Guid id)
        {
            try
            {
                this.InvokeService<ISimulationManagementCommandService>(service => service.DeleteSimulation(id));
                return new HttpStatusCodeResult(200);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteSimulationMatrixEntry(Guid id)
        {
            try
            {
                this.InvokeService<ISimulationManagementCommandService>(service => service.DeleteSimulationCombination(id));
                return new HttpStatusCodeResult(200);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult EditSimulationSet(Guid id)
        {
            try
            {
                var simulationSet = this.InvokeService<ISimulationManagementQueryService, SimulationSetView>(service => service.RetrieveSimulationSet(id));
                var model = Mapper.DynamicMap<EditSimulationSetModel>(simulationSet);

                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditSimulationSet(EditSimulationSetModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<UpdateSimulationSetRequest>(model);

                    this.InvokeService<ISimulationManagementCommandService>(
                        service => service.UpdateSimulationSet(request));

                    return RedirectToAction("EditSimulationSet", new { model.Id });
                }
                //fallback
                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View(model));
            }
        }

        public ActionResult EditSimulationDepartment(Guid id)
        {
            try
            {
                var simulationDepartment = this.InvokeService<ISimulationManagementQueryService, SimulationDepartmentView>(service => service.RetrieveSimulationDepartment(id));
                var model = Mapper.DynamicMap<EditSimulationDepartmentModel>(simulationDepartment);

                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditSimulationDepartment(EditSimulationDepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<UpdateSimulationDepartmentRequest>(model);

                    this.InvokeService<ISimulationManagementCommandService>(
                        service => service.UpdateSimulationDepartment(request));

                    return RedirectToAction("EditSimulationDepartment", new { model.Id });
                }
                //fallback
                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View(model));
            }
        }

        public ActionResult EditSimulationLevel(Guid id)
        {
            try
            {
                var simulationLevel = this.InvokeService<ISimulationManagementQueryService, SimulationLevelView>(service => service.RetrieveSimulationLevel(id));
                var model = Mapper.DynamicMap<EditSimulationLevelModel>(simulationLevel);

                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditSimulationLevel(EditSimulationLevelModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<UpdateSimulationLevelRequest>(model);

                    this.InvokeService<ISimulationManagementCommandService>(
                        service => service.UpdateSimulationLevel(request));

                    return RedirectToAction("EditSimulationLevel", new { model.Id });
                }
                //fallback
                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View(model));
            }
        }

        public ActionResult EditSimulation(Guid id)
        {
            try
            {
                var simulation = this.InvokeService<ISimulationManagementQueryService, SimulationView>(service => service.RetrieveSimulation(id));
                var model = Mapper.DynamicMap<EditSimulationModel>(simulation);

                model.SimulationTranslations.Sort((a, b) => a.LanguageId.CompareTo(b.LanguageId));

                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditSimulation(EditSimulationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<UpdateSimulationRequest>(model);

                    this.InvokeService<ISimulationManagementCommandService>(service => service.UpdateSimulation(request));

                    return RedirectToAction("EditSimulation", new { model.Id });
                }
                //fallback
                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View(model));
            }
        }

        public ActionResult EditSimulationMatrixEntry(Guid id)
        {
            try
            {
                var simulationMatrixEntry = this.InvokeService<ISimulationManagementQueryService, SimulationMatrixEntryView>(service => service.RetrieveSimulationMatrixEntry(id));

                var model = Mapper.DynamicMap<EditSimulationMatrixEntryModel>(simulationMatrixEntry);

                var simulationLanguages = this.InvokeService<ISimulationManagementQueryService, List<SimulationCombinationLanguageView>>(service => service.ListSimulationCombinationLangauges(id));
                model.SimulationLanguages = simulationLanguages.Select(Mapper.DynamicMap<SimulationLanguageModel>).ToList();

                return View(EnsureDropdownlistItems(model));
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult EditSimulationMatrixEntry(EditSimulationMatrixEntryModel model)
        {
            try
            {
                if (model.SimulationId == Guid.Empty)
                    ModelState.AddModelError("SimulationId", "A simulation is required.");

                if (model.SimulationSetId == Guid.Empty)
                    ModelState.AddModelError("SimulationSetId", "A simulation set is required.");

                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<UpdateSimulationCombinationRequest>(model);
                    request.AvailableLanguageIds = model.SimulationLanguages.Where(sl => sl.IsChecked).Select(sl => sl.LanguageId).ToList();

                    this.InvokeService<ISimulationManagementCommandService>(
                        service => service.UpdateSimulationCombination(request));
                }
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View(EnsureDropdownlistItems(model)));
            }

            return View(EnsureDropdownlistItems(model));
        }

        public EditSimulationMatrixEntryModel EnsureDropdownlistItems(EditSimulationMatrixEntryModel model)
        {
            if (model.SimulationSets == null)
                model.SimulationSets = this.InvokeService<ISimulationManagementQueryService, ListSimulationSetsResponse>(service => service.ListSimulationSets(new ListSimulationSetsRequest())).SimulationSets;

            if (model.SimulationDepartments == null)
                model.SimulationDepartments = this.InvokeService<ISimulationManagementQueryService, ListSimulationDepartmentsResponse>(service => service.ListSimulationDepartments(new ListSimulationDepartmentsRequest())).SimulationDepartments;

            if (model.SimulationLevels == null)
                model.SimulationLevels = this.InvokeService<ISimulationManagementQueryService, ListSimulationLevelsResponse>(service => service.ListSimulationLevels(new ListSimulationLevelsRequest())).SimulationLevels;

            if (model.Simulations == null)
                model.Simulations = this.InvokeService<ISimulationManagementQueryService, ListSimulationsResponse>(service => service.ListSimulations(new ListSimulationsRequest())).Simulations;

                        return model;
        }

        public ActionResult CreateSimulationSet()
        {
            return View("EditSimulationSet", new EditSimulationSetModel());
        }

        [HttpPost]
        public ActionResult CreateSimulationSet(EditSimulationSetModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<CreateSimulationSetRequest>(model);

                    model.Id = this.InvokeService<ISimulationManagementCommandService, Guid>(service => service.CreateSimulationSet(request));

                    return RedirectToAction("EditSimulationSet", new { model.Id });
                }
                return View("EditSimulationSet", model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View("EditSimulationSet", model));
            }
        }

        public ActionResult CreateSimulationDepartment()
        {
            return View("EditSimulationDepartment", new EditSimulationDepartmentModel());
        }

        [HttpPost]
        public ActionResult CreateSimulationDepartment(EditSimulationDepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<CreateSimulationDepartmentRequest>(model);

                    model.Id = this.InvokeService<ISimulationManagementCommandService, Guid>(service => service.CreateSimulationDepartment(request));

                    return RedirectToAction("EditSimulationDepartment", new { model.Id });
                }

                return View("EditSimulationDepartment", model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View("EditSimulationDepartment", model));
            }
        }

        public ActionResult CreateSimulationLevel()
        {
            return View("EditSimulationLevel", new EditSimulationLevelModel());
        }

        [HttpPost]
        public ActionResult CreateSimulationLevel(EditSimulationLevelModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<CreateSimulationLevelRequest>(model);

                    model.Id = this.InvokeService<ISimulationManagementCommandService, Guid>(service => service.CreateSimulationLevel(request));

                    return RedirectToAction("EditSimulationLevel", new { model.Id });
                }
                return View("EditSimulationLevel", model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View("EditSimulationLevel", model));
            }
        }

        public ActionResult CreateSimulation()
        {
            return View("EditSimulation", new EditSimulationModel());
        }

        [HttpPost]
        public ActionResult CreateSimulation(EditSimulationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<CreateSimulationRequest>(model);

                    model.Id = this.InvokeService<ISimulationManagementCommandService, Guid>(service => service.CreateSimulation(request));

                    return RedirectToAction("EditSimulation", new { model.Id });
                }

                return View("EditSimulation", model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View("EditSimulation", model));
            }
        }

        public ActionResult CreateSimulationMatrixEntry()
        {
            try
            {
                var model = new EditSimulationMatrixEntryModel();

                return View("EditSimulationMatrixEntry", EnsureDropdownlistItems(model));
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult CreateSimulationMatrixEntry(EditSimulationMatrixEntryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = Mapper.DynamicMap<CreateSimulationMatrixEntryRequest>(model);

                    model.Id = this.InvokeService<ISimulationManagementCommandService, Guid>(service => service.CreateSimulationMatrixEntry(request));

                    return RedirectToAction("EditSimulationMatrixEntry", new { model.Id });
                }

                return View("EditSimulationMatrixEntry", EnsureDropdownlistItems(model));
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception, () => View("EditSimulationMatrixEntry", EnsureDropdownlistItems(model)));
            }
        }

        public ActionResult SimulationContexts()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var contexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());

                    return PartialView(contexts);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditSimulationContext(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var context = this.InvokeService<ISimulationManagementQueryService, SimulationContextView>(service => service.RetrieveSimulationContext(id));

                    var model = Mapper.DynamicMap<EditSimulationContextModel>(context);

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditSimulationContext(EditSimulationContextModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateSimulationContextRequest>(model);

                    this.InvokeService<ISimulationManagementCommandService>(service => service.UpdateSimulationContext(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult SimulationContextUsers(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var users = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextUserView>>(service => service.ListSimulationContextUsers(id));

                    var model = new SimulationContextUsersActionModel();
                    model.Users = users.OrderByDescending(u => u.ValidFrom).Select(Mapper.DynamicMap<EditSimulationContextUserModel>).ToList();

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
        public ActionResult SimulationContextUsers(SimulationContextUsersActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    model.Users.ForEach(u => u.ValidTo = u.ValidTo.Date.AddDays(1).AddSeconds(-1));

                    var request = model.Users.Select(Mapper.DynamicMap<UpdateSimulationContextUserRequest>).ToList();

                    this.InvokeService<ISimulationManagementCommandService>(service => service.UpdateSimulationContextUsers(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult GeneratePassword()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var allowedChars = this.InvokeService<IInfrastructureQueryService, string>(service => service.RetrieveAllowedPasswordChars());
                    var chars = new char[8];

                    for (int i = 0; i < chars.Length;  i++)
                        chars[i] = allowedChars[Randomizer.Next(0, allowedChars.Length)];

                    return Content(new string(chars));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult AddSimulationContextUser(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var allowedChars = this.InvokeService<IInfrastructureQueryService, string>(service => service.RetrieveAllowedPasswordChars());

                    var model = new EditSimulationContextUserModel();

                    var chars = new char[8];

                    for (int i = 0; i < chars.Length; i++)
                        chars[i] = allowedChars[Randomizer.Next(0, allowedChars.Length)];

                    model.SimulationContextId = id;
                    model.Password = new string(chars);
                    model.ValidFrom = DateTime.Now.Date;
                    model.ValidTo = DateTime.Now.Date.AddMonths(1);

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
        public ActionResult AddSimulationContextUser(EditSimulationContextUserModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewSimulationContextUserRequest>(model);
                    this.InvokeService<ISimulationManagementCommandService>(service => service.CreateNewSimulationContextUser(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }
        
        public ActionResult DeleteSimulationContextUser(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISimulationManagementCommandService>(service => service.DeleteSimulationContextUser(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult GenerateNewYear(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ISimulationManagementCommandService>(service => service.GenerateNewYear(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddSimulationContext()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new EditSimulationContextModel();
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
        public ActionResult AddSimulationContext(EditSimulationContextModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewSimulationContextRequest>(model);
                    this.InvokeService<ISimulationManagementCommandService>(service => service.CreateNewSimulationContext(request));
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
