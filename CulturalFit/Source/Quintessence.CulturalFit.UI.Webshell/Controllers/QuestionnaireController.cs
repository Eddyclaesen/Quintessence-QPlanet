using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Exception;
using Quintessence.CulturalFit.DataModel.Reports;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.UI.Core.Exceptions;
using Quintessence.CulturalFit.UI.Core.Service;
using Quintessence.CulturalFit.UI.Webshell.HtmlHelpers;
using Quintessence.CulturalFit.UI.Webshell.Models.Questionnaire;
using Resources;

namespace Quintessence.CulturalFit.UI.Webshell.Controllers
{
    [Authorize]
    [Localization]
    public class QuestionnaireController : Controller
    {
        private readonly ServiceFactory _serviceFactory;

        public QuestionnaireController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public ViewResult Index(string requestcode, string listcode, QuestionnaireModel model)
        {
            try
            {
                var channel = _serviceFactory.CreateChannel<ITheoremListService>();

                Session["TheoremList"] = null;

                var languages = channel.ListLanguages();

                model.Languages = new List<LanguageEntity>(languages.Select(l => new LanguageEntity { Id = l.Id, Name = l.Name, GlobalizationName = l.Code }));

                var theoremListRequest = channel.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(requestcode));
                var theoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.VerificationCode == listcode);

                model.SelectedLanguage = model.Languages.FirstOrDefault(l => l.GlobalizationName == (string)RouteData.Values["lang"]);

                model.ListCode = listcode;

                model.IsCompleted = theoremListRequest.TheoremLists.All(tl => tl.IsCompleted);
                model.TheoremListCount = theoremListRequest.TheoremLists.Count;
                model.TheoremListRequestId = theoremListRequest.Id;

                model.Theorems = new List<TheoremEntity>(theoremList.Theorems.Select(t => new TheoremEntity { Id = t.Id, Quote = t.GetTranslation(model.SelectedLanguage.Id), IsLeast = t.IsLeastApplicable, IsMost = t.IsMostApplicable }));
                model.TheoremListType = theoremList.TheoremListType.Type;

                Session["TheoremListRequest"] = theoremListRequest;

                return View(model);
            }
            catch (BusinessException exception)
            {
                LogManager.LogError(exception.Message, exception);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
            }

            return View();
        }

        public ActionResult Submit(string lang, Guid requestId)
        {
            var service = _serviceFactory.CreateChannel<ITheoremListService>();
            var languages = service.ListLanguages();
            var language = languages.FirstOrDefault(l => l.Code == lang);
            language = language ?? new Language { Code = "nl", Id = 1, Name = "Nederlands" };

            var model = new QuestionnaireModel
                            {
                                ErrorMessages = new List<ErrorMessage>(),
                                IsCompleted = true,
                                TheoremListRequestId = requestId,
                                SelectedLanguage = new LanguageEntity
                                                       {
                                                           GlobalizationName = language.Code,
                                                           Id = language.Id,
                                                           Name = language.Name
                                                       }
                            };

            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterTheoremCheck(Guid theoremId, bool isLeast, bool isMost)
        {
            try
            {
                var channel = _serviceFactory.CreateChannel<ITheoremListService>();
                channel.RegisterTheoremCheck(theoremId, isLeast, isMost);
                return new EmptyResult();
            }
            catch (Exception exception)
            {
                return new EmptyResult();
            }    
        }

        [HttpPost]
        public ActionResult Submit(QuestionnaireModel model)
        {
            if (model != null)
            {
                try
                {
                    var service = _serviceFactory.CreateChannel<ITheoremListService>();
                    var languages = service.ListLanguages();
                    var language =
                        languages.FirstOrDefault(l => l.Code == (string) RouteData.Values["lang"]);
                    model.SelectedLanguage = new LanguageEntity
                                                 {
                                                     GlobalizationName = language.Code,
                                                     Id = language.Id,
                                                     Name = language.Name
                                                 };
                    var least = model.Theorems.Count(t => t.IsLeast);
                    if (least != 5)
                        model.ErrorMessages.Add(new ErrorMessage(string.Format(Global.PleaseSelect5LeastApplicableTheorems, least)));

                    var most = model.Theorems.Count(t => t.IsMost);
                    if (most != 5)
                        model.ErrorMessages.Add(new ErrorMessage(string.Format(Global.PleaseSelecte5MostApplicableTheorems, most)));

                    if (model.ErrorMessages.Count == 0)
                    {
                        var theoremListRequest = Session["TheoremListRequest"] as TheoremListRequest;

                        //TODO: add message that session has expired
                        if (theoremListRequest == null)
                            return Redirect("~/");


                        var listCode = model.ListCode;
                        var theoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.VerificationCode == listCode);
                        if (!model.IsCompleted && !theoremList.IsCompleted)
                        {
                            foreach (var theorem in theoremList.Theorems)
                            {
                                var theoremEntity = model.Theorems.FirstOrDefault(t => t.Id == theorem.Id);

                                if (theoremEntity == null)
                                    continue;

                                theorem.IsLeastApplicable = theoremEntity.IsLeast;
                                theorem.IsMostApplicable = theoremEntity.IsMost;
                            }

                            theoremList.IsCompleted = true;

                            var channel = _serviceFactory.CreateChannel<ITheoremListService>();
                            var savedList = channel.SaveTheoremList(theoremList);
                            model.TheoremListRequestId = savedList.TheoremListRequestId;
                            
                        }
                        if (theoremListRequest.TheoremListRequestType.Enum == TheoremListRequestTypeEnum.AsIsToBe
                            && theoremList.TheoremListType.Enum == TheoremListTypeEnum.AsIs)
                        {
                            var nextTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListType.Enum == TheoremListTypeEnum.ToBe);
                            var lang = RouteData.Values["lang"];
                            return Redirect(string.Format("~/{0}/Questionnaire/Index/{1}/{2}", lang, theoremListRequest.VerificationCode, nextTheoremList.VerificationCode));
                        }

                        if(theoremListRequest.TheoremLists.All(tl => tl.IsCompleted))
                        {

                            SendMailToProjectManager(theoremListRequest);
                        }
                    }

                }
                catch (FaultException<TheoremListAlreadyCompletedFault>)
                {
                    model.ErrorMessages.Add(new ErrorMessage(Global.QuestionnaireAlreadyCompleted));
                }
                catch (BusinessException exception)
                {
                    LogManager.LogError(exception.Message, exception);
                    model.ErrorMessages.Add(new ErrorMessage(exception.Message));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception.Message, exception);
                    model.ErrorMessages.Add(new ErrorMessage(exception.Message));
                }

                return View(model);
            }
            model = new QuestionnaireModel { ErrorMessages = new List<ErrorMessage>(), IsCompleted = true };
            return View(model);
        }

        public FileResult GenerateReport(Guid requestId, int languageId)
        {
            try
            {
                var service = _serviceFactory.CreateChannel<ITheoremListService>();

                var result = service.GenerateReport(requestId, languageId, OutputFormat.Pdf);

                return File(Convert.FromBase64String(result), "application/pdf", "questionnaire.pdf");
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
            }

            return File(new byte[0], "application/pdf", "questionnaire.pdf");
        }

        public void SendMailToProjectManager(TheoremListRequest request)
        {
            try
            {
                var crmService = _serviceFactory.CreateChannel<ICrmService>();
                var service = _serviceFactory.CreateChannel<ITheoremListService>();
                var project = crmService.RetrieveProject(request.ProjectId);
                var associate = crmService.RetrieveAssociateByUserId(project.ProjectManagerId);
                request.Contact = crmService.RetrieveContact(request.ContactId);

                var mailServerSetting = service.RetrieveSettingByKey("mailserver");
                var mailFromSetting = service.RetrieveSettingByKey("mailFrom");
                var mailCcSetting = service.RetrieveSettingByKey("mailCc");

                var builder = new StringBuilder();
                var subject = "";

                builder.AppendLine("<div style='font-family: Century Gothic, Verdana;'>Dear, <br /><br />");
                builder.AppendLine("The questionnaire was finished on " + DateTime.Now + "<br/><br/>");
                builder.AppendLine("<u>Additional info</u> <br/>");
                builder.AppendLine("<table style='font-family: Century Gothic, Verdana;'>");
                if(request.CandidateId == null) //request for company
                {
                    
                    builder.AppendLine("<tr><td>Respondent:</td><td>" + request.CrmEmail.FullName + "</td></tr>");
                    builder.AppendLine("<tr><td>Company:</td><td>" + request.Contact.Name + "</td></tr>");
                    builder.AppendLine("<tr><td>Type of questionnaire:</td><td>" + request.TheoremListRequestType.Type + "</td></tr>");
                    builder.AppendLine("<tr><td>Deadline for questionnaire:</td><td>" + request.Deadline.GetValueOrDefault().ToShortDateString() + "</td></tr>");

                    subject = string.Format("Cultural Fit: {0} - {1}", request.CrmEmail.FullName, request.Contact.Name);
                }
                else //request for candidate
                {
                    builder.AppendLine("<tr><td>Respondent:</td><td>" + request.Candidate.FullName + "</td></tr>");
                    builder.AppendLine("<tr><td>Company:</td><td>" + request.Contact.Name + "</td></tr>");
                    builder.AppendLine("<tr><td>Type of questionnaire:</td><td>" + request.TheoremListRequestType.Type + "</td></tr>");
                    builder.AppendLine("<tr><td>Deadline for questionnaire:</td><td>" + request.Deadline.GetValueOrDefault().ToShortDateString() + "</td></tr>");

                    subject = string.Format("Cultural Fit: {0} - {1}", request.Candidate.FullName, request.Contact.Name);
                }
                builder.AppendLine("<tr><td>Results:</td><td><a href='" + ConfigurationManager.AppSettings["WebsiteUrl"] + "/Project/ProjectAssessmentDevelopment/GenerateCulturalFitReport/" + request.Id + "/1'>Nederlands</a></td></tr>");
                builder.AppendLine("<tr><td></td><td><a href='" + ConfigurationManager.AppSettings["WebsiteUrl"] + "/Project/ProjectAssessmentDevelopment/GenerateCulturalFitReport/" + request.Id + "/2'>Français</a></td></tr>");
                builder.AppendLine("<tr><td></td><td><a href='" + ConfigurationManager.AppSettings["WebsiteUrl"] + "/Project/ProjectAssessmentDevelopment/GenerateCulturalFitReport/" + request.Id + "/3'>English</a></td></tr>");
                builder.AppendLine("</table>");
                builder.AppendLine("</div>");
                
                var msg = new MailMessage(mailFromSetting.Value, associate.Email)
                {
                    Body = builder.ToString(),
                    Subject = subject,
                    IsBodyHtml = true
                };

                msg.CC.Add(mailCcSetting.Value);

                var mailClient = new SmtpClient(mailServerSetting.Value);
                mailClient.Send(msg);

            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);

                throw new MailNotSentException("Something went wrong with sending the e-mail.");
            }
        }
    }
}
