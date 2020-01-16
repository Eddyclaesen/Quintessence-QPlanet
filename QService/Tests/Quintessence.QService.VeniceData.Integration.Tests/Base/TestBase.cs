using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.Infrastructure.Model.DataModel;
using Quintessence.QService.Business.Model;
using Quintessence.QService.DataModel.Dim;

namespace Quintessence.QService.VeniceData.Integration.Tests.Base
{
    [TestClass]
    public class TestBase
    {
        [TestMethod]
        public void ImportExcel()
        {
            var file = new FileInfo(@"C:\Users\sve\Google Drive\Palm_Dictionary_2013.xlsx");

            if (!file.Exists)
                return;

            //var excelConnectString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", file.FullName);
            var excelConnectString = string.Format("Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=\"Excel 12.0 Xml;IMEX=1;\"", file.FullName);

            var connection = new OleDbConnection(excelConnectString);

            var commandNl = new OleDbCommand("SELECT * FROM [NL$]", connection);
            var commandFr = new OleDbCommand("SELECT * FROM [FR$]", connection);
            var commandEn = new OleDbCommand("SELECT * FROM [EN$]", connection);
            //var commandDe = new OleDbCommand("SELECT * FROM [DE$]", connection);

            var adapterNl = new OleDbDataAdapter { SelectCommand = commandNl };
            var adapterFr = new OleDbDataAdapter { SelectCommand = commandFr };
            var adapterEn = new OleDbDataAdapter { SelectCommand = commandEn };
            //var adapterDe = new OleDbDataAdapter { SelectCommand = commandDe };

            var dataset = new DataSet();
            var tableNl = dataset.Tables.Add("Nl");
            var tableFr = dataset.Tables.Add("Fr");
            var tableEn = dataset.Tables.Add("En");
            //var tableDe = dataset.Tables.Add("De");

            adapterNl.Fill(tableNl);
            adapterFr.Fill(tableFr);
            adapterEn.Fill(tableEn);
            //adapterDe.Fill(tableDe);

            Assert.IsNotNull(dataset);

            var entriesNl = new DictionaryEntrySet(tableNl);
            var entriesFr = new DictionaryEntrySet(tableFr);
            var entriesEn = new DictionaryEntrySet(tableEn);

            var dictionary = Create<Dictionary>();
            dictionary.Name = "Palm 2013";
            dictionary.ContactId = 1310;

            using (var context = new DictionaryCommandContext())
            {
                context.Dictionaries.Add(dictionary);

                context.SaveChanges();

                var clustersNl = entriesNl.GroupBy(e => new { e.Cluster, e.ClusterDefinition }).ToList();
                var clustersFr = entriesFr.GroupBy(e => new { e.Cluster, e.ClusterDefinition }).ToList();
                var clustersEn = entriesEn.GroupBy(e => new { e.Cluster, e.ClusterDefinition }).ToList();

                for (int w = 0; w < clustersEn.Count; w++)
                {
                    var dictionaryCluster = context.DictionaryClusters.Add(Create<DictionaryCluster>());
                    dictionaryCluster.DictionaryId = dictionary.Id;
                    dictionaryCluster.Name = clustersEn[w].Key.Cluster;
                    dictionaryCluster.Description = clustersEn[w].Key.ClusterDefinition;
                    dictionaryCluster.Order = w;

                    context.SaveChanges();

                    var dcl = context.DictionaryClusterTranslations.Add(Create<DictionaryClusterTranslation>());
                    dcl.LanguageId = 1;
                    dcl.DictionaryClusterId = dictionaryCluster.Id;
                    dcl.Text = clustersNl[w].Key.Cluster;
                    dcl.Description = clustersNl[w].Key.ClusterDefinition;

                    dcl = context.DictionaryClusterTranslations.Add(Create<DictionaryClusterTranslation>());
                    dcl.LanguageId = 2;
                    dcl.DictionaryClusterId = dictionaryCluster.Id;
                    dcl.Text = clustersFr[w].Key.Cluster;
                    dcl.Description = clustersFr[w].Key.ClusterDefinition;

                    dcl = context.DictionaryClusterTranslations.Add(Create<DictionaryClusterTranslation>());
                    dcl.LanguageId = 3;
                    dcl.DictionaryClusterId = dictionaryCluster.Id;
                    dcl.Text = clustersEn[w].Key.Cluster;
                    dcl.Description = clustersEn[w].Key.ClusterDefinition;

                    context.SaveChanges();

                    var competencesNl = clustersNl[w].GroupBy(e => new { e.Competence, e.CompetenceDefinition }).ToList();
                    var competencesFr = clustersFr[w].GroupBy(e => new { e.Competence, e.CompetenceDefinition }).ToList();
                    var competencesEn = clustersEn[w].GroupBy(e => new { e.Competence, e.CompetenceDefinition }).ToList();

                    for (int x = 0; x < competencesEn.Count; x++)
                    {
                        var dictionaryCompetence = context.DictionaryCompetences.Add(Create<DictionaryCompetence>());
                        dictionaryCompetence.DictionaryClusterId = dictionaryCluster.Id;
                        dictionaryCompetence.Name = competencesEn[x].Key.Competence;

                        context.SaveChanges();

                        var dco = context.DictionaryCompetenceTranslations.Add(Create<DictionaryCompetenceTranslation>());
                        dco.LanguageId = 1;
                        dco.DictionaryCompetenceId = dictionaryCompetence.Id;
                        dco.Text = competencesNl[x].Key.Competence;
                        dco.Description = competencesNl[x].Key.CompetenceDefinition;

                        dco = context.DictionaryCompetenceTranslations.Add(Create<DictionaryCompetenceTranslation>());
                        dco.LanguageId = 2;
                        dco.DictionaryCompetenceId = dictionaryCompetence.Id;
                        dco.Text = competencesFr[x].Key.Competence;
                        dco.Description = competencesFr[x].Key.CompetenceDefinition;

                        dco = context.DictionaryCompetenceTranslations.Add(Create<DictionaryCompetenceTranslation>());
                        dco.LanguageId = 3;
                        dco.DictionaryCompetenceId = dictionaryCompetence.Id;
                        dco.Text = competencesEn[x].Key.Competence;
                        dco.Description = competencesEn[x].Key.CompetenceDefinition;

                        context.SaveChanges();

                        var levelsNl = competencesNl[x].GroupBy(e => new { e.Level, e.LevelDescription }).ToList();
                        var levelsFr = competencesFr[x].GroupBy(e => new { e.Level, e.LevelDescription }).ToList();
                        var levelsEn = competencesEn[x].GroupBy(e => new { e.Level, e.LevelDescription }).ToList();

                        for (int y = 0; y < levelsEn.Count; y++)
                        {
                            var dictionaryLevel = context.DictionaryLevels.Add(Create<DictionaryLevel>());
                            dictionaryLevel.DictionaryCompetenceId = dictionaryCompetence.Id;
                            dictionaryLevel.Level = int.Parse(levelsFr[y].Key.Level);
                            dictionaryLevel.Name = levelsFr[y].Key.Level;

                            context.SaveChanges();

                            var dlt = context.DictionaryLevelTranslations.Add(Create<DictionaryLevelTranslation>());
                            dlt.LanguageId = 1;
                            dlt.DictionaryLevelId = dictionaryLevel.Id;
                            dlt.Text = levelsNl[y].Key.LevelDescription ?? string.Empty;

                            dlt = context.DictionaryLevelTranslations.Add(Create<DictionaryLevelTranslation>());
                            dlt.LanguageId = 2;
                            dlt.DictionaryLevelId = dictionaryLevel.Id;
                            dlt.Text = levelsFr[y].Key.LevelDescription ?? string.Empty;

                            dlt = context.DictionaryLevelTranslations.Add(Create<DictionaryLevelTranslation>());
                            dlt.LanguageId = 3;
                            dlt.DictionaryLevelId = dictionaryLevel.Id;
                            dlt.Text = levelsEn[y].Key.LevelDescription ?? string.Empty;

                            context.SaveChanges();

                            var indicatorsNl = levelsNl[y].ToList();
                            var indicatorsFr = levelsFr[y].ToList();
                            var indicatorsEn = levelsEn[y].ToList();

                            for (int z = 0; z < indicatorsEn.Count; z++)
                            {
                                var dictionaryIndicator = context.DictionaryIndicators.Add(Create<DictionaryIndicator>());
                                dictionaryIndicator.DictionaryLevelId = dictionaryLevel.Id;
                                dictionaryIndicator.Name = indicatorsFr[z].Indicator;

                                context.SaveChanges();

                                var dit = context.DictionaryIndicatorTranslations.Add(Create<DictionaryIndicatorTranslation>());
                                dit.LanguageId = 1;
                                dit.DictionaryIndicatorId = dictionaryIndicator.Id;
                                dit.Text = indicatorsNl[z].Indicator;

                                dit = context.DictionaryIndicatorTranslations.Add(Create<DictionaryIndicatorTranslation>());
                                dit.LanguageId = 2;
                                dit.DictionaryIndicatorId = dictionaryIndicator.Id;
                                dit.Text = indicatorsFr[z].Indicator;

                                dit = context.DictionaryIndicatorTranslations.Add(Create<DictionaryIndicatorTranslation>());
                                dit.LanguageId = 3;
                                dit.DictionaryIndicatorId = dictionaryIndicator.Id;
                                dit.Text = indicatorsEn[z].Indicator;

                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        public static TEntity Create<TEntity>()
            where TEntity : EntityBase, new()
        {
            var entity = new TEntity
            {
                Id = Guid.NewGuid(),
                Audit = new Audit
                {
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    VersionId = Guid.NewGuid()
                }
            };

            return entity;
        }
    }

    public class DictionaryEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.ToLowerInvariant() == y.ToLowerInvariant();
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }

    public class DictionaryCommandContext : DbContext
    {
        public DictionaryCommandContext()
            : base(@"Data Source=vm-quintsql;Initial Catalog=Quintessence;Integrated Security=true") { }

        public IDbSet<Dictionary> Dictionaries { get; set; }
        public IDbSet<DictionaryCluster> DictionaryClusters { get; set; }
        public IDbSet<DictionaryClusterTranslation> DictionaryClusterTranslations { get; set; }
        public IDbSet<DictionaryCompetence> DictionaryCompetences { get; set; }
        public IDbSet<DictionaryCompetenceTranslation> DictionaryCompetenceTranslations { get; set; }
        public IDbSet<DictionaryLevel> DictionaryLevels { get; set; }
        public IDbSet<DictionaryLevelTranslation> DictionaryLevelTranslations { get; set; }
        public IDbSet<DictionaryIndicator> DictionaryIndicators { get; set; }
        public IDbSet<DictionaryIndicatorTranslation> DictionaryIndicatorTranslations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
