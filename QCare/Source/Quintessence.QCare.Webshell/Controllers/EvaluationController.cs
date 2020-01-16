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
    public class EvaluationController : Controller
    {
        public ActionResult Final()
        {
            //SetLanguage(evaluationForm.LanguageCode);

            return View("Final");
        }
        
        public ActionResult Error()
        {
            return View("Error");
        }

        #region Helper method(s)
        private static void SetLanguage(string languageCode)
        {
            var language = "en";

            if (languageCode.ToLower() == "fr")
                language = "fr";
            else if(languageCode.ToLower() == "nl")
                language = "nl";

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }
        #endregion

        
    }
}
