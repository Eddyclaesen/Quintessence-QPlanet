using System;
using System.Linq;
using System.Web.Mvc;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.UI.Core.Service;
using Quintessence.CulturalFit.UI.Webshell.HtmlHelpers;
using Quintessence.CulturalFit.UI.Webshell.Models.Welcome;
using Resources;

namespace Quintessence.CulturalFit.UI.Webshell.Controllers
{
    [Authorize]
    [Localization]
    public class WelcomeController : Controller
    {
        private readonly ServiceFactory _serviceFactory;

        public WelcomeController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public ActionResult Index(string requestCode, WelcomeModel welcomeModel)
        {
            try
            {
                var channel = _serviceFactory.CreateChannel<ITheoremListService>();

                var request = channel.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(requestCode));

                welcomeModel.RequestCode = requestCode;
                welcomeModel.LanguageGlobalization = (string) RouteData.Values["lang"];

                TheoremList theoremList;
                switch (request.TheoremListRequestType.Enum)
                {
                    case TheoremListRequestTypeEnum.AsIs:
                        if(request.CandidateId == null)
                            welcomeModel.WelcomeTypeText = WelcomeModel.WelcomeType.AsIsCustomer;
                        else
                            welcomeModel.WelcomeTypeText = WelcomeModel.WelcomeType.AsIsParticipant;
                        theoremList = request.TheoremLists.SingleOrDefault();
                        welcomeModel.ListCode = theoremList.VerificationCode;
                        
                        break;

                    case TheoremListRequestTypeEnum.AsIsToBe:
                        theoremList = request.TheoremLists.FirstOrDefault(tl => tl.TheoremListType.Enum == TheoremListTypeEnum.AsIs);
                            welcomeModel.WelcomeTypeText = WelcomeModel.WelcomeType.ToBe;
                        
                        welcomeModel.ListCode = theoremList.VerificationCode;
                        
                        break;
                }

                return View(welcomeModel);
            }
            catch (BusinessException exception)
            {
                LogManager.LogError(exception.Message, exception);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
            }

            return View(welcomeModel);
        }

    }
}
