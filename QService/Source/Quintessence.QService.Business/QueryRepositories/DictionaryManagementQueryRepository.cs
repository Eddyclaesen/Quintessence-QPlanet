using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class DictionaryManagementQueryRepository : QueryRepositoryBase<IDimQueryContext>, IDictionaryManagementQueryRepository
    {
        public DictionaryManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<DictionaryView> SearchDictionaries(string name = null, int? contactId = new int?())
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.Dictionaries

                            .Include(d => d.Contact)

                            .Where(d => (name == null || d.Name.Contains(name)
                                && (contactId == null || d.ContactId == contactId)));

                        return dictionaries.ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryView> ListQuintessenceDictionaries(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.Dictionaries
                            .Where(d => d.ContactId == null)
                            .ToList();

                        if (pagingInfo == null)
                            return dictionaries;

                        var filtered = dictionaries
                            .Where(d => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || d.Name.ToLowerInvariant().Contains(pagingInfo.FilterTerm.ToLowerInvariant())).ToList();

                        pagingInfo.TotalRecords = dictionaries.Count;
                        pagingInfo.TotalDisplayRecords = filtered.Count;

                        return filtered.SkipTake(pagingInfo).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryView RetrieveDictionaryDetail(Guid id, List<int> languageIds = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        if (languageIds == null)
                        {
                            var dictionary = context.Dictionaries
                                .Include(d => d.Contact)
                                .Include(d => d.DictionaryClusters)
                                .Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences))
                                .Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryLevels)))
                                .Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryLevels.Select(dl => dl.DictionaryIndicators))))

                                .SingleOrDefault(d => d.Id == id);

                            return dictionary;

                        }
                        else
                        {
                            var dbQuery = (from d in context.Dictionaries.Include(d => d.Contact)
                                           where d.Id == id
                                           select new
                                               {
                                                   d,
                                                   dictionaryclusters = from dcl in d.DictionaryClusters
                                                                        select new
                                                                            {
                                                                                dcl,
                                                                                dictionaryClusterTranslations = from dclt in dcl.DictionaryClusterTranslations
                                                                                                                where languageIds.Contains(dclt.LanguageId)
                                                                                                                select dclt,
                                                                                dictionaryCompetences = from dco in dcl.DictionaryCompetences
                                                                                                        select new
                                                                                                            {
                                                                                                                dco,
                                                                                                                dictionaryCompetenceTranslations = from dcot in dco.DictionaryCompetenceTranslations
                                                                                                                                                   where languageIds.Contains(dcot.LanguageId)
                                                                                                                                                   select dcot,
                                                                                                                dictionaryLevels = from dl in dco.DictionaryLevels
                                                                                                                                   select new
                                                                                                                                   {
                                                                                                                                       dl,
                                                                                                                                       dictionaryLevelsTranslations = from dlt in dl.DictionaryLevelTranslations
                                                                                                                                                                      where languageIds.Contains(dlt.LanguageId)
                                                                                                                                                                      select dlt,
                                                                                                                                       dictionaryIndicators = from di in dl.DictionaryIndicators
                                                                                                                                                              select new
                                                                                                                                                              {
                                                                                                                                                                  di,
                                                                                                                                                                  dictionaryIndicatorTranslations = from dit in di.DictionaryIndicatorTranslations
                                                                                                                                                                                                    where languageIds.Contains(dit.LanguageId)
                                                                                                                                                                                                    select dit
                                                                                                                                                              }
                                                                                                                                   }
                                                                                                            }
                                                                            }
                                               });


                            //context.Dictionaries
                            //.Include(d => d.Contact)
                            //.Include(d => d.DictionaryClusters)
                            //.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryClusterTranslations.Select(dct => dct.Language)))
                            //.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryCompetenceTranslations.Select(dct => dct.Language))))
                            //.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryLevels.Select(dl => dl.DictionaryLevelTranslations.Select(dlt => dlt.Language)))))
                            //.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryLevels.Select(dl => dl.DictionaryIndicators.Select(di => di.DictionaryIndicatorTranslations.Select(dit => dit.Language))))))

                            //.SingleOrDefault(d => d.Id == id);

                            return dbQuery.AsEnumerable().Select(d => d.d).SingleOrDefault();

                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryView RetrieveDictionary(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionary = context.Dictionaries

                            .SingleOrDefault(d => d.Id == id);

                        return dictionary;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void SaveDictionary(DictionaryView dictionaryView)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {

                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryView> ListDictionariesByContactName(string contactName)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.Dictionaries

                            .Include(d => d.Contact)

                            .Where(d => d.Contact != null && (d.Contact.Name.Contains(contactName) || d.Contact.Department.Contains(contactName)))

                            .ToList();

                        return dictionaries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryView> ListDictionariesByContactId(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.Dictionaries

                            .Where(d => d.ContactId == id)

                            .ToList();

                        return dictionaries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<AvailableDictionaryView> ListAvailableDictionaries(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ListAvailableDictionariesForContact(contactId).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryView> ListDetailedDictionaries(int? contactId, bool tillClusters = false, bool tillCompetences = false, bool tillLevels = false, bool tillIndicators = false)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        IQueryable<DictionaryView> query = null;

                        if (tillIndicators)
                            query = context.Dictionaries.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryLevels.Select(dl => dl.DictionaryIndicators))));

                        else if (tillLevels)
                            query = context.Dictionaries.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences.Select(dco => dco.DictionaryLevels)));

                        else if (tillCompetences)
                            query = context.Dictionaries.Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences));

                        else if (tillClusters)
                            query = context.Dictionaries.Include(d => d.DictionaryClusters);
                        else
                            query = context.Dictionaries;

                        var dictionaries = query.Where(d => !d.ContactId.HasValue || d.ContactId == contactId).ToList();

                        return dictionaries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryView> ListDictionaries(PagingInfo pagingInfo)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.Dictionaries
                            .Include(d => d.Contact)
                            .ToList();

                        if (pagingInfo == null)
                            return dictionaries;

                        var filtered = dictionaries
                            .Where(d => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || d.Name.ToLowerInvariant().Contains(pagingInfo.FilterTerm.ToLowerInvariant())).ToList();

                        pagingInfo.TotalRecords = dictionaries.Count;
                        pagingInfo.TotalDisplayRecords = filtered.Count;

                        return filtered.SkipTake(pagingInfo).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryView> ListCustomerDictionaries(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.Dictionaries
                            .Include(d => d.Contact)
                            .Where(d => d.ContactId != null)
                            .ToList();

                        if (pagingInfo == null)
                            return dictionaries;

                        var filtered = dictionaries
                            .Where(d => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || d.Name.ToLowerInvariant().Contains(pagingInfo.FilterTerm.ToLowerInvariant())).ToList();

                        pagingInfo.TotalRecords = dictionaries.Count;
                        pagingInfo.TotalDisplayRecords = filtered.Count;

                        return filtered.SkipTake(pagingInfo).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryIndicatorMatrixEntryView> ListDictionaryIndicatorMatrixEntries(Guid dictionaryId, int? languageId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListDictionaryIndicatorMatrixEntries(dictionaryId, languageId).ToList();
                        //var entries = context.DictionaryIndicatorMatrixEntries
                        //    .Where(di => di.DictionaryId == dictionaryId)
                        //    .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryIndicatorView> ListIndicatorsByDictionaryLevel(Guid dictionaryLevelId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.DictionaryIndicators
                            .Where(di => di.DictionaryLevelId == dictionaryLevelId)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryLevelView> ListLevelsByDictionaryCompetence(Guid dictionaryComptenceId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.DictionaryLevels
                            .Where(di => di.DictionaryCompetenceId == dictionaryComptenceId)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryIndicatorView> ListIndicatorsByDictionaryCompetence(Guid dictionaryCompetenceId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.DictionaryIndicators
                            .Include(di => di.DictionaryLevel)
                            .Where(di => di.DictionaryLevel.DictionaryCompetenceId == dictionaryCompetenceId)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryIndicatorView> ListDictionaryIndicators(List<Guid> ids)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.DictionaryIndicators
                            .Where(di => ids.Contains(di.Id))
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryAdminView RetrieveDictionaryAdmin(Guid dictionaryId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionary = context.DictionaryAdmins
                            .Include(d => d.Contact)
                            .Include(d => d.DictionaryClusters)
                            .FirstOrDefault(d => d.Id == dictionaryId);

                        return dictionary;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryClusterAdminView RetrieveDictionaryClusterAdmin(Guid dictionaryClusterId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaryCluster = context.DictionaryClusterAdmins
                            .Include(d => d.DictionaryCompetences)
                            .Include(d => d.DictionaryClusterTranslations)
                            .FirstOrDefault(d => d.Id == dictionaryClusterId);

                        return dictionaryCluster;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryCompetenceAdminView RetrieveDictionaryCompetenceAdmin(Guid dictionaryCompetenceId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaryCluster = context.DictionaryCompetenceAdmins
                            .Include(d => d.DictionaryLevels)
                            .Include(d => d.DictionaryCompetenceTranslations)
                            .FirstOrDefault(d => d.Id == dictionaryCompetenceId);

                        return dictionaryCluster;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryLevelAdminView RetrieveDictionaryLevelAdmin(Guid dictionaryLevelId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaryCluster = context.DictionaryLevelAdmins
                            .Include(d => d.DictionaryIndicators.Select(di => di.DictionaryIndicatorTranslations))
                            .Include(d => d.DictionaryLevelTranslations)
                            .FirstOrDefault(d => d.Id == dictionaryLevelId);

                        return dictionaryCluster;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public DictionaryIndicatorAdminView RetrieveDictionaryIndicatorAdmin(Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaryCluster = context.DictionaryIndicatorAdmins
                            .Include(d => d.DictionaryIndicatorTranslations)
                            .FirstOrDefault(d => d.Id == dictionaryIndicatorId);

                        return dictionaryCluster;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryAdminView> ListQuintessenceAdminDictionaries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.DictionaryAdmins
                            .Where(d => d.ContactId == null)
                            .ToList();

                        return dictionaries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<DictionaryAdminView> ListCustomerAdminDictionaries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var dictionaries = context.DictionaryAdmins
                            .Include(d => d.Contact)
                            .Where(d => d.ContactId != null)
                            .ToList();

                        return dictionaries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
