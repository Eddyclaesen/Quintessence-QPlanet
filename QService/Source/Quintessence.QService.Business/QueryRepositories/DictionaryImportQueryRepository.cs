using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Business.Model;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class DictionaryImportQueryRepository : IDictionaryImportQueryRepository
    {
        private readonly IUnityContainer _unityContainer;
        private Dictionary<string, string> _connectionStrings;

        public DictionaryImportQueryRepository(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            PopulateConnectionString();
        }

        private void PopulateConnectionString()
        {
            _connectionStrings = new Dictionary<string, string>
                {
                    {".xls", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;"},
                    {".xlsx","Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=\"Excel 12.0 Xml;IMEX=1;\""}
                };
        }

        public DictionaryImportView ProcessDictionaryFile(string filename)
        {
            try
            {
                var file = new FileInfo(filename);

                if (!file.Exists)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("Unable to retrieve {0} for processing.", filename));
                    return null;
                }

                var connectionString = string.Format(_connectionStrings[file.Extension], filename);

                var connection = new OleDbConnection(connectionString);

                var infrastructureQueryService = _unityContainer.Resolve<IInfrastructureQueryService>();

                var languages = infrastructureQueryService.ListLanguages();

                var dictionaryDataSet = new DataSet();
                foreach (var language in languages.OrderBy(l => l.Id))
                {
                    try
                    {
                        var command = new OleDbCommand(string.Format("SELECT * FROM [{0}$]", language.Code), connection);
                        var adapter = new OleDbDataAdapter(command);
                        var table = new DataTable(language.Id.ToString(CultureInfo.InvariantCulture));
                        adapter.Fill(table);
                        dictionaryDataSet.Tables.Add(table);
                    }
                    catch (Exception exception)
                    {
                        LogManager.LogError(exception);
                    }
                }

                var dictionaryEntrySets = dictionaryDataSet.Tables.OfType<DataTable>().Select(table => new DictionaryEntrySet(table)).ToList();

                if (!dictionaryEntrySets.Any())
                    throw new ArgumentOutOfRangeException("filename", "No dictionaries found.");

                var dictionary = new DictionaryImportView();
                dictionary.Name = file.Name;
                dictionary.ContactId = 3;

                var dictionaryLanguages = dictionaryEntrySets
                    .ToDictionary(dictionaryEntrySet => dictionaryEntrySet.FirstOrDefault().LanguageId, dictionaryEntrySet =>
                        {
                            var clusterIndex = 0;
                            var competenceIndex = 0;
                            var levelIndex = 0;
                            var indicatorIndex = 0;
                            return dictionaryEntrySet
                                .GroupBy(a => new {a.Cluster, a.ClusterDefinition, a.LanguageId})
                                .Where(a => !string.IsNullOrWhiteSpace(a.Key.Cluster))
                                .Select(a => new DictionaryClusterImportView
                                    {
                                        Id = clusterIndex++,
                                        Name = a.Key.Cluster,
                                        LanguageId = a.Key.LanguageId,
                                        Description = a.Key.ClusterDefinition,
                                        Order = clusterIndex * 10,
                                        DictionaryCompetences = a
                                                 .GroupBy(b => new {b.Competence, b.CompetenceDefinition})
                                                 .Where(b => !string.IsNullOrWhiteSpace(b.Key.Competence))
                                                 .Select(b => new DictionaryCompetenceImportView
                                                     {
                                                         Id = competenceIndex++,
                                                         Name = b.Key.Competence,
                                                         LanguageId = a.Key.LanguageId,
                                                         Description = b.Key.CompetenceDefinition,
                                                         Order = competenceIndex * 10,
                                                         DictionaryLevels = b
                                                                  .GroupBy(c => new {c.Level, c.LevelDescription})
                                                                  .Select(c => new DictionaryLevelImportView
                                                                      {
                                                                          Id = levelIndex++,
                                                                          Level = ParseLevel(c.Key.Level),
                                                                          LanguageId = a.Key.LanguageId,
                                                                          Name = c.Key.LevelDescription,
                                                                          DictionaryIndicators = c
                                                                                   .Where(d => !string.IsNullOrWhiteSpace(d.Indicator))
                                                                                   .Select(d => new DictionaryIndicatorImportView
                                                                                           {
                                                                                               Id = indicatorIndex++,
                                                                                               LanguageId = a.Key.LanguageId,
                                                                                               Name = d.Indicator,
                                                                                               Order = indicatorIndex * 10
                                                                                           })
                                                                                   .ToList()
                                                                      }).ToList()
                                                     })
                                                 .ToList()
                                    }).ToList();
                        });

                var dictionaryLanguage = dictionaryLanguages.FirstOrDefault();

                dictionary.DictionaryClusters = dictionaryLanguage.Value;

                foreach (var dictionaryCluster in dictionary.DictionaryClusters)
                {
                    dictionaryCluster.DictionaryClusterTranslations =
                        dictionaryLanguages.SelectMany(a => a.Value.Where(b => b.Id == dictionaryCluster.Id))
                                           .Select(c => new DictionaryClusterTranslationImportView
                                                   {
                                                       LanguageId = c.LanguageId,
                                                       Description = c.Description,
                                                       Text = c.Name
                                                   })
                                           .ToList();

                    foreach (var dictionaryCompetence in dictionaryCluster.DictionaryCompetences)
                    {
                        dictionaryCompetence.DictionaryCompetenceTranslations =
                            dictionaryLanguages.SelectMany(a => a.Value.SelectMany(b => b.DictionaryCompetences).Where(b => b.Id == dictionaryCompetence.Id))
                                               .Select(c => new DictionaryCompetenceTranslationImportView
                                               {
                                                   LanguageId = c.LanguageId,
                                                   Description = c.Description,
                                                   Text = c.Name
                                               })
                                               .ToList();

                        foreach (var dictionaryLevel in dictionaryCompetence.DictionaryLevels)
                        {
                            dictionaryLevel.DictionaryLevelTranslations =
                                dictionaryLanguages.SelectMany(a => a.Value.SelectMany(b => b.DictionaryCompetences).SelectMany(c => c.DictionaryLevels).Where(b => b.Id == dictionaryLevel.Id))
                                                   .Select(c => new DictionaryLevelTranslationImportView
                                                   {
                                                       LanguageId = c.LanguageId,
                                                       Text = c.Name
                                                   })
                                                   .ToList();

                            foreach (var dictionaryIndicator in dictionaryLevel.DictionaryIndicators)
                            {
                                dictionaryIndicator.DictionaryIndicatorTranslations =
                                    dictionaryLanguages.SelectMany(a => a.Value.SelectMany(b => b.DictionaryCompetences).SelectMany(c => c.DictionaryLevels).SelectMany(d => d.DictionaryIndicators).Where(b => b.Id == dictionaryIndicator.Id))
                                                       .Select(c => new DictionaryIndicatorTranslationImportView
                                                       {
                                                           LanguageId = c.LanguageId,
                                                           Text = c.Name
                                                       })
                                                       .ToList();
                            }
                        }
                    }
                }

                return dictionary;
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                ValidationContainer.RegisterException(exception);
                return null;
            }
        }

        private ValidationContainer ValidationContainer
        {
            get { return _unityContainer.Resolve<ValidationContainer>(); }
        }

        private int ParseLevel(string level)
        {
            var lev = 0;
            if (int.TryParse(level, out lev))
                return lev;
            return lev;
        }
    }
}