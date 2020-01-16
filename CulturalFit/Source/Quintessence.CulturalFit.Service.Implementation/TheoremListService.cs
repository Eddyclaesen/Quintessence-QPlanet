using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Exception;
using Quintessence.CulturalFit.DataModel.Reports;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.Service.Implementation.Base;

namespace Quintessence.CulturalFit.Service.Implementation
{
    public class TheoremListService : UnityServiceBase, ITheoremListService
    {
        #region Retrieve methods
        /// <summary>
        /// Retrieves the theorem list request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public TheoremListRequest RetrieveTheoremListRequest(RetrieveTheoremListRequestRequest request)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();
                return !string.IsNullOrWhiteSpace(request.VerificationCode)
                    ? repository.RetrieveTheoremListRequest(request.VerificationCode)
                    : repository.RetrieveTheoremListRequest(request.Id);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to retrieve the theorem list request with code '{0}' or id '{1}'.", request.VerificationCode, request.Id);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }
        #endregion

        #region List methods
        /// <summary>
        /// Lists the languages.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public List<Language> ListLanguages()
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();
                return repository.ListLanguages();
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to retrieve languages.");
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }
        #endregion

        #region Save methods
        /// <summary>
        /// Saves the theorem list request.
        /// </summary>
        /// <param name="theoremListRequest">The theorem list request.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public TheoremListRequest SaveTheoremListRequest(TheoremListRequest theoremListRequest)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();

                repository.Save(theoremListRequest);

                return repository.RetrieveTheoremListRequest(theoremListRequest.Id);
            }
            catch (FaultException<TheoremListAlreadyCompletedFault> exception)
            {
                throw;
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to save the theorem list.");
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        /// <summary>
        /// Saves the theorem list.
        /// </summary>
        /// <param name="theoremList">The theorem list.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public TheoremList SaveTheoremList(TheoremList theoremList)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();

                var existingTheoremList = repository.RetrieveTheoremList(theoremList.Id);

                if (existingTheoremList != null && existingTheoremList.IsCompleted)
                    throw new FaultException<TheoremListAlreadyCompletedFault>(new TheoremListAlreadyCompletedFault());

                repository.Save(theoremList);
                return repository.RetrieveTheoremList(theoremList.Id);
            }
            catch (FaultException<TheoremListAlreadyCompletedFault> exception)
            {
                throw;
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to save the theorem list.");
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }
        #endregion

        #region Other methods
        /// <summary>
        /// Validates the verification code.
        /// </summary>
        /// <param name="verificationCode">The verification code.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public TheoremListRequest ValidateVerificationCode(string verificationCode)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();
                return repository.ValidateVerificationCode(verificationCode);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to validate the verification code '{0}' for theorem list request.", verificationCode);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        /// <summary>
        /// Registers the theorem checkmark.
        /// </summary>
        /// <param name="theoremId">The theorem id.</param>
        /// <param name="isLeastApplicable">if set to <c>true</c> [is least applicable].</param>
        /// <param name="isMostApplicable">if set to <c>true</c> [is most applicable].</param>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public void RegisterTheoremCheck(Guid theoremId, bool isLeastApplicable, bool isMostApplicable)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();
                repository.RegisterTheoremCheck(theoremId, isLeastApplicable, isMostApplicable);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to register theorem check for id '{0}'", theoremId);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="theoremListRequestId">The theorem list request id.</param>
        /// <param name="languageId">The language id.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public string GenerateReport(Guid theoremListRequestId, int languageId, OutputFormat outputFormat)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();
                return repository.GenerateReport(theoremListRequestId, languageId, outputFormat);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to generate report (format: {0}) for theoremlist request id {1} and language id {2}.",
                                                    outputFormat,
                                                    theoremListRequestId,
                                                    languageId);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }

        }
        #endregion

        #region Retrieve methods
        /// <summary>
        /// Retrieves the setting by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public Setting RetrieveSettingByKey(string key)
        {
            try
            {
                var repository = Container.Resolve<ITheoremListRepository>();

                return repository.RetrieveSettingByKey(key);
            }
            catch (InvalidOperationException exception)
            {
                var message = string.Format("Setting with key {0} not found or multiple settings found with that key.", key);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to find setting with key {0}.", key);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }
        #endregion
    }
}
