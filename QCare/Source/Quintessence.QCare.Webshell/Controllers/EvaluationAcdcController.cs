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
    public class EvaluationAcdcController : Controller
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

                var evaluationFormAcdc = evaluationForm as EvaluationFormAcdcView;

                var introModel = new EvaluationFormBaseModel() { VerificationCode = verificationCode };
                return View("Intro", introModel);

            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
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

                var part1Model = Mapper.DynamicMap<EvaluationFormAcdcPart1Model>(evaluationForm);
                return View("Part1", part1Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Part1(EvaluationFormAcdcPart1Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcPart1Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcPart1(request));

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

                var part2Model = Mapper.DynamicMap<EvaluationFormAcdcPart2Model>(evaluationForm);
                part2Model.Navigation = 1; //Default: next.
                return View("Part2", part2Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Part2(EvaluationFormAcdcPart2Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcPart2Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcPart2(request));

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

                var part3Model = Mapper.DynamicMap<EvaluationFormAcdcPart3Model>(evaluationForm);
                return View("Part3", part3Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Part3(EvaluationFormAcdcPart3Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcPart3Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcPart3(request));

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

                var part4Model = Mapper.DynamicMap<EvaluationFormAcdcPart4Model>(evaluationForm);
                return View("Part4", part4Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Part4(EvaluationFormAcdcPart4Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcPart4Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcPart4(request));

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

                var part5Model = Mapper.DynamicMap<EvaluationFormAcdcPart5Model>(evaluationForm);
                return View("Part5", part5Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Part5(EvaluationFormAcdcPart5Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcPart5Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcPart5(request));

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

                var part6Model = Mapper.DynamicMap<EvaluationFormAcdcPart6Model>(evaluationForm);
                return View("Part6", part6Model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Part6(EvaluationFormAcdcPart6Model model)
        {
            try
            {
                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcPart6Request>(model);
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcPart6(request));

                return RedirectToAction(model.Navigation == 1 ? "Completed" : "Part5", new { verificationCode = model.VerificationCode });
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public ActionResult Completed(EvaluationFormBaseModel model)
        {
            try
            {
                if (model.Navigation == 0)
                    return RedirectToAction("Part6", new { verificationCode = model.VerificationCode });

                var request = Mapper.DynamicMap<UpdateEvaluationFormAcdcCompletedRequest>(model);
                request.IsCompleted = true;
                ServiceFactory.InvokeService<IProjectManagementCommandService>(service => service.UpdateEvaluationFormAcdcCompleted(request));

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
        private void SetLanguage(string languageCode)
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

