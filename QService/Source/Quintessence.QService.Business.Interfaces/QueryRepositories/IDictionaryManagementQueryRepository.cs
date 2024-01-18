using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IDictionaryManagementQueryRepository : IQueryRepository
    {
        List<DictionaryView> SearchDictionaries(string name = null, int? contactId = null);
        List<DictionaryView> ListQuintessenceDictionaries(PagingInfo pagingInfo = null);
        DictionaryView RetrieveDictionaryDetail(Guid id, List<int> languageIds = null);
        DictionaryView RetrieveDictionary(Guid id);
        List<DictionaryView> ListDictionariesByContactName(string contactName);
        List<DictionaryView> ListDictionariesByContactId(int id);
        List<AvailableDictionaryView> ListAvailableDictionaries(int contactId);
        List<AvailableBceView> ListAvailableBces(int contactId);
        List<DictionaryView> ListDictionaries(PagingInfo pagingInfo = null);
        List<DictionaryView> ListDetailedDictionaries(int? contactId, bool tillClusters = false, bool tillCompetences = false, bool tillLevels = false, bool tillIndicators = false);
        List<DictionaryView> ListCustomerDictionaries(PagingInfo pagingInfo = null);
        List<DictionaryIndicatorMatrixEntryView> ListDictionaryIndicatorMatrixEntries(Guid dictionaryId, int? languageId = null);
        List<DictionaryIndicatorView> ListIndicatorsByDictionaryLevel(Guid dictionaryLevelId);
        List<DictionaryLevelView> ListLevelsByDictionaryCompetence(Guid dictionaryComptenceId);
        List<DictionaryIndicatorView> ListIndicatorsByDictionaryCompetence(Guid dictionaryCompetenceId);
        List<DictionaryIndicatorView> ListDictionaryIndicators(List<Guid> ids);
        DictionaryAdminView RetrieveDictionaryAdmin(Guid dictionaryId);
        DictionaryClusterAdminView RetrieveDictionaryClusterAdmin(Guid dictionaryClusterId);
        DictionaryCompetenceAdminView RetrieveDictionaryCompetenceAdmin(Guid dictionaryCompetenceId);
        DictionaryLevelAdminView RetrieveDictionaryLevelAdmin(Guid dictionaryLevelId);
        DictionaryIndicatorAdminView RetrieveDictionaryIndicatorAdmin(Guid dictionaryIndicatorId);
        List<DictionaryAdminView> ListQuintessenceAdminDictionaries();
        List<DictionaryAdminView> ListCustomerAdminDictionaries();
    }
}
