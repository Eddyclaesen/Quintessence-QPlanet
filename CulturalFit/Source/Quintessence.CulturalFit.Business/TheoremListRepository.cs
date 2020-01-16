using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Security.Principal;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Business.Base;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.Business.ReportExecutionReference;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Reports;
using Quintessence.CulturalFit.Infra.Configuration;
using Quintessence.CulturalFit.Infra.Exceptions;
using Quintessence.CulturalFit.Infra.Logging;

namespace Quintessence.CulturalFit.Business
{
    public class TheoremListRepository : BaseRepository, ITheoremListRepository
    {
        #region Constructor(s)
        /// <summary>
        /// Initializes a new instance of the <see cref="TheoremListRepository"/> class.
        /// </summary>
        /// <param name="unityContainer">The unity container.</param>
        public TheoremListRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }
        #endregion

        #region Retrieve methods
        /// <summary>
        /// Retrieves the theorem list.
        /// </summary>
        /// <param name="theoremListId">The theorem list id.</param>
        /// <returns></returns>
        public TheoremList RetrieveTheoremList(Guid theoremListId)
        {
            using (new DurationLog())
            {
                try
                {
                    TheoremList theoremList = null;
                    using (var context = CreateContext())
                    {
                        theoremList =
                            context.TheoremLists
                                .Include(tl => tl.Theorems.Select(t => t.TheoremTranslations))

                                .FirstOrDefault(tl => tl.Id == theoremListId);
                    }
                    return theoremList;
                }
                catch (Exception exception)
                {
                    const string message = "Unable to retrieve theoremlist.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        public TheoremListRequest RetrieveTheoremListRequest(string verificationCode)
        {
            using (new DurationLog())
            {
                try
                {
                    TheoremListRequest theoremListRequest = null;
                    using (var context = CreateContext())
                    {
                        theoremListRequest =
                            context.TheoremListRequests
                                //.Include(tlr => tlr.Contact)
                                .Include(tlr => tlr.CrmEmail)
                                .Include(tlr => tlr.Candidate)
                                .Include(tlr => tlr.TheoremLists.Select(tl => tl.TheoremListType))
                                .Include(tlr => tlr.TheoremLists.Select(tl => tl.Theorems.Select(t => t.TheoremTranslations)))
                                .Include(tlr => tlr.TheoremListRequestType)

                                .FirstOrDefault(tlr => tlr.VerificationCode == verificationCode);
                    }

                    if (theoremListRequest == null)
                        throw new ArgumentException(string.Format("Unknown verification code '{0}'.", verificationCode));

                    return theoremListRequest;
                }
                catch (Exception exception)
                {
                    const string message = "Unable to retrieve theoremlistrequest.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        /// <summary>
        /// Retrieves the theorem list request.
        /// </summary>
        /// <param name="theoremListRequestId">The theorem list request id.</param>
        /// <returns></returns>
        public TheoremListRequest RetrieveTheoremListRequest(Guid theoremListRequestId)
        {
            using (new DurationLog())
            {
                try
                {
                    TheoremListRequest theoremListRequest = null;
                    using (var context = CreateContext())
                    {
                        theoremListRequest = context.TheoremListRequests
                            .Include(tlr => tlr.Contact)
                            .Include(tlr => tlr.TheoremListRequestType)

                            .Include(tlr => tlr.TheoremLists.Select(tl => tl.TheoremListType))
                            .Include(tlr => tlr.TheoremLists.Select(tl => tl.Theorems.Select(t => t.TheoremTranslations)))

                            .FirstOrDefault(tlr => tlr.Id == theoremListRequestId);
                    }
                    return theoremListRequest;
                }
                catch (Exception exception)
                {
                    const string message = "Unable to retrieve theoremlist request.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        #endregion

        #region List methods
        public List<Language> ListLanguages()
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Languages.ToList();
                    }
                }
                catch (Exception exception)
                {
                    const string message = "Unable to retrieve languages.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        #endregion

        #region Save & create methods
        public void Save(TheoremList theoremList)
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.Save(theoremList);
                        context.SaveChanges();
                    }

                }
                catch (Exception exception)
                {
                    const string message = "Unable to save theoremlist.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }


        /// <summary>
        /// Saves the specified theorem list request.
        /// </summary>
        /// <param name="theoremListRequest">The theorem list request.</param>
        public void Save(TheoremListRequest theoremListRequest)
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.Save(theoremListRequest);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    const string message = "Unable to save theoremlist request.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }
        #endregion

        #region Other methods
        public TheoremListRequest ValidateVerificationCode(string verificationCode)
        {
            using (new DurationLog())
            {
                try
                {
                    TheoremListRequest theoremListRequest = null;
                    using (var context = CreateContext())
                    {
                        theoremListRequest =
                            context.TheoremListRequests
                            .Include(tlr => tlr.TheoremListRequestType)
                            .FirstOrDefault(tlr => tlr.VerificationCode == verificationCode);
                    }

                    if (theoremListRequest == null)
                        throw new BusinessException(string.Format("Unknown verification code '{0}'.", verificationCode));

                    return theoremListRequest;
                }
                catch (BusinessException exception)
                {
                    LogManager.LogError(string.Format("No theoremlist for verification code: {0}", verificationCode), exception);
                    throw;
                }
                catch (Exception exception)
                {
                    const string message = "Unable to verify code.";
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        public void RegisterTheoremCheck(Guid theoremId, bool isLeastApplicable, bool isMostApplicable)
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var theorem = context.Theorems.Single(t => t.Id == theoremId);
                        theorem.IsLeastApplicable = isLeastApplicable;
                        theorem.IsMostApplicable = isMostApplicable;
                        context.SaveChanges();
                    }
                }
                catch (BusinessException exception)
                {
                    LogManager.LogError(string.Format("Unable to register theorem check for id '{0}'.", theoremId), exception);
                    throw;
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to register theorem check for id '{0}'.", theoremId);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="theoremListRequestId">The theorem list request id.</param>
        /// <param name="languageId">The language id.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        /// <exception cref="Quintessence.CulturalFit.Infra.Exceptions.BusinessException"></exception>
        public string GenerateReport(Guid theoremListRequestId, int languageId, OutputFormat outputFormat)
        {
            try
            {
                var configuration = Container.Resolve<IConfiguration>();
                var reportExecution = new ReportExecutionServiceSoapClient();
                reportExecution.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;
                reportExecution.ClientCredentials.Windows.ClientCredential = new NetworkCredential(configuration.ReportingServiceUserName,
                                                                                                   configuration.ReportingServicePassword,
                                                                                                   configuration.ReportingServiceDomain);

                //Initialize variables
                TrustedUserHeader trustedUserHeader = null;
                var reportPath = configuration.ReportingServiceReportPath;
                string historyID = null;
                ServerInfoHeader serverInfoHeader = null;
                ExecutionInfo executionInfo = null;
                string deviceInfo = null;
                byte[] result = null;
                string extension = null;
                string mimeType = null;
                string encoding = null;
                Warning[] warnings = null;
                string[] streamIds = null;

                //Load the report information
                var executionHeader = reportExecution.LoadReport(
                    trustedUserHeader,
                    reportPath,
                    historyID,
                    out serverInfoHeader,
                    out executionInfo);

                //Set report parameters
                var parameters = new ParameterValue[2];
                parameters[0] = new ParameterValue { Name = "TheoremListRequestId", Value = theoremListRequestId.ToString() };
                parameters[1] = new ParameterValue { Name = "LanguageId", Value = languageId.ToString(CultureInfo.InvariantCulture) };

                reportExecution.SetExecutionParameters(executionHeader, trustedUserHeader, parameters, "en-us",
                                                       out executionInfo);

                //Render report
                reportExecution.Render(executionHeader, trustedUserHeader, GetOutputFormatString(outputFormat), deviceInfo,
                                                          out result, out extension, out mimeType, out encoding,
                                                          out warnings, out streamIds);

                return Convert.ToBase64String(result);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to generate report (format: {0}) for theoremlist request id {1} and language id {2}.",
                                                    outputFormat,
                                                    theoremListRequestId,
                                                    languageId);
                LogManager.LogError(message, exception);
                throw new BusinessException(message, exception);
            }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Gets the output format string.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        private static string GetOutputFormatString(OutputFormat f)
        {
            switch (f)
            {
                case OutputFormat.Xml: return "XML";
                case OutputFormat.Csv: return "CSV";
                case OutputFormat.Image: return "IMAGE";
                case OutputFormat.Pdf: return "PDF";
                case OutputFormat.MHtml: return "MHTML";
                case OutputFormat.Html4: return "HTML4.0";
                case OutputFormat.Html32: return "HTML3.2";
                case OutputFormat.Excel: return "EXCEL";
                case OutputFormat.Word: return "WORD";

                default:
                    return "PDF";
            }
        }
        #endregion

        #region Retrieve methods
        public EmailTemplate RetrieveEmailTemplate(int languageId, int theoremListRequestTypeId)
        {
            using (new DurationLog())
            {
                try
                {
                    return Filter<EmailTemplate>(e => e.LanguageId == languageId
                                                      && e.TheoremListRequestTypeId == theoremListRequestTypeId)
                                            .Single();
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to find e-mail template for language id {0} and theoremlist request type {1}.", languageId, theoremListRequestTypeId);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }

        public Setting RetrieveSettingByKey(string key)
        {
            using (new DurationLog())
            {
                try
                {
                    return Filter<Setting>(s => s.Key.ToLower() == key.ToLower()).Single();
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to retrieve setting with key {0}.", key);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }
        #endregion

        #region Other methods
        /// <summary>
        /// Updates the mail status of the request.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="isMailSent">if set to <c>true</c> [is mail sent].</param>
        /// <returns></returns>
        /// <exception cref="Quintessence.CulturalFit.Infra.Exceptions.BusinessException"></exception>
        public TheoremListRequest UpdateMailStatus(Guid requestId, bool isMailSent)
        {
            using (new DurationLog())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var request = context.TheoremListRequests.Single(tlr => tlr.Id == requestId);
                        request.IsMailSent = isMailSent;
                        context.SaveChanges();
                        return request;
                    }
                }
                catch (Exception exception)
                {
                    var message = string.Format("Unable to update the mail sent status of theoremlist request with id '{0}'.", requestId);
                    LogManager.LogError(message, exception);
                    throw new BusinessException(message, exception);
                }
            }
        }
        #endregion
    }
}
