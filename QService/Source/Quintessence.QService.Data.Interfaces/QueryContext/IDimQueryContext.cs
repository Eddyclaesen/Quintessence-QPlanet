using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface IDimQueryContext : IQuintessenceQueryContext
    {
        DbQuery<DictionaryView> Dictionaries { get; }
        DbQuery<DictionaryIndicatorMatrixEntryView> DictionaryIndicatorMatrixEntries { get; }
        DbQuery<DictionaryIndicatorView> DictionaryIndicators { get; }
        DbQuery<DictionaryLevelView> DictionaryLevels { get; }
        DbQuery<DictionaryAdminView> DictionaryAdmins { get; }
        DbQuery<DictionaryClusterAdminView> DictionaryClusterAdmins { get; }
        DbQuery<DictionaryCompetenceAdminView> DictionaryCompetenceAdmins { get; }
        DbQuery<DictionaryLevelAdminView> DictionaryLevelAdmins { get; }
        DbQuery<DictionaryIndicatorAdminView> DictionaryIndicatorAdmins { get; }
        DbQuery<DictionaryClusterTranslationView> DictionaryClusterTranslations { get; }
        IEnumerable<AvailableDictionaryView> ListAvailableDictionariesForContact(int contactId);
        IEnumerable<DictionaryIndicatorMatrixEntryView> ListDictionaryIndicatorMatrixEntries(Guid dictionaryId, int? languageId = null);
    }
}
