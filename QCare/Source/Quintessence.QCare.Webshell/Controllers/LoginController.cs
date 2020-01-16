using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using Quintessence.QCare.Webshell.Infrastructure.Services;
using Quintessence.QCare.Webshell.Models.Login;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Resources;
using System.Web.Security;

namespace Quintessence.QCare.Webshell.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                var evaluationForm = ServiceFactory.InvokeService<IProjectManagementQueryService, EvaluationFormView>(service => service.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { VerificationCode = model.VerificationCode }));
                
                if (evaluationForm == null)
                    throw new AuthenticationException();
                
               if (evaluationForm.IsCompleted)
                    return RedirectToAction("Final", "Evaluation");

                var evaluationFormCoaching = evaluationForm as EvaluationFormCoachingView;
                if (evaluationFormCoaching != null)
                {
                    LogManager.LogTrace("EvaluationFormCoachingView found");
                    return RedirectToAction("Intro", "EvaluationCoaching", new { verificationCode = model.VerificationCode });
                }

                var evaluationFormAcdc = evaluationForm as EvaluationFormAcdcView;
                if (evaluationFormAcdc != null)
                {
                    return RedirectToAction("Intro", "EvaluationAcdc", new { verificationCode = model.VerificationCode });
                }

                var evaluationFormCustomProjects = evaluationForm as EvaluationFormCustomProjectsView;
                if (evaluationFormCustomProjects != null)
                {
                    LogManager.LogTrace("EvaluationFormCustomProjectsView found");
                    return RedirectToAction("Intro", "EvaluationCustomProjects", new { verificationCode = model.VerificationCode });
                }



                throw new AuthenticationException();
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);

                if (exception is AuthenticationException)
                {
                    return RedirectToAction("AuthenticationFailed");
                }

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        public ActionResult AuthenticationFailed()
        {
            return View("AuthenticationFailed");
        }
    }
}
