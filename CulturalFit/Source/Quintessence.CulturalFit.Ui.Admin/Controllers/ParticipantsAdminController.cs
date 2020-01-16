using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.UI.Admin.Models.Admin;
using Quintessence.CulturalFit.UI.Admin.Models.Admin.Entities;
using Quintessence.CulturalFit.UI.Core.Service;

namespace Quintessence.CulturalFit.UI.Admin.Controllers
{

    public class ParticipantsAdminController : Controller
    {
        #region Private fields
        private readonly ServiceFactory _serviceFactory;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantsAdminController" /> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public ParticipantsAdminController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index view for listing the participants.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Index(int projectId)
        {
            try
            {
                TempData["ProjectId"] = projectId;
                var service = _serviceFactory.CreateChannel<IAdminService>();
                var project = service.RetrieveProject(projectId);
                var participants = service.ListParticipants(project.AcProjectId);
                var entities = participants.Select(Mapper.Map<CandidateEntity>);
                var model = new CandidateOverviewModel
                {
                    CandidateEntities = entities.ToList(),
                    ContactId = project.ContactId
                };
                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
                throw new Exception("Something went wrong with listing the candidates... Please retry later.");
            }

        }

        /// <summary>
        /// HttpPost for Index view.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        [HttpPost]
        public ActionResult Save(int contactId, CandidateOverviewModel model)
        {
            try
            {
                var m = model;
                var service = _serviceFactory.CreateChannel<IAdminService>();
                var projectId = (int) TempData["ProjectId"];
                service.CreateCandidateRequests(model.CandidateEntities.Where(c => c.HasRequest).Select(ce => new CandidateRequest
                {
                    FirstName = ce.FirstName,
                    LastName = ce.LastName,
                    CrmParticipantId = ce.Id,
                    ContactId = model.ContactId.GetValueOrDefault(),
                    Deadline = ce.Deadline,
                    TheoremListRequestTypeId = 1
                }).ToList(), projectId);

                
                return new HttpStatusCodeResult(200);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
                return new HttpStatusCodeResult(500);
                throw new Exception("Something went wrong with saving the requests for the selected candidates... Please retry later.");
            }
            return View(model);
        }
        #endregion
    }
}
