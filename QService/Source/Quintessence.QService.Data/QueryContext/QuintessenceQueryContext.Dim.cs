using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IDimQueryContext
    {
        public DbQuery<DictionaryView> Dictionaries { get { return Set<DictionaryView>().AsNoTracking(); } }
        public DbQuery<DictionaryIndicatorMatrixEntryView> DictionaryIndicatorMatrixEntries { get { return Set<DictionaryIndicatorMatrixEntryView>().AsNoTracking(); } }
        public DbQuery<DictionaryIndicatorView> DictionaryIndicators { get { return Set<DictionaryIndicatorView>().AsNoTracking(); } }
        public DbQuery<DictionaryLevelView> DictionaryLevels { get { return Set<DictionaryLevelView>().AsNoTracking(); } }
        public DbQuery<DictionaryAdminView> DictionaryAdmins { get { return Set<DictionaryAdminView>().AsNoTracking(); } }
        public DbQuery<DictionaryClusterAdminView> DictionaryClusterAdmins { get { return Set<DictionaryClusterAdminView>().AsNoTracking(); } }
        public DbQuery<DictionaryCompetenceAdminView> DictionaryCompetenceAdmins { get { return Set<DictionaryCompetenceAdminView>().AsNoTracking(); } }
        public DbQuery<DictionaryLevelAdminView> DictionaryLevelAdmins { get { return Set<DictionaryLevelAdminView>().AsNoTracking(); } }
        public DbQuery<DictionaryIndicatorAdminView> DictionaryIndicatorAdmins { get { return Set<DictionaryIndicatorAdminView>().AsNoTracking(); } }
        public DbQuery<DictionaryClusterTranslationView> DictionaryClusterTranslations { get { return Set<DictionaryClusterTranslationView>().AsNoTracking(); } }

        public IEnumerable<AvailableDictionaryView> ListAvailableDictionariesForContact(int contactId)
        {
            var query = Database.SqlQuery<AvailableDictionaryView>("Dictionary_ListAvailableDictionariesForContact {0}", contactId);
            return query;
        }

        public IEnumerable<AvailableBceView> ListAvailableBcesForContact(int contactId)
        {
            var query = Database.SqlQuery<AvailableBceView>("Bce_ListAvailableEntitiesForContact {0}", contactId);
            return query;
        }

        public IEnumerable<DictionaryIndicatorMatrixEntryView> ListDictionaryIndicatorMatrixEntries(Guid dictionaryId, int? languageId = null)
        {
            var query = Database.SqlQuery<DictionaryIndicatorMatrixEntryView>("Dictionary_ListDictionaryIndicatorMatrixEntries {0}, {1}", dictionaryId, languageId);
            return query;
        }
    }
}
