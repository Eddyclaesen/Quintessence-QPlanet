using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.SecurityContracts;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class DictionaryManagementQueryService : SecuredUnityServiceBase, IDictionaryManagementQueryService
    {
        public SearchDictionaryResponse SearchDictionary(SearchDictionaryRequest request)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    var dictionaries = new List<DictionaryView>();

                    if (!string.IsNullOrWhiteSpace(request.ContactName))
                    {
                        dictionaries.AddRange(repository.ListDictionariesByContactName(request.ContactName));
                    }

                    var response = new SearchDictionaryResponse();
                    response.Dictionaries = dictionaries.Distinct().ToList();

                    return response;
                }
            });
        }

        public List<DictionaryView> ListQuintessenceDictionaries()
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    var dictionaries = repository.ListQuintessenceDictionaries();

                    return dictionaries;
                }
            });
        }

        public List<DictionaryView> ListDetailedDictionaries(ListDetailedDictionariesRequest request)
        {
            return Execute(() =>
                {
                    using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                    using (var prmQueryRepository = Container.Resolve<IProjectManagementQueryRepository>())
                    {
                        var projectRole = prmQueryRepository.Retrieve<ProjectRoleView>(request.ProjectRoleId);

                        return repository.ListDetailedDictionaries(projectRole.ContactId, request.TillClusters, request.TillCompetences, request.TillLevels, request.TillIndicators);
                    }
                });
        }

        public List<DictionaryView> ListCustomerDictionaries()
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    var dictionaries = repository.ListCustomerDictionaries();

                    return dictionaries;
                }
            });
        }

        public DictionaryView RetrieveDictionaryDetail(RetrieveDictionaryDetailRequest request)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    var dictionary = repository.RetrieveDictionaryDetail(request.Id, request.LanguageIds);

                    return dictionary;
                }
            });
        }

        public DictionaryView RetrieveDictionary(Guid id)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.RetrieveDictionary(id);
                }
            });
        }

        public List<DictionaryView> ListDictionariesByContactId(int id)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListDictionariesByContactId(id);
                }
            });
        }

        public List<AvailableDictionaryView> ListAvailableDictionaries(int contactId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListAvailableDictionaries(contactId);
                }
            });
        }

        public List<AvailableBceView> ListAvailableBces(int contactId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListAvailableBces(contactId);
                }
            });
        }

        public ListDictionariesResponse ListDictionaries(ListDictionariesRequest request)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                    var response = new ListDictionariesResponse
                        {
                            Dictionaries = repository.ListDictionaries(pagingInfo),
                            DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                        };

                    return response;
                }
            });
        }

        public List<DictionaryIndicatorMatrixEntryView> ListDictionaryIndicatorMatrixEntries(ListDictionaryIndicatorMatrixEntriesRequest request)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListDictionaryIndicatorMatrixEntries(request.DictionaryId, request.LanguageId);
                }
            });
        }

        public List<DictionaryIndicatorView> ListIndicatorsByDictionaryLevel(Guid dictionaryLevelId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListIndicatorsByDictionaryLevel(dictionaryLevelId);
                }
            });
        }

        public List<DictionaryIndicatorView> ListIndicatorsByDictionaryCompetence(Guid dictionaryCompetenceId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListIndicatorsByDictionaryCompetence(dictionaryCompetenceId);
                }
            });
        }

        public DictionaryLevelView RetrieveDictionaryLevel(Guid id)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.Retrieve<DictionaryLevelView>(id);
                }
            });
        }

        public List<DictionaryIndicatorView> ListDictionaryIndicators(List<Guid> ids)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListDictionaryIndicators(ids);
                }
            });
        }

        public List<DictionaryImportFileInfoView> ListImportDictionaries()
        {
            return Execute(() =>
                {
                    var dictionaryImportFolder = ConfigurationManager.AppSettings["DictionaryImportFolder"];

                    if (string.IsNullOrWhiteSpace(dictionaryImportFolder))
                    {
                        ValidationContainer.RegisterEntityValidationFaultEntry("Dictionary Import Folder is not specified in the configuration file.");
                        return null;
                    }

                    var directoryInfo = new DirectoryInfo(dictionaryImportFolder);

                    if (!directoryInfo.Exists)
                    {
                        ValidationContainer.RegisterEntityValidationFaultEntry("Specified Dictionary Import Folder does not exist.");
                        return null;
                    }

                    var files = directoryInfo.GetFiles("*.xls*");

                    var dictionaries = files.Select(Mapper.DynamicMap<DictionaryImportFileInfoView>).ToList();

                    return dictionaries;
                });
        }

        public DictionaryImportView AnalyseDictionary(string name)
        {
            return Execute(() =>
            {
                var dictionaryImportFolder = ConfigurationManager.AppSettings["DictionaryImportFolder"];

                if (string.IsNullOrWhiteSpace(dictionaryImportFolder))
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("Dictionary Import Folder is not specified in the configuration file.");
                    return null;
                }

                var directoryInfo = new DirectoryInfo(dictionaryImportFolder);

                if (!directoryInfo.Exists)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("Specified Dictionary Import Folder does not exist.");
                    return null;
                }

                var file = directoryInfo.GetFiles(name).SingleOrDefault();

                if (file == null)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("Specified filename was not found.");
                    return null;
                }

                var repository = Container.Resolve<IDictionaryImportQueryRepository>();

                var dictionary = repository.ProcessDictionaryFile(file.FullName);

                return dictionary;
            });
        }

        public DictionaryAdminView RetrieveDictionaryAdmin(Guid dictionaryId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.RetrieveDictionaryAdmin(dictionaryId);
                }
            });
        }

        public DictionaryClusterAdminView RetrieveDictionaryClusterAdmin(Guid dictionaryClusterId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.RetrieveDictionaryClusterAdmin(dictionaryClusterId);
                }
            });
        }

        public DictionaryCompetenceAdminView RetrieveDictionaryCompetenceAdmin(Guid dictionaryCompetenceId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.RetrieveDictionaryCompetenceAdmin(dictionaryCompetenceId);
                }
            });
        }

        public DictionaryLevelAdminView RetrieveDictionaryLevelAdmin(Guid dictionaryLevelId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.RetrieveDictionaryLevelAdmin(dictionaryLevelId);
                }
            });
        }

        public DictionaryIndicatorAdminView RetrieveDictionaryIndicatorAdmin(Guid dictionaryIndicatorId)
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.RetrieveDictionaryIndicatorAdmin(dictionaryIndicatorId);
                }
            });
        }

        public List<DictionaryAdminView> ListQuintessenceAdminDictionaries()
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListQuintessenceAdminDictionaries();
                }
            });
        }

        public List<DictionaryAdminView> ListCustomerAdminDictionaries()
        {
            return Execute(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementQueryRepository>())
                {
                    return repository.ListCustomerAdminDictionaries();
                }
            });
        }
    }
}
