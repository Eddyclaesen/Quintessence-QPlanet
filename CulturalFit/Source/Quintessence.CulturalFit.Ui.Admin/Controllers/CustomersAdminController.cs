using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Reports;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.UI.Admin.Models.Admin;
using Quintessence.CulturalFit.UI.Core.Exceptions;
using Quintessence.CulturalFit.UI.Core.Service;

namespace Quintessence.CulturalFit.UI.Admin.Controllers
{
    public class CustomersAdminController : Controller
    {
        #region Private fields
        private readonly ServiceFactory _serviceFactory;
        #endregion

        #region Constructor(s)
        public CustomersAdminController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index view for listing the customer requests.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Index(int? projectId)
        {
            var model = new CustomerModel
                            {
                                ErrorMessages = new List<string>()
                            };
            try
            {
                var service = _serviceFactory.CreateChannel<IAdminService>();
                var contact = service.RetrieveContactByProjectId(projectId.GetValueOrDefault());
                var contactFound = service.RetrieveContactWithRequests(contact.Id);
                model = Mapper.Map<CustomerModel>(contactFound);
                model.ProjectId = projectId.GetValueOrDefault();
                model.ErrorMessages = new List<string>();

                //Set languages
                var theoremListService = _serviceFactory.CreateChannel<ITheoremListService>();
                var languages = theoremListService.ListLanguages();
                model.Languages = languages;
                model.SelectedLanguageId = languages.FirstOrDefault() == null ? 1 : languages.FirstOrDefault().Id;

                return View(model);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
                throw new Exception("Something went wrong with listing the customer requests... Please retry later."); 
            }
        }
        #endregion

        #region Create customer request
        /// <summary>
        /// View for creating the customer request.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public ActionResult CreateCustomerRequest(int? projectId)
        {
            var model = new CustomerRequestModel
            {
                ErrorMessages = new List<string>()
            };
            try
            {

                var service = _serviceFactory.CreateChannel<IAdminService>();
                var theoremListService = _serviceFactory.CreateChannel<ITheoremListService>();
                var languages = theoremListService.ListLanguages();
                var contact = service.RetrieveContactByProjectId(projectId.GetValueOrDefault());
                var requestTypes = service.ListTheoremListRequestTypes();
                model = new CustomerRequestModel
                {
                    ProjectId = projectId.GetValueOrDefault(),
                    ContactId = contact.Id,
                    ContactName = contact.Name,
                    Deadline = DateTime.Now.AddDays(1),
                    Languages = languages,
                    TheoremListRequestTypes = requestTypes,
                    ErrorMessages = new List<string>(),
                    IsMailSent = false,
                    SelectedLanguageId = 1,
                    SelectedGenderId = 1
                };
                return View(model);
            }
            catch (Exception exception)
            {
                model.ErrorMessages.Add(exception.Message);
                LogManager.LogError(exception.Message, exception);
            }
            return View(model);
        }

        /// <summary>
        /// Creates the customer request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCustomerRequest(CustomerRequestModel model)
        {
            model.ErrorMessages = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    SaveRequest(model);

                    return RedirectToAction("Index", new { projectId = model.ProjectId });
                }
                catch (Exception exception)
                {
                    model.ErrorMessages.Add(exception.Message);
                    LogManager.LogError(exception.Message, exception);
                }
            }
            try
            {
                var adminService = _serviceFactory.CreateChannel<IAdminService>();
                var requestTypes = adminService.ListTheoremListRequestTypes();
                model.TheoremListRequestTypes = requestTypes;
            }
            catch (Exception exception)
            {
                model.ErrorMessages.Add(exception.Message);
                LogManager.LogError(exception.Message, exception);
            }

            return View(model);
        }

        /// <summary>
        /// Creates the customer request and sends the email.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCustomerRequestAndSend(CustomerRequestModel model)
        {
            model.ErrorMessages = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    var request = SaveRequest(model);
                    SendMailToContactPerson(request);

                    return RedirectToAction("Index", new { projectId = model.ProjectId });
                }
                catch (MailNotSentException exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message + " Nonetheless the request is saved, try again later.");
                    model.ErrorMessages.Add(exception.Message);
                }
                catch (Exception exception)
                {
                    model.ErrorMessages.Add(exception.Message);
                    LogManager.LogError(exception.Message, exception);
                }
            }
            try
            {
                var adminService = _serviceFactory.CreateChannel<IAdminService>();
                var requestTypes = adminService.ListTheoremListRequestTypes();
                var theoremListService = _serviceFactory.CreateChannel<ITheoremListService>();
                var languages = theoremListService.ListLanguages();
                model.Languages = languages;
                model.TheoremListRequestTypes = requestTypes;
                model.ErrorMessages = new List<string>();
            }

            catch (Exception exception)
            {
                model.ErrorMessages.Add(exception.Message);
                LogManager.LogError(exception.Message, exception);
            }

            return View("CreateCustomerRequest", model);
        }
        #endregion

        #region Edit customer request
        /// <summary>
        /// View for editing the customer request.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="requestId">The request id.</param>
        /// <returns></returns>
        public ActionResult EditCustomerRequest(int? projectId, Guid? requestId)
        {
            var model = new CustomerRequestModel
            {
                ErrorMessages = new List<string>()
            };
            try
            {
                var service = _serviceFactory.CreateChannel<ITheoremListService>();
                var languages = service.ListLanguages();
                var request = new RetrieveTheoremListRequestRequest(requestId.GetValueOrDefault());
                var retrievedRequest = service.RetrieveTheoremListRequest(request);
                var adminService = _serviceFactory.CreateChannel<IAdminService>();
                var requestTypes = adminService.ListTheoremListRequestTypes();

                model = new CustomerRequestModel
                {
                    TheoremListRequestId = request.Id,
                    ProjectId = projectId.GetValueOrDefault(),
                    ContactId = retrievedRequest.ContactId,
                    ContactName = retrievedRequest.Contact.Name,
                    ContactPersonId = retrievedRequest.ContactPersonId,
                    Deadline = retrievedRequest.Deadline.GetValueOrDefault(),
                    Email = retrievedRequest.ContactPerson.Email,
                    FirstName = retrievedRequest.ContactPerson.FirstName,
                    LastName = retrievedRequest.ContactPerson.LastName,
                    ErrorMessages = new List<string>(),
                    SelectedGenderId = retrievedRequest.ContactPerson.Gender,
                    SelectedTheoremListRequestTypeId = retrievedRequest.TheoremListRequestTypeId,
                    SelectedLanguageId = retrievedRequest.ContactPerson.LanguageId,
                    Languages = languages,
                    TheoremListRequestTypes = requestTypes,
                    Gender = retrievedRequest.ContactPerson.GenderType,
                    IsMailSent = retrievedRequest.IsMailSent
                };
                if (TempData["SaveSuccess"] == null)
                    TempData["SaveSuccess"] = false;

            }
            catch (Exception exception)
            {
                model.ErrorMessages.Add(exception.Message);
                LogManager.LogError(exception.Message, exception);
            }
            return View(model);
        }

        /// <summary>
        /// Edits the customer request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCustomerRequest(CustomerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var service = _serviceFactory.CreateChannel<ITheoremListService>();

                    var theoremListRequest = service.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(model.TheoremListRequestId));

                    theoremListRequest.Deadline = model.Deadline;

                    if (model.ContactPersonId == null)
                    {
                        var contactPerson = new ContactPerson
                        {
                            Id = Guid.NewGuid(),
                            Email = model.Email,
                            FirstName = model.FirstName,
                            Gender = model.SelectedGenderId,
                            LastName = model.LastName,
                            LanguageId = model.SelectedLanguageId
                        };
                        theoremListRequest.ContactPerson = contactPerson;
                        theoremListRequest.ContactPersonId = contactPerson.Id;
                    }
                    else
                        theoremListRequest.ContactPerson.LanguageId = model.SelectedLanguageId;

                    service.SaveTheoremListRequest(theoremListRequest);
                    TempData["SaveSuccess"] = true;


                }
                catch (Exception exception)
                {
                    model.ErrorMessages.Add(exception.Message);
                    LogManager.LogError(exception.Message, exception);
                }
            }
            return RedirectToAction("EditCustomerRequest", new { projectId = model.ProjectId, requestId = model.TheoremListRequestId });

        }

        /// <summary>
        /// Edits the customer request and sends the email to the contact person.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCustomerRequestAndSend(CustomerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var service = _serviceFactory.CreateChannel<ITheoremListService>();

                    var theoremListRequest = service.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(model.TheoremListRequestId));

                    SendMailToContactPerson(theoremListRequest);

                    return RedirectToAction("Index", new { projectId = model.ProjectId });
                }
                catch (MailNotSentException exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message + " The request is saved, try again later.");
                    model.ErrorMessages.Add(exception.Message);
                }
                catch (Exception exception)
                {
                    model.ErrorMessages.Add(exception.Message);
                    LogManager.LogError(exception.Message, exception);
                }
            }
            return RedirectToAction("EditCustomerRequest", new { projectId = model.ProjectId, requestId = model.TheoremListRequestId });

        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Saves the request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private TheoremListRequest SaveRequest(CustomerRequestModel model)
        {
            var service = _serviceFactory.CreateChannel<IAdminService>();
            
            var request = new CustomerRequest
            {
                ContactId = model.ContactId,
                ContactPersonId = model.ContactPersonId,
                ContactPersonEmail = model.Email,
                ContactPersonFirstName = model.FirstName,
                ContactPersonLastName = model.LastName,
                ContactPersonGender = model.SelectedGenderId,
                Deadline = model.Deadline,
                TheoremListRequestTypeId = model.SelectedTheoremListRequestTypeId,
                LanguageId = model.SelectedLanguageId
            };

            var savedTheoremListRequest = service.CreateCustomerRequest(request);

            service.LinkProjectTheoremListRequest(model.ProjectId, savedTheoremListRequest.Id);

            return savedTheoremListRequest;
        }

        /// <summary>
        /// Sends the mail to the contact person.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Quintessence.CulturalFit.UI.Core.Exceptions.MailNotSentException"></exception>
        private TheoremListRequest SendMailToContactPerson(TheoremListRequest request)
        {
            try
            {
                var adminService = _serviceFactory.CreateChannel<IAdminService>();
                var template = adminService.RetrieveEmailTemplate(request.ContactPerson.LanguageId, request.TheoremListRequestTypeId);
                var mailServerSetting = adminService.RetrieveSettingByKey("mailserver");
                var mailFromSetting = adminService.RetrieveSettingByKey("mailFrom");
                var siteUrlSetting = adminService.RetrieveSettingByKey("siteUrl");
                var companyNameSetting = adminService.RetrieveSettingByKey("companyName");

                var lang = ConvertLanguageIdToLanguageCode(request.ContactPerson.LanguageId);

                var body = template.Body;
                body = body.Replace("<!--siteUrl-->", string.Format(siteUrlSetting.Value, lang));
                body = body.Replace("<!--verificationCode-->", request.VerificationCode);
                body = body.Replace("<!--deadline-->", request.Deadline.GetValueOrDefault().ToShortDateString());
                body = body.Replace("<!--companyName-->", companyNameSetting.Value);

                var msg = new MailMessage(mailFromSetting.Value, request.ContactPerson.Email)
                              {
                                  Body = body,
                                  Subject = template.Subject,
                                  IsBodyHtml = true
                              };


                var mailClient = new SmtpClient(mailServerSetting.Value);
                mailClient.Send(msg);

                return adminService.UpdateMailStatus(request.Id, true);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);

                throw new MailNotSentException("Something went wrong with sending the e-mail.");
            }
        }
        #endregion

        #region Other methods
        /// <summary>
        /// Finds the contact person(s).
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="maxResults">The max results.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindContactPersons(string searchText, int maxResults)
        {
            try
            {
                var service = _serviceFactory.CreateChannel<IAdminService>();

                var contactPersons = service.SearchContactPerson(searchText, maxResults);

                return Json(contactPersons);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
            }
            return Json(null);
        }

        /// <summary>
        /// Reopens the questionnaire.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="requestId">The request id.</param>
        /// <returns></returns>
        public ActionResult ReopenQuestionnaire(int? projectId, Guid? requestId)
        {
            try
            {

                var service = _serviceFactory.CreateChannel<ITheoremListService>();

                var theoremListRequest = service.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(requestId.GetValueOrDefault()));

                foreach (var theoremList in theoremListRequest.TheoremLists)
                {
                    theoremList.IsCompleted = false;
                }

                service.SaveTheoremListRequest(theoremListRequest);


            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
            }
            return RedirectToAction("Index", new { projectId = projectId });
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="languageId">The language id.</param>
        /// <returns></returns>
        public FileResult GenerateReport(Guid requestId, int languageId)
        {
            try
            {
                var adminService = _serviceFactory.CreateChannel<IAdminService>();

                var result = adminService.GenerateReport(requestId, languageId, OutputFormat.Pdf);

                var lang = ConvertLanguageIdToLanguageCode(languageId);

                return File(Convert.FromBase64String(result), "application/pdf", string.Format("questionnaire-{0}.pdf", lang));
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception.Message, exception);
            }

            return File(new byte[0], "application/pdf", "questionnaire.pdf");
        }

        /// <summary>
        /// Converts the language id to language code.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public string ConvertLanguageIdToLanguageCode(int id)
        {
            string lang;

            switch (id)
            {
                case 1:
                    lang = "nl";
                    break;
                case 2:
                    lang = "fr";
                    break;
                default:
                    lang = "en";
                    break;
            }

            return lang;
        }
        #endregion
    }
}
