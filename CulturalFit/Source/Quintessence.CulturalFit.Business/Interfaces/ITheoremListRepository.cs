using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Reports;

namespace Quintessence.CulturalFit.Business.Interfaces
{
    public interface ITheoremListRepository
    {
        #region Retrieve methods
        /// <summary>
        /// Retrieves the theorem list.
        /// </summary>
        /// <param name="theoremListId">The theorem list id.</param>
        /// <returns></returns>
        TheoremList RetrieveTheoremList(Guid theoremListId);

        /// <summary>
        /// Retrieves the theorem list request.
        /// </summary>
        /// <param name="theoremListRequestId">The theorem list request id.</param>
        /// <returns></returns>
        TheoremListRequest RetrieveTheoremListRequest(Guid theoremListRequestId);

        /// <summary>
        /// Retrieves the theorem list request.
        /// </summary>
        /// <param name="verificationCode">The verification code.</param>
        /// <returns></returns>
        TheoremListRequest RetrieveTheoremListRequest(string verificationCode);
        #endregion

        #region List methods

        /// <summary>
        /// Lists the languages.
        /// </summary>
        /// <returns></returns>
        List<Language> ListLanguages();
        #endregion

        #region Save & create methods
        /// <summary>
        /// Saves the specified theorem list request.
        /// </summary>
        /// <param name="theoremListRequest">The theorem list request.</param>
        void Save(TheoremListRequest theoremListRequest);

        /// <summary>
        /// Saves the specified theorem list.
        /// </summary>
        /// <param name="theoremList">The theorem list.</param>
        void Save(TheoremList theoremList);
        #endregion

        #region Other methods
        /// <summary>
        /// Validates the verification code.
        /// </summary>
        /// <param name="verificationCode">The verification code.</param>
        /// <returns></returns>
        TheoremListRequest ValidateVerificationCode(string verificationCode);

        /// <summary>
        /// Registers the theorem check.
        /// </summary>
        /// <param name="theoremId">The theorem id.</param>
        /// <param name="isLeastApplicable">if set to <c>true</c> [is least applicable].</param>
        /// <param name="isMostApplicable">if set to <c>true</c> [is most applicable].</param>
        void RegisterTheoremCheck(Guid theoremId, bool isLeastApplicable, bool isMostApplicable);

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="theoremListRequestId">The theorem list request id.</param>
        /// <param name="languageId">The language id.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        string GenerateReport(Guid theoremListRequestId, int languageId, OutputFormat outputFormat);
        #endregion

        #region Generic methods

        /// <summary>
        /// Filters the entities with the specified where clause.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IList<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> whereClause,
                                       Func<IDbSet<TEntity>, IEnumerable<TEntity>> filter = null)
            where TEntity : class;

        /// <summary>
        /// Adds the specified entity to the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        TEntity Add<TEntity>(TEntity entity)
            where TEntity : class;
        #endregion

        #region Retrieve methods
        /// <summary>
        /// Retrieves the setting by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Setting RetrieveSettingByKey(string key);
        #endregion

        #region Other methods
        /// <summary>
        /// Updates the mail status of the request.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="isMailSent">if set to <c>true</c> [is mail sent].</param>
        /// <returns></returns>
        TheoremListRequest UpdateMailStatus(Guid requestId, bool isMailSent);
        #endregion
    }
}