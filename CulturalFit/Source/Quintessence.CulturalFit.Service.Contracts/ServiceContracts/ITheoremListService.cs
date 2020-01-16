using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Exception;
using Quintessence.CulturalFit.DataModel.Reports;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;

namespace Quintessence.CulturalFit.Service.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface ITheoremListService
    {
        #region Retrieve methods
        /// <summary>
        /// Retrieves the theorem list request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [OperationContract]
        TheoremListRequest RetrieveTheoremListRequest(RetrieveTheoremListRequestRequest request);
        #endregion

        #region List methods
        /// <summary>
        /// Lists the languages.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Language> ListLanguages();
        #endregion

        #region Save methods
        /// <summary>
        /// Saves the theorem list request.
        /// </summary>
        /// <param name="theoremListRequest">The theorem list request.</param>
        /// <returns></returns>
        [OperationContract]
        TheoremListRequest SaveTheoremListRequest(TheoremListRequest theoremListRequest);

        /// <summary>
        /// Saves the theorem list.
        /// </summary>
        /// <param name="theoremList">The theorem list.</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(TheoremListAlreadyCompletedFault))]
        TheoremList SaveTheoremList(TheoremList theoremList);
        #endregion

        #region Other methods
        /// <summary>
        /// Validates the verification code.
        /// </summary>
        /// <param name="verificationCode">The verification code.</param>
        /// <returns></returns>
        [OperationContract]
        TheoremListRequest ValidateVerificationCode(string verificationCode);

        /// <summary>
        /// Registers the theorem checkmark.
        /// </summary>
        /// <param name="theoremId">The theorem id.</param>
        /// <param name="isLeastApplicable">if set to <c>true</c> [is least applicable].</param>
        /// <param name="isMostApplicable">if set to <c>true</c> [is most applicable].</param>
        [OperationContract]
        void RegisterTheoremCheck(Guid theoremId, bool isLeastApplicable, bool isMostApplicable);

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="theoremListRequestId">The theorem list request id.</param>
        /// <param name="languageId">The language id.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        [OperationContract]
        string GenerateReport(Guid theoremListRequestId, int languageId, OutputFormat outputFormat);
        #endregion

        #region Retrieve methods
        /// <summary>
        /// Retrieves the setting by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [OperationContract]
        Setting RetrieveSettingByKey(string key);
        #endregion
    }
}