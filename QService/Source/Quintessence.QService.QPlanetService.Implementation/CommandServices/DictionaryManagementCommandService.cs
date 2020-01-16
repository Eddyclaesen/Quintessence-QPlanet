using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.DataModel.Dim;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class DictionaryManagementCommandService : SecuredUnityServiceBase, IDictionaryManagementCommandService
    {
        public Guid SaveDictionary(Dictionary dictionary)
        {
            LogTrace("Save dictionary {0} (id: {1}).", dictionary.Name, dictionary.Id);

            return ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    repository.SaveDictionary(dictionary);
                    return dictionary.Id;
                }
            });
        }

        public void ValidateDictionary(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var queryService = Container.Resolve<IDictionaryManagementQueryService>();
                    var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                    var languages = infrastructureQueryService.ListLanguages();

                    var dictionary =
                        queryService.RetrieveDictionaryDetail(new RetrieveDictionaryDetailRequest
                            {
                                Id = id,
                                LanguageIds = languages.Select(l => l.Id).ToList()
                            });

                    if (dictionary.DictionaryClusters != null)
                    {
                        foreach (var clusterTranslation in dictionary.DictionaryClusters
                                                                     .SelectMany(
                                                                         dc =>
                                                                         dc.DictionaryClusterTranslations ??
                                                                         new List<DictionaryClusterTranslationView>())
                                                                     .GroupBy(dct => dct.DictionaryClusterId)
                                                                     .ToDictionary(dctg => dctg.Key,
                                                                                   dctg =>
                                                                                   dctg.Select(dct => dct.LanguageId)
                                                                                       .ToList()))
                        {
                            if (clusterTranslation.Value.Count == languages.Count)
                                continue;

                            foreach (var language in languages)
                            {
                                if (!clusterTranslation.Value.Contains(language.Id))
                                {
                                    var ct = repository.Prepare<DictionaryClusterTranslation>();
                                    ct.DictionaryClusterId = clusterTranslation.Key;
                                    ct.LanguageId = language.Id;
                                    ct.Text = ct.Description = string.Empty;
                                    repository.Save(ct);
                                }
                            }
                        }

                        var dictionaryCompetences =
                            dictionary.DictionaryClusters.SelectMany(
                                dc => dc.DictionaryCompetences ?? new List<DictionaryCompetenceView>()).ToList();
                        foreach (var competenceTranslation in dictionaryCompetences
                            .SelectMany(
                                dc =>
                                dc.DictionaryCompetenceTranslations ?? new List<DictionaryCompetenceTranslationView>())
                            .GroupBy(dct => dct.DictionaryCompetenceId)
                            .ToDictionary(dctg => dctg.Key, dctg => dctg.Select(dct => dct.LanguageId).ToList()))
                        {
                            if (competenceTranslation.Value.Count == languages.Count)
                                continue;

                            foreach (var language in languages)
                            {
                                if (!competenceTranslation.Value.Contains(language.Id))
                                {
                                    var ct = repository.Prepare<DictionaryCompetenceTranslation>();
                                    ct.DictionaryCompetenceId = competenceTranslation.Key;
                                    ct.LanguageId = language.Id;
                                    ct.Text = ct.Description = string.Empty;
                                    repository.Save(ct);
                                }
                            }
                        }

                        var dictionaryLevels =
                            dictionaryCompetences.SelectMany(
                                dc => dc.DictionaryLevels ?? new List<DictionaryLevelView>()).ToList();
                        foreach (var levelTranslation in dictionaryLevels
                            .SelectMany(
                                dl => dl.DictionaryLevelTranslations ?? new List<DictionaryLevelTranslationView>())
                            .GroupBy(dl => dl.DictionaryLevelId)
                            .ToDictionary(dltg => dltg.Key, dltg => dltg.Select(dlt => dlt.LanguageId).ToList()))
                        {
                            if (levelTranslation.Value.Count == languages.Count)
                                continue;

                            foreach (var language in languages)
                            {
                                if (!levelTranslation.Value.Contains(language.Id))
                                {
                                    var lt = repository.Prepare<DictionaryLevelTranslation>();
                                    lt.DictionaryLevelId = levelTranslation.Key;
                                    lt.LanguageId = language.Id;
                                    lt.Text = string.Empty;
                                    repository.Save(lt);
                                }
                            }
                        }

                        foreach (
                            var indicatorTranslation in
                                dictionaryLevels.SelectMany(
                                    dl => dl.DictionaryIndicators ?? new List<DictionaryIndicatorView>())
                                                .SelectMany(
                                                    di =>
                                                    di.DictionaryIndicatorTranslations ??
                                                    new List<DictionaryIndicatorTranslationView>())
                                                .GroupBy(dit => dit.DictionaryIndicatorId)
                                                .ToDictionary(ditg => ditg.Key,
                                                              ditg => ditg.Select(dit => dit.LanguageId).ToList()))
                        {
                            if (indicatorTranslation.Value.Count == languages.Count)
                                continue;

                            foreach (var language in languages)
                            {
                                if (!indicatorTranslation.Value.Contains(language.Id))
                                {
                                    var it = repository.Prepare<DictionaryIndicatorTranslation>();
                                    it.DictionaryIndicatorId = indicatorTranslation.Key;
                                    it.LanguageId = language.Id;
                                    it.Text = string.Empty;
                                    repository.Save(it);
                                }
                            }
                        }
                    }
                }
            });
        }

        public void UpdateDictionary(UpdateDictionaryRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
                {
                    using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                    {
                        var dictionary = repository.Retrieve<Dictionary>(request.Id);
                        Mapper.DynamicMap(request, dictionary);

                        repository.Save(dictionary);
                    }
                });
        }

        public void UpdateDictionaryCluster(UpdateDictionaryClusterRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryCluster = repository.Retrieve<DictionaryCluster>(request.Id);
                    Mapper.DynamicMap(request, dictionaryCluster);

                    var translations = new List<DictionaryClusterTranslation>();
                    foreach (var translationRequest in request.DictionaryClusterTranslations)
                    {
                        var dictionaryClusterTranslation = repository.Retrieve<DictionaryClusterTranslation>(translationRequest.Id);
                        Mapper.DynamicMap(translationRequest, dictionaryClusterTranslation);
                        translations.Add(dictionaryClusterTranslation);
                    }

                    repository.SaveList(translations);
                    repository.Save(dictionaryCluster);
                }
            });
        }

        public void UpdateDictionaryCompetence(UpdateDictionaryCompetenceRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryCompetence = repository.Retrieve<DictionaryCompetence>(request.Id);
                    Mapper.DynamicMap(request, dictionaryCompetence);

                    var translations = new List<DictionaryCompetenceTranslation>();
                    foreach (var translationRequest in request.DictionaryCompetenceTranslations)
                    {
                        var dictionaryCompetenceTranslation = repository.Retrieve<DictionaryCompetenceTranslation>(translationRequest.Id);
                        Mapper.DynamicMap(translationRequest, dictionaryCompetenceTranslation);
                        translations.Add(dictionaryCompetenceTranslation);
                    }

                    repository.SaveList(translations);
                    repository.Save(dictionaryCompetence);
                }
            });
        }

        public void UpdateDictionaryLevel(UpdateDictionaryLevelRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryLevel = repository.Retrieve<DictionaryLevel>(request.Id);
                    Mapper.DynamicMap(request, dictionaryLevel);

                    var translations = new List<DictionaryLevelTranslation>();
                    foreach (var translationRequest in request.DictionaryLevelTranslations)
                    {
                        var dictionaryLevelTranslation = repository.Retrieve<DictionaryLevelTranslation>(translationRequest.Id);
                        Mapper.DynamicMap(translationRequest, dictionaryLevelTranslation);
                        translations.Add(dictionaryLevelTranslation);
                    }

                    repository.SaveList(translations);
                    repository.Save(dictionaryLevel);
                }
            });
        }

        public void UpdateDictionaryIndicator(UpdateDictionaryIndicatorRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryIndicator = repository.Retrieve<DictionaryIndicator>(request.Id);
                    Mapper.DynamicMap(request, dictionaryIndicator);

                    var translations = new List<DictionaryIndicatorTranslation>();
                    foreach (var translationRequest in request.DictionaryIndicatorTranslations)
                    {
                        var dictionaryIndicatorTranslation = repository.Retrieve<DictionaryIndicatorTranslation>(translationRequest.Id);
                        Mapper.DynamicMap(translationRequest, dictionaryIndicatorTranslation);
                        translations.Add(dictionaryIndicatorTranslation);
                    }

                    repository.SaveList(translations);
                    repository.Save(dictionaryIndicator);
                }
            });
        }

        public Guid CreateNewDictionaryCluster(CreateNewDictionaryClusterRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryCluster = repository.Prepare<DictionaryCluster>();
                    Mapper.DynamicMap(request, dictionaryCluster);

                    if (ValidationContainer.ValidateObject(dictionaryCluster))
                        repository.Save(dictionaryCluster);
                    else
                        return Guid.Empty;

                    var translations = new List<DictionaryClusterTranslation>();
                    var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                    foreach (var language in infrastructureQueryService.ListLanguages())
                    {
                        var translation = repository.Prepare<DictionaryClusterTranslation>();
                        translation.DictionaryClusterId = dictionaryCluster.Id;
                        translation.LanguageId = language.Id;

                        if (language.Id == request.LanguageId)
                        {
                            translation.Text = dictionaryCluster.Name;
                            translation.Description = dictionaryCluster.Description;
                        }
                        translations.Add(translation);
                    }
                    repository.SaveList(translations);

                    return dictionaryCluster.Id;
                }
            });
        }

        public Guid CreateNewDictionaryCompetence(CreateNewDictionaryCompetenceRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryCompetence = repository.Prepare<DictionaryCompetence>();
                    Mapper.DynamicMap(request, dictionaryCompetence);

                    if (ValidationContainer.ValidateObject(dictionaryCompetence))
                        repository.Save(dictionaryCompetence);
                    else
                        return Guid.Empty;

                    var translations = new List<DictionaryCompetenceTranslation>();
                    var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                    foreach (var language in infrastructureQueryService.ListLanguages())
                    {
                        var translation = repository.Prepare<DictionaryCompetenceTranslation>();
                        translation.DictionaryCompetenceId = dictionaryCompetence.Id;
                        translation.LanguageId = language.Id;

                        if (language.Id == request.LanguageId)
                        {
                            translation.Text = dictionaryCompetence.Name;
                            translation.Description = dictionaryCompetence.Description;
                        }
                        translations.Add(translation);
                    }
                    repository.SaveList(translations);

                    return dictionaryCompetence.Id;
                }
            });
        }

        public Guid CreateNewDictionaryLevel(CreateNewDictionaryLevelRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var competenceIds = new List<Guid> { request.DictionaryCompetenceId };
                    if (request.ApplyToAllCompetences)
                    {
                        var queryService = Container.Resolve<IDictionaryManagementQueryService>();

                        var dictionaryCompetence = queryService.RetrieveDictionaryCompetenceAdmin(request.DictionaryCompetenceId);
                        var dictionaryCluster = queryService.RetrieveDictionaryClusterAdmin(dictionaryCompetence.DictionaryClusterId);

                        competenceIds.AddRange(dictionaryCluster.DictionaryCompetences.Select(dc => dc.Id).Except(new[] { request.DictionaryCompetenceId }));
                    }

                    Guid? dictionaryLevelId = null;

                    foreach (var competenceId in competenceIds)
                    {
                        var dictionaryLevel = repository.Prepare<DictionaryLevel>();
                        Mapper.DynamicMap(request, dictionaryLevel);

                        dictionaryLevel.DictionaryCompetenceId = competenceId;

                        if (ValidationContainer.ValidateObject(dictionaryLevel))
                            repository.Save(dictionaryLevel);
                        else
                            return Guid.Empty;

                        if (!dictionaryLevelId.HasValue)
                            dictionaryLevelId = dictionaryLevel.Id;

                        var translations = new List<DictionaryLevelTranslation>();
                        var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                        foreach (var language in infrastructureQueryService.ListLanguages())
                        {
                            var translation = repository.Prepare<DictionaryLevelTranslation>();
                            translation.DictionaryLevelId = dictionaryLevel.Id;
                            translation.LanguageId = language.Id;

                            if (language.Id == request.LanguageId)
                            {
                                translation.Text = dictionaryLevel.Name;
                            }
                            translations.Add(translation);
                        }
                        repository.SaveList(translations);
                    }

                    return dictionaryLevelId ?? Guid.Empty;
                }
            });
        }

        public Guid CreateNewDictionaryIndicator(CreateNewDictionaryIndicatorRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryIndicator = repository.Prepare<DictionaryIndicator>();
                    Mapper.DynamicMap(request, dictionaryIndicator);

                    if (ValidationContainer.ValidateObject(dictionaryIndicator))
                        repository.Save(dictionaryIndicator);
                    else
                        return Guid.Empty;

                    var translations = new List<DictionaryIndicatorTranslation>();
                    var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                    foreach (var language in infrastructureQueryService.ListLanguages())
                    {
                        var translation = repository.Prepare<DictionaryIndicatorTranslation>();
                        translation.DictionaryIndicatorId = dictionaryIndicator.Id;
                        translation.LanguageId = language.Id;

                        if (language.Id == request.LanguageId)
                        {
                            translation.Text = dictionaryIndicator.Name;
                        }
                        translations.Add(translation);
                    }
                    repository.SaveList(translations);

                    return dictionaryIndicator.Id;
                }
            });
        }

        public void DeleteDictionaryCluster(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryClusterTranslations = repository.List<DictionaryClusterTranslation>(dcts => dcts.Where(dct => dct.DictionaryClusterId == id));

                    var dictionaryCompetences = repository.List<DictionaryCompetence>(dcs => dcs.Where(dc => dc.DictionaryClusterId == id));
                    var competenceIds = dictionaryCompetences.Select(dc => dc.Id).ToList();

                    var dictionaryCompetenceTranslations = repository.List<DictionaryCompetenceTranslation>(dcts => dcts.Where(dct => competenceIds.Contains(dct.DictionaryCompetenceId)));

                    var dictionaryLevels = repository.List<DictionaryLevel>(dls => dls.Where(dl => competenceIds.Contains(dl.DictionaryCompetenceId)));
                    var levelIds = dictionaryLevels.Select(dl => dl.Id).ToList();

                    var dictionaryLevelTranslations = repository.List<DictionaryLevelTranslation>(dlts => dlts.Where(dlt => levelIds.Contains(dlt.DictionaryLevelId)));

                    var dictionaryIndicators = repository.List<DictionaryIndicator>(dis => dis.Where(di => levelIds.Contains(di.DictionaryLevelId)));
                    var indicatorIds = dictionaryIndicators.Select(dl => dl.Id).ToList();

                    var dictionaryIndicatorTranslations = repository.List<DictionaryIndicatorTranslation>(dits => dits.Where(dit => indicatorIds.Contains(dit.DictionaryIndicatorId)));

                    repository.DeleteList(dictionaryIndicatorTranslations);
                    repository.DeleteList(dictionaryIndicators);
                    repository.DeleteList(dictionaryLevelTranslations);
                    repository.DeleteList(dictionaryLevels);
                    repository.DeleteList(dictionaryCompetenceTranslations);
                    repository.DeleteList(dictionaryCompetences);
                    repository.DeleteList(dictionaryClusterTranslations);
                    repository.Delete<DictionaryCluster>(id);
                }
            });
        }

        public void DeleteDictionaryCompetence(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryCompetenceTranslations = repository.List<DictionaryCompetenceTranslation>(dcts => dcts.Where(dct => dct.DictionaryCompetenceId == id));

                    var dictionaryLevels = repository.List<DictionaryLevel>(dls => dls.Where(dl => dl.DictionaryCompetenceId == id));
                    var levelIds = dictionaryLevels.Select(dl => dl.Id).ToList();

                    var dictionaryLevelTranslations = repository.List<DictionaryLevelTranslation>(dlts => dlts.Where(dlt => levelIds.Contains(dlt.DictionaryLevelId)));

                    var dictionaryIndicators = repository.List<DictionaryIndicator>(dis => dis.Where(di => levelIds.Contains(di.DictionaryLevelId)));
                    var indicatorIds = dictionaryIndicators.Select(dl => dl.Id).ToList();

                    var dictionaryIndicatorTranslations = repository.List<DictionaryIndicatorTranslation>(dits => dits.Where(dit => indicatorIds.Contains(dit.DictionaryIndicatorId)));

                    repository.DeleteList(dictionaryIndicatorTranslations);
                    repository.DeleteList(dictionaryIndicators);
                    repository.DeleteList(dictionaryLevelTranslations);
                    repository.DeleteList(dictionaryLevels);
                    repository.DeleteList(dictionaryCompetenceTranslations);
                    repository.Delete<DictionaryCompetence>(id);
                }
            });
        }

        public void DeleteDictionaryLevel(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryLevelTranslations = repository.List<DictionaryLevelTranslation>(dlts => dlts.Where(dlt => dlt.DictionaryLevelId == id));

                    var dictionaryIndicators = repository.List<DictionaryIndicator>(dis => dis.Where(di => di.DictionaryLevelId == id));
                    var indicatorIds = dictionaryIndicators.Select(dl => dl.Id).ToList();

                    var dictionaryIndicatorTranslations = repository.List<DictionaryIndicatorTranslation>(dits => dits.Where(dit => indicatorIds.Contains(dit.DictionaryIndicatorId)));

                    repository.DeleteList(dictionaryIndicatorTranslations);
                    repository.DeleteList(dictionaryIndicators);
                    repository.DeleteList(dictionaryLevelTranslations);
                    repository.Delete<DictionaryLevel>(id);
                }
            });
        }

        public void DeleteDictionaryIndicator(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryIndicatorTranslations = repository.List<DictionaryIndicatorTranslation>(dits => dits.Where(dit => dit.DictionaryIndicatorId == id));

                    repository.DeleteList(dictionaryIndicatorTranslations);
                    repository.Delete<DictionaryIndicator>(id);
                }
            });
        }

        public Guid ImportDictionary(ImportDictionaryRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var dictionaryClusters = new List<DictionaryCluster>();
                    var dictionaryClusterTranslations = new List<DictionaryClusterTranslation>();
                    var dictionaryCompetences = new List<DictionaryCompetence>();
                    var dictionaryCompetenceTranslations = new List<DictionaryCompetenceTranslation>();
                    var dictionaryLevels = new List<DictionaryLevel>();
                    var dictionaryLevelTranslations = new List<DictionaryLevelTranslation>();
                    var dictionaryIndicators = new List<DictionaryIndicator>();
                    var dictionaryIndicatorTranslations = new List<DictionaryIndicatorTranslation>();

                    var dictionary = repository.Prepare<Dictionary>();
                    Mapper.DynamicMap(request, dictionary);

                    //Quintessence (Id: 3) dictionaries don't have a contact id.
                    if (dictionary.ContactId == 3)
                        dictionary.ContactId = null;

                    foreach (var importDictionaryClusterRequest in request.DictionaryClusters)
                    {
                        var dictionaryCluster = repository.Prepare<DictionaryCluster>();

                        Mapper.DynamicMap(importDictionaryClusterRequest, dictionaryCluster);
                        dictionaryCluster.DictionaryId = dictionary.Id;

                        dictionaryClusters.Add(dictionaryCluster);

                        foreach (var importDictionaryClusterTranslationRequest in importDictionaryClusterRequest.DictionaryClusterTranslations)
                        {
                            var dictionaryClusterTranslation = repository.Prepare<DictionaryClusterTranslation>();

                            Mapper.DynamicMap(importDictionaryClusterTranslationRequest, dictionaryClusterTranslation);
                            dictionaryClusterTranslation.DictionaryClusterId = dictionaryCluster.Id;

                            dictionaryClusterTranslations.Add(dictionaryClusterTranslation);
                        }

                        foreach (var importDictionaryCompetenceRequest in importDictionaryClusterRequest.DictionaryCompetences)
                        {
                            var dictionaryCompetence = repository.Prepare<DictionaryCompetence>();

                            Mapper.DynamicMap(importDictionaryCompetenceRequest, dictionaryCompetence);
                            dictionaryCompetence.DictionaryClusterId = dictionaryCluster.Id;

                            dictionaryCompetences.Add(dictionaryCompetence);

                            foreach (var importDictionaryCompetenceTranslationRequest in importDictionaryCompetenceRequest.DictionaryCompetenceTranslations)
                            {
                                var dictionaryCompetenceTranslation = repository.Prepare<DictionaryCompetenceTranslation>();

                                Mapper.DynamicMap(importDictionaryCompetenceTranslationRequest, dictionaryCompetenceTranslation);
                                dictionaryCompetenceTranslation.DictionaryCompetenceId = dictionaryCompetence.Id;

                                dictionaryCompetenceTranslations.Add(dictionaryCompetenceTranslation);
                            }

                            foreach (var importDictionaryLevelRequest in importDictionaryCompetenceRequest.DictionaryLevels)
                            {
                                var dictionaryLevel = repository.Prepare<DictionaryLevel>();

                                Mapper.DynamicMap(importDictionaryLevelRequest, dictionaryLevel);
                                dictionaryLevel.DictionaryCompetenceId = dictionaryCompetence.Id;

                                dictionaryLevels.Add(dictionaryLevel);

                                foreach (var importDictionaryLevelTranslationRequest in importDictionaryLevelRequest.DictionaryLevelTranslations)
                                {
                                    var dictionaryLevelTranslation = repository.Prepare<DictionaryLevelTranslation>();

                                    Mapper.DynamicMap(importDictionaryLevelTranslationRequest, dictionaryLevelTranslation);
                                    dictionaryLevelTranslation.DictionaryLevelId = dictionaryLevel.Id;

                                    dictionaryLevelTranslations.Add(dictionaryLevelTranslation);
                                }

                                foreach (var importDictionaryIndicatorRequest in importDictionaryLevelRequest.DictionaryIndicators)
                                {
                                    var dictionaryIndicator = repository.Prepare<DictionaryIndicator>();

                                    Mapper.DynamicMap(importDictionaryIndicatorRequest, dictionaryIndicator);
                                    dictionaryIndicator.DictionaryLevelId = dictionaryLevel.Id;

                                    dictionaryIndicators.Add(dictionaryIndicator);

                                    foreach (var importDictionaryIndicatorTranslationRequest in importDictionaryIndicatorRequest.DictionaryIndicatorTranslations)
                                    {
                                        var dictionaryIndicatorTranslation = repository.Prepare<DictionaryIndicatorTranslation>();

                                        Mapper.DynamicMap(importDictionaryIndicatorTranslationRequest, dictionaryIndicatorTranslation);
                                        dictionaryIndicatorTranslation.DictionaryIndicatorId = dictionaryIndicator.Id;

                                        dictionaryIndicatorTranslations.Add(dictionaryIndicatorTranslation);
                                    }
                                }
                            }
                        }
                    }

                    repository.Save(dictionary);
                    repository.SaveList(dictionaryClusters);
                    repository.SaveList(dictionaryClusterTranslations);
                    repository.SaveList(dictionaryCompetences);
                    repository.SaveList(dictionaryCompetenceTranslations);
                    repository.SaveList(dictionaryLevels);
                    repository.SaveList(dictionaryLevelTranslations);
                    repository.SaveList(dictionaryIndicators);
                    repository.SaveList(dictionaryIndicatorTranslations);

                    return dictionary.Id;
                }
            });
        }

        public void NormalizeDictionaryClusterOrder(Guid dictionaryId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var queryService = Container.Resolve<IDictionaryManagementQueryService>();

                    var dictionary = queryService.RetrieveDictionaryAdmin(dictionaryId);

                    int order = 0;
                    foreach (var dictionaryCluster in dictionary.DictionaryClusters.OrderBy(dc => dc.Order))
                    {
                        var cluster = repository.Retrieve<DictionaryCluster>(dictionaryCluster.Id);
                        cluster.Order = ++order * 10;
                        repository.Save(cluster);
                    }
                }
            });
        }

        public void NormalizeDictionaryCompetenceOrder(Guid dictionaryClusterId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var queryService = Container.Resolve<IDictionaryManagementQueryService>();

                    var dictionaryCluster = queryService.RetrieveDictionaryClusterAdmin(dictionaryClusterId);

                    int order = 0;
                    foreach (var dictionaryCompetence in dictionaryCluster.DictionaryCompetences.OrderBy(dc => dc.Order))
                    {
                        var competence = repository.Retrieve<DictionaryCompetence>(dictionaryCompetence.Id);
                        competence.Order = ++order * 10;
                        repository.Save(competence);
                    }
                }
            });
        }

        public void NormalizeDictionaryIndicatorOrder(Guid dictionaryLevelId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<IDictionaryManagementCommandRepository>())
                {
                    var queryService = Container.Resolve<IDictionaryManagementQueryService>();

                    var dictionaryLevel = queryService.RetrieveDictionaryLevelAdmin(dictionaryLevelId);

                    int order = 0;
                    foreach (var dictionaryIndicator in dictionaryLevel.DictionaryIndicators.OrderBy(dc => dc.Order))
                    {
                        var indicator = repository.Retrieve<DictionaryIndicator>(dictionaryIndicator.Id);
                        indicator.Order = ++order * 10;
                        repository.Save(indicator);
                    }
                }
            });
        }
    }
}
