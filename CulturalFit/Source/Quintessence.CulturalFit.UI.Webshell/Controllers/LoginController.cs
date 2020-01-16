using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Exception;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.UI.Core.Service;
using Quintessence.CulturalFit.UI.Webshell.HtmlHelpers;
using Quintessence.CulturalFit.UI.Webshell.Models.Login;
using Resources;

namespace Quintessence.CulturalFit.UI.Webshell.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServiceFactory _serviceFactory;

        public LoginController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }


        public ViewResult Index(string lang = "en")
        {
            var loginModel = new LoginModel();
            try
            {
                var channel = _serviceFactory.CreateChannel<ITheoremListService>();

                var languages = channel.ListLanguages();

                loginModel.Languages =
                    new List<LanguageEntity>(
                        languages.Select(
                            l => new LanguageEntity { Id = l.Id, Name = l.Name, GlobalizationName = l.Code }));

                if (loginModel.Languages.All(l => l.GlobalizationName != lang))
                    lang = "en";

                loginModel.SelectedLanguage =
                    //loginModel.Languages.FirstOrDefault(l => l.GlobalizationName == Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    loginModel.Languages.FirstOrDefault(l => l.GlobalizationName.ToLower() == lang.ToLower())
                        .GlobalizationName;

                return View(loginModel);
            }
            catch (BusinessException exception)
            {
                LogManager.LogError(exception.Message, exception);
                ViewData.ModelState.AddModelError("VerificationCode", exception);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
                ViewData.ModelState.AddModelError("VerificationCode", exception);
            }
            return View();
        }

        //[ChildActionOnly]
        [Localization]
        public JsonResult ChangeLanguage()
        {
            var result = new object[]
                             {
                                 new {id = "LblLegend", value = Global.EnterVerificationCode},
                                 new {id = "LblNotice", value = Global.EnterVerificationCode},
                                 new {id = "LblVerificationCode", value = "Code"},
                                 new {id = "LblShowChars", value = Global.ShowCharacters},
                                 new {id ="VerificationCodeValidation", value = Global.WrongVerificationCode}
                             };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Localization]
        public ActionResult Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var channel = _serviceFactory.CreateChannel<ITheoremListService>();



                    var request = channel.ValidateVerificationCode(loginModel.VerificationCode);

                    FormsAuthentication.SetAuthCookie(loginModel.VerificationCode, false);

                    var retrievedRequest =
                        channel.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(loginModel.VerificationCode));
                    if (retrievedRequest.TheoremLists.Count(t => t.IsCompleted) == retrievedRequest.TheoremLists.Count &&
                        retrievedRequest.TheoremLists.Count > 0)
                    {
                        var returnAction = RedirectToAction("Submit", "Questionnaire", new { lang = loginModel.SelectedLanguage, requestId = retrievedRequest.Id.ToString() });
                        return returnAction;
                    }

                    switch (request.TheoremListRequestType.Enum)
                    {
                        case TheoremListRequestTypeEnum.AsIs:
                            return
                                new RedirectResult(string.Format("~/{0}/Welcome/Index/{1}", loginModel.SelectedLanguage,
                                                                 loginModel.VerificationCode));

                        case TheoremListRequestTypeEnum.AsIsToBe:
                            return
                                new RedirectResult(string.Format("~/{0}/Welcome/Index/{1}", loginModel.SelectedLanguage,
                                                                 loginModel.VerificationCode));
                    }
                }
                catch (BusinessException exception)
                {
                    LogManager.LogError(exception.Message, exception);
                    ViewData.ModelState.AddModelError("VerificationCode", exception);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception.Message, exception);
                    ViewData.ModelState.AddModelError("VerificationCode", exception);
                }
            }
            var ch = _serviceFactory.CreateChannel<ITheoremListService>();
            var languages = ch.ListLanguages();

            loginModel.Languages =
                new List<LanguageEntity>(
                    languages.Select(
                        l => new LanguageEntity { Id = l.Id, Name = l.Name, GlobalizationName = l.Code }));
            loginModel.SelectedLanguage =
                loginModel.Languages.FirstOrDefault(l => l.GlobalizationName == loginModel.SelectedLanguage)
                    .GlobalizationName;

            return View(loginModel);
        }
    }
}
