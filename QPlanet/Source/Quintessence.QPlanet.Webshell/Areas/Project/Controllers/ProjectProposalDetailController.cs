using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.ViewModel.Fin;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectProposalDetailController : ProjectController
    {
        public ActionResult Create()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateProposalModel();

                    model.DateReceived = DateTime.Now;

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
        public ActionResult Create(CreateProposalModel model)
        {
            using (DurationLog.Create())
            {

                try
                {
                        var request = CreateNewProposalRequest(model);
                        //var request = Mapper.DynamicMap<CreateNewProposalRequest>(model);
                        var proposalId =
                            this.InvokeService<IProjectManagementCommandService, Guid>(
                                service => service.CreateNewProposal(request));
                        return RedirectToAction("Edit", new {id = proposalId});

                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        private CreateNewProposalRequest CreateNewProposalRequest(CreateProposalModel model)
        {
            return new CreateNewProposalRequest()
            {
                ContactId = model.ContactId,
                Name = model.Name,
                Deadline = model.Deadline,
                DateReceived = model.DateReceived,
                Description = model.Description,
                StatusCode = ProposalStatusType.ToEvaluate,
                FinalBudget = 0,
                WrittenProposal = model.WrittenProposal,
                DateWon = null
            };
        }

        public ActionResult CreateWon()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateWonProposalModel();

                    model.DateReceived = DateTime.Now;
                    model.DateWon = DateTime.Now;
                    model.WrittenProposal = false;

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
        public ActionResult CreateWon(CreateWonProposalModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewWonProposalRequest>(model);
                    var proposalId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewWonProposal(request));
                    return RedirectToAction("Edit", new { id = proposalId });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult DeleteProposal(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProposal(id));
                    return new HttpStatusCodeResult(200);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult Edit(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var proposal = this.InvokeService<IProjectManagementQueryService, ProposalView>(service => service.RetrieveProposal(id));

                    var model = Mapper.DynamicMap<EditProposalModel>(proposal);
                    model.TypeOfSubmit = 1;

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
        public ActionResult Edit(EditProposalModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (model.TypeOfSubmit == 2) //Redirect to "Create project" page with proposal details
                    {
                        var createProjectModel = new CreateProjectModel();

                        createProjectModel.ProjectTypes =
                            MapperExtensions.MapList<ProjectTypeSelectListItemModel>(
                                this.InvokeService<IProjectManagementQueryService, List<ProjectTypeView>>(
                                    service => service.ListProjectTypes()));

                        var identity = IdentityHelper.RetrieveIdentity(HttpContext.ApplicationInstance.Context);

                        if (identity == null)
                        {
                            return new HttpUnauthorizedResult("Unable to retrieve identity information");
                        }
                        var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationTokenDetail(new Guid(identity.Ticket.UserData)));

                        createProjectModel.ProjectManagerUserId = token.UserId;
                        createProjectModel.ProjectManagerFullName = token.User.FullName;
                        createProjectModel.ContactId = model.ContactId;
                        createProjectModel.ContactName = model.ContactFullName;

                        return View("~/Areas/Project/Views/ProjectGeneral/Create.cshtml", createProjectModel);
                    }
                    
                    //Else save proposal
                    var request = Mapper.DynamicMap<UpdateProposalRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProposal(request));

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
