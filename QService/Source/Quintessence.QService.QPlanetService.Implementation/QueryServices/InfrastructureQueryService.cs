using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.DataModel.Cam;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QPlanetService.Implementation.CommandServices;
using Quintessence.QService.QueryModel.Prm;
using AutoMapper;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class InfrastructureQueryService : SecuredUnityServiceBase, IInfrastructureQueryService
    {
        public List<LanguageView> ListLanguages()
        {
            LogTrace();

            return Execute(() =>
                {
                    var repository = Container.Resolve<IInfrastructureQueryRepository>();

                    return repository.ListLanguages();
                });
        }

        public List<AssessmentRoomView> ListAssessmentRooms(ListAssessmentRoomsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                if (request.OfficeId.HasValue)
                    return repository.ListOfficeAssessmentRooms(request.OfficeId.Value);
                return repository.ListAssessmentRooms();
            });
        }

        public OfficeView RetrieveOffice(RetrieveOfficeRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                return request.Id.HasValue
                    ? repository.RetrieveOffice(request.Id.Value)
                    : repository.RetrieveOffice(request.ShortName);
            });
        }

        public List<OfficeView> ListOffices()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                return repository.List<OfficeView>();
            });
        }

        public List<AssessorColorView> ListAssessorColors()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                return repository.List<AssessorColorView>();
            });
        }

        public AssessmentRoomView RetrieveAssessmentRoom(Guid roomId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                return repository.Retrieve<AssessmentRoomView>(roomId);
            });
        }

        public MailTemplateView RetrieveMailTemplateByCode(string code)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                return repository.RetrieveMailTemplateByCode(code);
            });
        }

        public MailTemplateView RetrieveMailTemplate(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();
                var commandService = Container.Resolve<IInfrastructureCommandService>();

                var mailTemplate = repository.RetrieveMailTemplate(id);

                var missingTranslationsCount = 0;

                //Check if there are any translations missing for a language, add them if they are.
                var languages = repository.ListLanguages();
                foreach (var language in languages)
                {
                    if (mailTemplate.MailTemplateTranslations.All(mtt => language.Id != mtt.LanguageId))
                    {
                        var createRequest = new CreateMailTemplateTranslationRequest
                            {
                                Body = "",
                                Subject = "",
                                LanguageId = language.Id,
                                MailTemplateId = mailTemplate.Id
                            };
                        commandService.CreateMailTemplateTranslation(createRequest);
                        missingTranslationsCount++;
                    }
                }

                return missingTranslationsCount > 0 ? repository.RetrieveMailTemplate(id) : mailTemplate;
            });
        }

        public MailTemplateTranslationView RetrieveMailTemplateTranslation(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                var mailTemplateTranslation = repository.Retrieve<MailTemplateTranslationView>(id);

                return mailTemplateTranslation;
            });
        }

        public List<MailTemplateView> ListMailTemplates()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                var mailTemplates = repository.List<MailTemplateView>();

                return mailTemplates;
            });
        }

        public List<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IInfrastructureQueryRepository>();

                var mailTemplateTags = repository.ListMailTemplateTags(storedProcedureName, id);

                return mailTemplateTags;
            });
        }

        public CreateEvaluationFormMailResponse CreateEvaluationFormMail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var response = new CreateEvaluationFormMailResponse
                {
                    To = "",
                    Bcc = "",
                    Subject = "",
                    Body = ""
                };

                var repository = Container.Resolve<IInfrastructureQueryRepository>();
                var prmQueryService = Container.Resolve<IProjectManagementQueryService>();

                var evaluationForm = prmQueryService.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest { Id = id });

                MailTemplateView mailTemplate;
                switch (evaluationForm.EvaluationFormTypeId)
                {
                    case (int)EvaluationFormEnumType.Acdc:
                        mailTemplate = repository.RetrieveMailTemplateByCode("EVALACDC");
                        break;
                    case (int)EvaluationFormEnumType.Coaching:
                        mailTemplate = repository.RetrieveMailTemplateByCode("EVALCOACH");
                        break;
                    case (int)EvaluationFormEnumType.CustomProjects:
                        mailTemplate = repository.RetrieveMailTemplateByCode("EVALCUSTOM");
                        break;
                    default:
                        mailTemplate = null;
                        break;
                }

                if (mailTemplate != null)
                {
                    var mailTemplateTranslation = mailTemplate.MailTemplateTranslations.FirstOrDefault(mtt => mtt.LanguageId == evaluationForm.LanguageId);

                    var body = ReplaceTagsWithValues(repository, mailTemplate.StoredProcedureName, mailTemplateTranslation.Body, id);

                    if (!string.IsNullOrWhiteSpace(mailTemplate.BccAddress))
                        response.Bcc = mailTemplate.BccAddress.Replace(',', ';').TrimEnd(';');

                    response.Subject = mailTemplateTranslation.Subject ?? "";
                    response.Body = body;
                    response.To = evaluationForm.Email ?? "";
                }

                return response;
            });
        }

        public CreateProjectCandidateInvitationMailResponse CreateProjectCandidateInvitationMail(Guid id)
        {
            LogTrace();

            return Execute(() =>
                {
                    var response = new CreateProjectCandidateInvitationMailResponse
                        {
                            To = "",
                            Bcc = "",
                            Subject = "",
                            Body = ""
                        };
                    var repository = Container.Resolve<IInfrastructureQueryRepository>();
                    var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
                    var projectCandidate = prmQueryService.RetrieveProjectCandidateDetail(id);
                    var username = projectCandidate.CandidateEmail;
                    string password = null;
                    string languageCode = null;
                    var qCandidateAccess = QCandidateAccess.NoAccess;

                    if(projectCandidate.CandidateHasQCandidateAccess)
                    {
                        var infrastructureService = Container.Resolve<IInfrastructureQueryService>();
                        var languages = infrastructureService.ListLanguages();
                        languageCode = languages.SingleOrDefault(l => l.Id == projectCandidate.CandidateLanguageId)?.Code;

                        if(!projectCandidate.CandidateQCandidateUserId.HasValue)
                        {
                            //Create user in Azure AD B2C
                            var graphService = Container.Resolve<IGraphService>();
                            password = GenerateNewPassword(3, 3, 2);
                            var qCandidateUserId = graphService.CreateUser(projectCandidate.CandidateFirstName, projectCandidate.CandidateLastName, languageCode, projectCandidate.CandidateEmail, projectCandidate.CandidateId, password);

                            var candidateManagementCommandService = Container.Resolve<ICandidateManagementCommandService>();
                            candidateManagementCommandService.SetCandidateQCandidateUserId(projectCandidate.CandidateId, qCandidateUserId);
                            qCandidateAccess = QCandidateAccess.UserCreated;
                        }
                        else
                        {
                            qCandidateAccess = QCandidateAccess.UserAlreadyExists;
                        }
                    }

                    var projectCandidateCategoryDetails = prmQueryService.ListProjectCandidateCategoryDetailTypes(projectCandidate.Id);
                    var projectCategoryDetails = prmQueryService.ListProjectCategoryDetails(projectCandidate.ProjectId);

                    var includedProjectCategoryDetailIds = new List<Guid>();
                    includedProjectCategoryDetailIds.AddRange(projectCategoryDetails.OfType<ProjectCategoryDetailType1View>().Where(pcdt => pcdt.SurveyPlanningId == 2 || pcdt.SurveyPlanningId == 3).Select(pcdt => pcdt.Id));
                    includedProjectCategoryDetailIds.AddRange(projectCategoryDetails.OfType<ProjectCategoryDetailType2View>().Where(pcdt => pcdt.SurveyPlanningId == 2 || pcdt.SurveyPlanningId == 3).Select(pcdt => pcdt.Id));
                    includedProjectCategoryDetailIds.AddRange(projectCategoryDetails.OfType<ProjectCategoryDetailType3View>().Where(pcdt => pcdt.SurveyPlanningId == 2 || pcdt.SurveyPlanningId == 3).Select(pcdt => pcdt.Id));

                    var mailTemplate = repository.RetrieveMailTemplateByCode("CANDINVITE");

                    if (mailTemplate != null)
                    {
                        var mailTemplateTranslation = mailTemplate.MailTemplateTranslations.FirstOrDefault(mtt => mtt.LanguageId == projectCandidate.CandidateLanguageId);

                        if (mailTemplateTranslation != null)
                        {
                            var body = ReplaceTagsWithValues(repository, mailTemplate.StoredProcedureName, mailTemplateTranslation.Body, id);

                            var subCategoryStringBuilder = new StringBuilder();
                            foreach (var projectCategoryDetailType in projectCandidateCategoryDetails.Where(pccd => includedProjectCategoryDetailIds.Contains(pccd.ProjectCategoryDetailTypeId)))
                            {
                                var detailType1 = projectCategoryDetailType as ProjectCandidateCategoryDetailType1View;
                                if (detailType1 != null)
                                {
                                    var detailType1MailTemplate = repository.RetrieveMailTemplateByCode("DETAILTYPE1");
                                    var detailType1MailTemplateTranslation = detailType1MailTemplate.MailTemplateTranslations
                                                                    .FirstOrDefault(mtt => mtt.LanguageId == projectCandidate.CandidateLanguageId);
                                    var detailType1Body = ReplaceTagsWithValues(repository, detailType1MailTemplate.StoredProcedureName, detailType1MailTemplateTranslation.Body, detailType1.Id);

                                    subCategoryStringBuilder.Append(detailType1Body);
                                }

                                var detailType2 = projectCategoryDetailType as ProjectCandidateCategoryDetailType2View;
                                if (detailType2 != null)
                                {
                                    var detailType2MailTemplate = repository.RetrieveMailTemplateByCode("DETAILTYPE2");
                                    var detailType2MailTemplateTranslation = detailType2MailTemplate.MailTemplateTranslations
                                                                    .FirstOrDefault(mtt => mtt.LanguageId == projectCandidate.CandidateLanguageId);
                                    var detailType2Body = ReplaceTagsWithValues(repository, detailType2MailTemplate.StoredProcedureName, detailType2MailTemplateTranslation.Body, projectCategoryDetailType.Id);

                                    subCategoryStringBuilder.Append(detailType2Body);
                                }

                                var detailType3 = projectCategoryDetailType as ProjectCandidateCategoryDetailType3View;
                                if (detailType3 != null)
                                {
                                    var detailType3MailTemplate = repository.RetrieveMailTemplateByCode("DETAILTYPE3");
                                    var detailType3MailTemplateTranslation = detailType3MailTemplate.MailTemplateTranslations
                                                                    .FirstOrDefault(mtt => mtt.LanguageId == projectCandidate.CandidateLanguageId);
                                    var detailType3Body = ReplaceTagsWithValues(repository, detailType3MailTemplate.StoredProcedureName, detailType3MailTemplateTranslation.Body, projectCategoryDetailType.Id);

                                    subCategoryStringBuilder.Append(detailType3Body);
                                }

                            }

                            //Check if candidate has cultural fit.
                            var theoremListRequest =
                                prmQueryService.RetrieveTheoremListRequestByProjectAndCandidate(
                                    projectCandidate.ProjectId, projectCandidate.CandidateId);
                            if (theoremListRequest != null)
                            {
                                var culturalFitMailTemplate = repository.RetrieveMailTemplateByCode("CANDCULTFIT");
                                var culturalFitMailTemplateTranslation = culturalFitMailTemplate.MailTemplateTranslations
                                                                .FirstOrDefault(mtt => mtt.LanguageId == projectCandidate.CandidateLanguageId);
                                var culturalFitBody = ReplaceTagsWithValues(repository, culturalFitMailTemplate.StoredProcedureName, culturalFitMailTemplateTranslation.Body, projectCandidate.Id);

                                subCategoryStringBuilder.Append(culturalFitBody);
                            }

                            //Add simulation context logins to email
                            var simulationContextLogins = prmQueryService.ListSimulationContextLogins(projectCandidate.Id);
                            body = body.Replace(@"&lt;!--SIMCONLOGINS--&gt;", CreateSimulationContextLoginsEmailBody(simulationContextLogins));
                            //Add QCandidate login to email
                            body = body.Replace(@"&lt;!--QCANDIDATELOGIN--&gt;", CreateQCandidateLoginsEmailBody(qCandidateAccess, languageCode, username, password));

                            body = body.Replace(@"&lt;!--SUBCATEGORIES--&gt;", subCategoryStringBuilder.ToString());

                            //Replace the subject by tags
                            var subject = mailTemplateTranslation.Subject;

                            if (subject != null)
                                subject = ReplaceTagsWithValues(repository, mailTemplate.StoredProcedureName, subject, id);
                            
                            if (!string.IsNullOrWhiteSpace(mailTemplate.BccAddress))
                                response.Bcc = mailTemplate.BccAddress.Replace(',', ';').TrimEnd(';');

                            response.Subject = subject ?? "";
                            response.Body = body;
                            response.To = projectCandidate.CandidateEmail ?? "";
                        }
                    }

                    return response;

                });
        }

        private string CreateSimulationContextLoginsEmailBody(List<SimulationContextLoginView> simulationContextLogins)
        {
            var builder = new StringBuilder();
            if (simulationContextLogins.Any())
            {
                foreach (var login in simulationContextLogins)
                {
                    builder.Append(string.Format("{0} - ", login.ValidFrom.ToShortDateString()));
                    builder.Append(string.Format("{0}<br />", login.ValidTo.ToShortDateString()));
                    builder.Append(string.Format("Username: {0}<br />", login.UserName));
                    builder.Append(string.Format("Password: {0}<br />", login.Password));
                    builder.Append(string.Format("<br />"));
                }
            }
            return builder.ToString();
        }

        private static string CreateQCandidateLoginsEmailBody(QCandidateAccess qCandidateAccess, string language, string username, string password)
        {
            if(qCandidateAccess == QCandidateAccess.NoAccess)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            var url = ConfigurationManager.AppSettings["QCandidateUrl"];

            if(qCandidateAccess == QCandidateAccess.UserCreated)
            {
                var gotAccess = string.Empty;
                var usernameLabel = string.Empty;
                var passwordLabel = string.Empty;
                var qCandidateUrl = $"<a href=\"{url}\"><strong>{url}</strong></a>";

                switch(language?.ToLower())
                {
                    case "nl":
                        gotAccess = $"U heeft toegang tot ons QCandidate-platform op: {qCandidateUrl} en u kunt hierop inloggen met de volgende informatie:";
                        usernameLabel = "Gebruikersnaam";
                        passwordLabel = "Paswoord";
                        break;
                    case "en":
                        gotAccess = $"You have access to our QCandidate-platform at: {qCandidateUrl} and can login to it using the following information:";
                        usernameLabel = "Username";
                        passwordLabel = "Password";
                        break;
                    case "fr":
                        gotAccess = $"Vous avez accès à notre plateforme QCandidate sur: {qCandidateUrl} et pouvez vous y connecter en utilisant les informations suivantes:";
                        usernameLabel = "Nom d'utilisateur";
                        passwordLabel = "Mot de passe";
                        break;
                    case "de":
                        gotAccess = $"Sie haben Zugriff auf unsere QCandidate-Plattform unter: {qCandidateUrl} und können sich mit folgenden Informationen anmelden:";
                        usernameLabel = "Nutzername";
                        passwordLabel = "Passwort";
                        break;
                }

                builder.Append(gotAccess);
                builder.Append("<br /><strong><span style=\"color: #002649; font-family: calibri; font-size: 11pt;\">");
                builder.Append($"{usernameLabel}: {username}<br />");
                builder.Append($"{passwordLabel}: {password}<br />");
                builder.Append("</span></strong></p>");
            }
            else if(qCandidateAccess == QCandidateAccess.UserAlreadyExists)
            {
                var gotAccess = string.Empty;
                var qCandidateUrl = $"<a href=\"{url}\"><strong>{url}</strong></a>";

                switch(language?.ToLower())
                {
                    case "nl":
                        gotAccess = $"U heeft toegang tot ons QCandidate-platform op: {qCandidateUrl} en kunt hierop inloggen met de eerder verzonden informatie.";
                        break;
                    case "en":
                        gotAccess = $"You have access to our QCandidate-platform at: {qCandidateUrl} and can login to it using the information sent previously.";
                        break;
                    case "fr":
                        gotAccess = $"Vous avez accès à notre plateforme QCandidate sur: {qCandidateUrl} et pouvez vous y connecter en utilisant les informations envoyées précédemment.";
                        break;
                    case "de":
                        gotAccess = $"Sie haben Zugriff auf unsere QCandidate-Plattform unter: {qCandidateUrl} und können sich mit den zuvor gesendeten Informationen anmelden.";
                        break;
                }

                builder.Append(gotAccess);
            }

            builder.Append("<br />");

            return builder.ToString();
        }

        public string RetrieveAllowedPasswordChars()
        {
            LogTrace();

            return Execute(() => ConfigurationManager.AppSettings["AllowedPasswordChars"] ?? "ABCDEFGHJKMNPQRSTUVWXYZ23456789");
        }

        public List<JobDefinitionView> ListJobDefinitions()
        {
            LogTrace();

            return Execute(() =>
                {
                    var repository = Container.Resolve<IInfrastructureQueryRepository>();

                    var jobDefinitions = repository.ListJobDefinitions();

                    return jobDefinitions;
                });
        }

        public CreateCulturalFitInvitationMailResponse CreateCulturalFitInvitationMail(Guid id, int languageId)
        {
            LogTrace();

            return Execute(() =>
                {
                    var repository = Container.Resolve<IInfrastructureQueryRepository>();
                    var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
                    var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
                    var response = new CreateCulturalFitInvitationMailResponse
                    {
                        To = "",
                        Bcc = "",
                        Subject = "",
                        Body = ""
                    };

                    var theoremListRequest = prmQueryService.RetrieveTheoremListRequest(id);
                    var crmEmail = crmQueryService.RetrieveCrmEmail(theoremListRequest.CrmEmailId.GetValueOrDefault());

                    var culturalFitMailTemplate = repository.RetrieveMailTemplateByCode("CUSTCULTFIT");
                    var culturalFitMailTemplateTranslation = culturalFitMailTemplate.MailTemplateTranslations
                                                    .FirstOrDefault(mtt => mtt.LanguageId == languageId);

                    var body = ReplaceTagsWithValues(repository, culturalFitMailTemplate.StoredProcedureName, culturalFitMailTemplateTranslation.Body, id, languageId);

                    if (!string.IsNullOrWhiteSpace(culturalFitMailTemplate.BccAddress))
                        response.Bcc = culturalFitMailTemplate.BccAddress.Replace(',', ';').TrimEnd(';');

                    response.Subject = culturalFitMailTemplateTranslation.Subject ?? "";
                    response.Body = body;
                    response.To = crmEmail != null ? crmEmail.Email ?? string.Empty : string.Empty;

                    return response;
                });
        }

        /// <summary>
        /// Searches the specified term.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public SearchResponse Search(string term)
        {
            LogTrace();

            return Execute(() =>
                {
                    var response = new SearchResponse();
                    response.Results = new List<SearchResultView>();

                    using (var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>())
                    using (var candidateManagementQueryRepository = Container.Resolve<ICandidateManagementQueryRepository>())
                    {
                        var terms = term.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                        response.Results.AddRange(projectManagementQueryRepository.List<ProjectView>(projects => projects.Where(project => terms.All(t => project.Name.Contains(t)))).Select(p => new ProjectSearchResultView { Id = p.Id, Name = p.Name }));
                        response.Results.AddRange(candidateManagementQueryRepository.List<CandidateView>(candidates => candidates.Where(candidate => terms.All(t => candidate.FirstName.Contains(t)) || terms.Any(t => candidate.LastName.Contains(t)))).OrderByDescending(candidate => candidate.Audit.CreatedOn).Select(c => new CandidateSearchResultView { Id = c.Id, Name = c.FullName }));

                        int number;

                        if (int.TryParse(term, out number))
                        {
                            response.Results.AddRange(projectManagementQueryRepository.List<ProjectCandidateView>(projectCandidates => projectCandidates.Where(projectCandidate => projectCandidate.CrmCandidateInfoId == number)).Select(p => new CandidateSearchResultView{Id = p.CandidateId, Name = string.Format("{0} {1}", p.CrmCandidateInfoId, p.CandidateFullName)}));   
                        }
                    }

                    return response;
                });
        }

        /// <summary>
        /// Replaces the mail tags with values.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="text">The text.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private string ReplaceTagsWithValues(IInfrastructureQueryRepository repository, string storedProcedureName, string text, Guid id)
        {
            var mailTemplateTags = repository.ListMailTemplateTags(storedProcedureName, id);

            if (mailTemplateTags.Any())
            {
                var builder = new StringBuilder(text);
                foreach (var tag in mailTemplateTags)
                {
                    builder.Replace("&lt;!--" + tag.Tag + "--&gt;", tag.Value);
                    builder.Replace("<!--" + tag.Tag + "-->", tag.Value);
                }
                return builder.ToString();
            }

            //No tags found.
            return text;
        }


        /// <summary>
        /// Replaces the mail tags with values (overload with language id).
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="text">The text.</param>
        /// <param name="id">The id.</param>
        /// <param name="languageId">The language id.</param>
        /// <returns></returns>
        private string ReplaceTagsWithValues(IInfrastructureQueryRepository repository, string storedProcedureName, string text, Guid id, int languageId)
        {
            var mailTemplateTags = repository.ListMailTemplateTags(storedProcedureName, id, languageId);

            if (mailTemplateTags.Any())
            {
                var builder = new StringBuilder(text);
                foreach (var tag in mailTemplateTags)
                {
                    builder.Replace("&lt;!--" + tag.Tag + "--&gt;", tag.Value);
                }
                return builder.ToString();
            }

            //No tags found.
            return text;
        }

        private static string GenerateNewPassword(int lowercase, int uppercase, int numerics)
        {
            var lowers = "abcdefghijklmnopqrstuvwxyz";
            var uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var number = "0123456789";

            var random = new Random();
            var generated = "!";

            for(var i = 0; i < lowercase; ++i)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for(var i = 0; i < uppercase; ++i)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for(var i = 0; i < numerics; ++i)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }

        private enum QCandidateAccess
        {
            NoAccess = 0,
            UserCreated = 1,
            UserAlreadyExists = 2
        }
    }
}
