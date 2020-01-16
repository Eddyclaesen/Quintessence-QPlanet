using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QCare.ViewModel;
using Quintessence.QCare.Webshell.Infrastructure.Services;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QCare.Webshell.Controllers
{
    public class EvaluationCustomProjectsController : Controller
    {
        public ActionResult Intro(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var evaluationFormCustomProjects = evaluationForm as EvaluationFormCustomProjectsView;
                
                    var introModel = new EvaluationFormBaseModel() { VerificationCode = verificationCode };
                    return View("Intro", introModel);
                               
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        #region Part 1
        public ActionResult Part1(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part1Model = Mapper.DynamicMap<EvaluationFormCustomProjectsPart1Model>(evaluationForm);
                return View("Part1", part1Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part1(EvaluationFormCustomProjectsPart1Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCustomProjectsPart1Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCustomProjectsPart1(request));

                return RedirectToAction("Part2", new { verificationCode = model.VerificationCode });
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Part 2
        public ActionResult Part2(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part2Model = Mapper.DynamicMap<EvaluationFormCustomProjectsPart2Model>(evaluationForm);
                part2Model.Navigation = 1; //Default: next.

                return View("Part2", part2Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part2(EvaluationFormCustomProjectsPart2Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCustomProjectsPart2Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCustomProjectsPart2(request));

                return RedirectToAction(model.Navigation == 1 ? "Part3" : "Part1", new { verificationCode = model.VerificationCode });
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Part 3
        public ActionResult Part3(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part3Model = Mapper.DynamicMap<EvaluationFormCustomProjectsPart3Model>(evaluationForm);
                return View("Part3", part3Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part3(EvaluationFormCustomProjectsPart3Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCustomProjectsPart3Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCustomProjectsPart3(request));

                return RedirectToAction(model.Navigation == 1 ? "Completed" : "Part2", new { verificationCode = model.VerificationCode });

            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Completed
        public ActionResult Completed(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));
                
                if(evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");
                
                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var model = Mapper.DynamicMap<EvaluationFormBaseModel>(evaluationForm);
                return View("Completed", model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Completed(EvaluationFormBaseModel model)
        {
            try
            {
                if (model.Navigation == 0)
                    return RedirectToAction("Part3", new { verificationCode = model.VerificationCode });

                var request = Mapper.DynamicMap<UpdateEvaluationFormCustomProjectsCompletedRequest>(model);
                request.IsCompleted = true;
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCustomProjectsCompleted(request));

                return RedirectToAction("Final", "Evaluation");
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }

        }
        #endregion

        #region Helper method(s)
        private static void SetLanguage(string languageCode)
        {
            var language = "en";

            if (languageCode.ToLower() == "fr")
                language = "fr";
            else if (languageCode.ToLower() == "nl")
                language = "nl";

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }
        #endregion
    }
}

