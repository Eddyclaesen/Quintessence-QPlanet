using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Cam;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QPlanet.Webshell.Infrastructure.Enums;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Controllers
{
    //[QPlanetAuthenticateController("CAM")]
    public class CandidateController : QPlanetControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCandidates()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var candidates = this.InvokeService<ICandidateManagementQueryService, List<CandidateView>>(service => service.ListCandidates());

                    return PartialView(candidates);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult Neo(Guid id)
        {
            String strConnString = @"Data Source = 10.3.176.13\QNT02S; Initial Catalog = Quintessence; User ID = QuintessenceUser; Password =$Quint123;";
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strConnString);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Quintessence.dbo.StartSingleNeoQplanet", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CandidateId", id);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            var candidate =
                        this.InvokeService<ICandidateManagementQueryService, CandidateView>(
                            service => service.RetrieveCandidate(id));

            var model = Mapper.Map<CandidateModel>(candidate);

            var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(
                                    service => service.ListLanguages());
            model.Languages = languages;
            model.LanguageId = languages.Single(l => l.Id == candidate.LanguageId).Id;

            ViewBag.ViewMode = ViewMode.Edit;
            return View("Edit", model);
        }

        public ActionResult Edit(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var candidate =
                        this.InvokeService<ICandidateManagementQueryService, CandidateView>(
                            service => service.RetrieveCandidate(id));

                    var model = Mapper.Map<CandidateModel>(candidate);

                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(
                                            service => service.ListLanguages());
                    model.Languages = languages;
                    model.LanguageId = languages.Single(l => l.Id == candidate.LanguageId).Id;

                    ViewBag.ViewMode = ViewMode.Edit;
                    return View(model);
                }
                catch (Exception exception)
                {
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(CandidateModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var updateCandidateRequest = Mapper.Map<UpdateCandidateRequest>(model);

                        this.InvokeService<ICandidateManagementCommandService>(
                            service => service.UpdateCandidate(updateCandidateRequest));

                    }
                    ViewBag.ViewMode = ViewMode.Edit;
                    return RedirectToAction("Edit", new { id = model.Id });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult Create()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var languages =
                                        this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(
                                            service => service.ListLanguages());
                    var selectedLanguageId = languages.First().Id;
                    var model = new CandidateModel
                        {
                            Languages = languages,
                            LanguageId = selectedLanguageId
                        };
                    ViewBag.ViewMode = ViewMode.Create;
                    return View("Edit", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult Create(CandidateModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var createCandidateRequest = Mapper.Map<CreateCandidateRequest>(model);

                        var createdId = this.InvokeService<ICandidateManagementCommandService, Guid>(
                            service => service.CreateCandidate(createCandidateRequest));
                        ViewBag.ViewMode = ViewMode.Edit;
                        return RedirectToAction("Edit", new { id = createdId });
                    }
                    return RedirectToAction("Create", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }
    }
}
