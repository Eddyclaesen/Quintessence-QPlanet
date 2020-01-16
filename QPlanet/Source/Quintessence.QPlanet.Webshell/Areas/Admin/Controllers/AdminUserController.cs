using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Crm;
using Quintessence.QPlanet.ViewModel.Sec;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminUser;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    public class AdminUserController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var users = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListUsers());

                    var model = new UsersActionModel();
                    model.Users = users;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult Roles()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var roles = this.InvokeService<ISecurityQueryService, List<RoleView>>(service => service.ListRoles());

                    var model = new RolesActionModel();
                    model.Roles = roles;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult SynchronizeUsers()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var unsynchronizedEmployees = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmUnsynchronizedEmployeeView>>(service => service.ListUnsynchronizedEmployees());

                    var model = new SynchronizeUsersActionModel();
                    model.UnsynchronizedEmployees = unsynchronizedEmployees
                        .OrderBy(e => e.FullName)
                        .Select(Mapper.DynamicMap<UnsynchronizedEmployeeModel>)
                        .ToList();

                    model.UnsynchronizedEmployees.ForEach(e =>
                    {
                        e.UserName = e.UserName.ToLowerInvariant();
                        e.Password = e.UserName.ToLowerInvariant();
                    });

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
        public ActionResult SynchronizeUsers(SynchronizeUsersActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = model.UnsynchronizedEmployees.Where(e => e.IsChecked).Select(Mapper.DynamicMap<SynchronizeUserRequest>).ToList();

                    this.InvokeService<ISecurityCommandService>(service => service.SynchronizeUsers(requests));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult AddUser()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());

                    var model = new AddUserModel();

                    var dutchLanguage = languages.FirstOrDefault(l => l.Code == "NL");

                    if (dutchLanguage != null)
                        model.LanguageId = dutchLanguage.Id;

                    model.Languages = languages;

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
        public ActionResult AddUser(AddUserModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewUserRequest>(model);

                    this.InvokeService<ISecurityCommandService>(service => service.CreateNewUser(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult UserDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUser(id));

                    return View(user);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditUser(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUser(id));
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());
                    var roles = this.InvokeService<ISecurityQueryService, List<RoleView>>(service => service.ListRoles());

                    EditUserModel model;

                    if (user is EmployeeView)
                        model = Mapper.DynamicMap<EditEmployeeModel>(user);
                    else
                        model = Mapper.DynamicMap<EditUserModel>(user);
                    model.Languages = languages;
                    model.Roles = roles;

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
        public ActionResult EditUser(EditUserModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateUserRequest>(model);
                    this.InvokeService<ISecurityCommandService>(service => service.UpdateUser(request));

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
        public ActionResult EditEmployee(EditEmployeeModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateEmployeeRequest>(model);
                    this.InvokeService<ISecurityCommandService>(service => service.UpdateUser(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult RoleDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var role = this.InvokeService<ISecurityQueryService, RoleView>(service => service.RetrieveRole(id));

                    return View(role);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult UsersInRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var users = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListUsersInRole(id));

                    return PartialView(users);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult OperationsInRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var operations = this.InvokeService<ISecurityQueryService, List<RoleOperationView>>(service => service.ListOperationsInRole(id));

                    return PartialView(operations);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }



























        //public ActionResult Roles(DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var roles = this.InvokeService<ISecurityQueryService, IEnumerable<RoleView>>(service => service.ListRoles());

        //        var total = roles.Count();

        //        var filteredRoles = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? roles
        //                            : roles.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedRoles = filteredRoles
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Name, u.Id.ToString() });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredRoles.Count(),
        //            aaData = selectedRoles
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult UserGroups(DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var userGroups = this.InvokeService<ISecurityQueryService, IEnumerable<UserGroupView>>(service => service.ListUserGroups());

        //        var total = userGroups.Count();

        //        var filteredUserGroups = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? userGroups
        //                            : userGroups.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedUserGroups = filteredUserGroups
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Name, u.Id.ToString() });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredUserGroups.Count(),
        //            aaData = selectedUserGroups
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult OperationDomains(DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var operationDomains = this.InvokeService<ISecurityQueryService, IEnumerable<OperationDomainView>>(service => service.ListOperationDomains());

        //        var total = operationDomains.Count();

        //        var filteredOperationDomains = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? operationDomains
        //                            : operationDomains.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedOperationDomains = filteredOperationDomains
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Name, u.Id.ToString() });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredOperationDomains.Count(),
        //            aaData = selectedOperationDomains
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult DeleteUser(Guid id)
        //{
        //    try
        //    {
        //        this.InvokeService<ISecurityCommandService>(service => service.DeleteUser(id));
        //        return new HttpStatusCodeResult(200);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult DeleteUserGroup(Guid id)
        //{
        //    try
        //    {
        //        this.InvokeService<ISecurityCommandService>(service => service.DeleteUserGroup(id));
        //        return new HttpStatusCodeResult(200);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult EditUser(Guid id)
        //{
        //    var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUser(id));

        //    var model = Mapper.DynamicMap<EditUserModel>(user);

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult EditUser(EditUserModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var request = Mapper.DynamicMap<UpdateUserRequest>(model);

        //            this.InvokeService<ISecurityCommandService>(service => service.UpdateUser(request));

        //            return RedirectToAction("EditUser", new { model.Id });
        //        }
        //        //fallback
        //        return View(model);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleError(exception, () => View(model));
        //    }
        //}

        //public ActionResult AssignedRoles(Guid id, DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var roles = this.InvokeService<ISecurityQueryService, List<UserAssignedRoleView>>(service => service.ListRolesByUser(id));

        //        var total = roles.Count();

        //        var filteredRoles = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? roles
        //                            : roles.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedRoles = filteredRoles
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Name, u.Id.ToString() });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredRoles.Count(),
        //            aaData = selectedRoles
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult AssignedUserGroups(Guid id, DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var userGroups = this.InvokeService<ISecurityQueryService, List<UserAssignedUserGroupView>>(service => service.ListUserGroupsByUser(id));

        //        var total = userGroups.Count();

        //        var filteredUserGroups = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? userGroups
        //                            : userGroups.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedUserGroups = filteredUserGroups
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Name, u.Id.ToString() });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredUserGroups.Count(),
        //            aaData = selectedUserGroups
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult AssignedOperations(Guid id, DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var operations = this.InvokeService<ISecurityQueryService, List<UserAssignedOperationView>>(service => service.ListOperationsByUser(id));

        //        var total = operations.Count();

        //        var filteredOperations = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? operations
        //                            : operations.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedOperations = filteredOperations
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Name, u.Id.ToString() });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredOperations.Count(),
        //            aaData = selectedOperations
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        //public ActionResult AllowedOperations(Guid id, DataTableParameterModel parameters)
        //{
        //    try
        //    {
        //        var roles = this.InvokeService<ISecurityQueryService, List<OperationView>>(service => service.ListAllowedOperationsByUser(id));

        //        var total = roles.Count();

        //        var filteredRoles = string.IsNullOrWhiteSpace(parameters.sSearch)
        //                            ? roles
        //                            : roles.Where(ss => parameters.sSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        //                                .All(st => ss.Name.ToLowerInvariant().Contains(st.ToLowerInvariant())));

        //        var selectedRoles = filteredRoles
        //            .OrderBy(ss => ss.Name)
        //            .Skip(parameters.iDisplayStart)
        //            .Take(parameters.iDisplayLength)
        //            .Select(u => new[] { u.Code, u.Name, u.Description });

        //        var model = new
        //        {
        //            sEcho = parameters.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = filteredRoles.Count(),
        //            aaData = selectedRoles
        //        };

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogManager.LogError(exception);
        //        return HandleStatusCodeError(exception);
        //    }
        //}

        ////public ActionResult SearchUsers(string term)
        ////{
        ////    var response = this.InvokeService<ISecurityQueryService, SearchUserResponse>(service => service.SearchUser(new SearchUserRequest { Name = term }));
        ////    var contactJson = response.Users.Select(u => new { label = u.FullName, value = u.Id }).ToList();

        ////    var total = contactJson.Count;

        ////    var limitedList = contactJson.Take(10).ToList();

        ////    if (total > 10)
        ////        limitedList.Add(new { label = string.Format("... Showing 10 of {0}", total), value = Guid.Empty });

        ////    var result = Json(limitedList, JsonRequestBehavior.AllowGet);
        ////    return result;
        ////}

        ////public ActionResult UserDetail(Guid id)
        ////{
        ////    var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUserDetail(id));
        ////    var userView = Mapper.Map<UserModel>(user);
        ////    userView.Operations.AddRange(user.Operations.Select(Mapper.Map<OperationModel>));
        ////    userView.Roles.AddRange(user.Roles.Select(Mapper.Map<RoleModel>));
        ////    userView.UserGroups.AddRange(user.UserGroups.Select(Mapper.Map<UserGroupModel>));
        ////    userView.AuthenticationTokens.AddRange(user.AuthenticationTokens.Select(Mapper.Map<AuthenticationTokenModel>));

        ////    return PartialView(userView);
        ////}

        ////[HttpPost]
        ////public ActionResult UserDetail(UserModel userModel)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //[QPlanetAuthorizeAction(OperationType.ADMINREVOK)]
        //public ActionResult Revoke(Guid id)
        //{
        //    try
        //    {
        //        var commandServiceInvoker = new ServiceInvoker<ISecurityCommandService>();
        //        commandServiceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, service => service.RevokeAuthenticationTokens(id));
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception exception)
        //    {
        //        return HandleError(exception);
        //    }
        //}
    }
}
