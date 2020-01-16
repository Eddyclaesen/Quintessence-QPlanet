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
    public class EvaluationCoachingController : Controller
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

                var evaluationFormCoaching = evaluationForm as EvaluationFormCoachingView;

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

                var part1Model = Mapper.DynamicMap<EvaluationFormCoachingPart1Model>(evaluationForm);
                return View("Part1", part1Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part1(EvaluationFormCoachingPart1Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart1Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart1(request));

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

                var part2Model = Mapper.DynamicMap<EvaluationFormCoachingPart2Model>(evaluationForm);
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
        public ActionResult Part2(EvaluationFormCoachingPart2Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart2Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart2(request));

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

                var part3Model = Mapper.DynamicMap<EvaluationFormCoachingPart3Model>(evaluationForm);
                return View("Part3", part3Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part3(EvaluationFormCoachingPart3Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart3Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart3(request));

                return RedirectToAction(model.Navigation == 1 ? "Part4" : "Part2", new { verificationCode = model.VerificationCode });

            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Part 4
        public ActionResult Part4(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part4Model = Mapper.DynamicMap<EvaluationFormCoachingPart4Model>(evaluationForm);
                return View("Part4", part4Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part4(EvaluationFormCoachingPart4Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart4Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart4(request));

                return RedirectToAction(model.Navigation == 1 ? "Part5" : "Part3", new { verificationCode = model.VerificationCode });
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Part 5
        public ActionResult Part5(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part5Model = Mapper.DynamicMap<EvaluationFormCoachingPart5Model>(evaluationForm);
                return View("Part5", part5Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part5(EvaluationFormCoachingPart5Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart5Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart5(request));

                return RedirectToAction(model.Navigation == 1 ? "Part6" : "Part4", new { verificationCode = model.VerificationCode });
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Part 6
        public ActionResult Part6(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part6Model = Mapper.DynamicMap<EvaluationFormCoachingPart6Model>(evaluationForm);
                return View("Part6", part6Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part6(EvaluationFormCoachingPart6Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart6Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart6(request));

                return RedirectToAction(model.Navigation == 1 ? "Part7" : "Part5", new { verificationCode = model.VerificationCode });
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
        #endregion

        #region Part 7
        public ActionResult Part7(string verificationCode)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = verificationCode }));

                if (evaluationForm == null)
                    return RedirectToAction("Error", "Evaluation");

                if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                SetLanguage(evaluationForm.LanguageCode);

                var part7Model = Mapper.DynamicMap<EvaluationFormCoachingPart7Model>(evaluationForm);
                return View("Part7", part7Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return RedirectToAction("Error", "Evaluation");
            }
        }

        [HttpPost]
        public ActionResult Part7(EvaluationFormCoachingPart7Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingPart7Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingPart7(request));

                return RedirectToAction(model.Navigation == 1 ? "Completed" : "Part6", new { verificationCode = model.VerificationCode });
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

                if (evaluationForm == null)
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
                    return RedirectToAction("Part7", new { verificationCode = model.VerificationCode });

                var request = Mapper.DynamicMap<UpdateEvaluationFormCoachingCompletedRequest>(model);
                request.IsCompleted = true;
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormCoachingCompleted(request));

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
