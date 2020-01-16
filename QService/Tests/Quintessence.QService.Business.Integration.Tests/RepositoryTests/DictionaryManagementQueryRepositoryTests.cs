using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;

namespace Quintessence.QService.Business.Integration.Tests.RepositoryTests
{
    /// <summary>
    /// Tests the dictionary manegement query repository
    /// </summary>
    [TestClass]
    public class DictionaryManagementQueryRepositoryTests : QueryRepositoryTestBase<IDictionaryManagementQueryRepository>
    {
        /// <summary>
        /// Tests the search dictionaries.
        /// </summary>
        [TestMethod]
        public void TestSearchDictionaries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.SearchDictionaries();

            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);
        }

        /// <summary>
        /// Tests the list dictionaries.
        /// </summary>
        [TestMethod]
        public void TestListDictionaries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListDictionaries();

            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);
        }

        /// <summary>
        /// Tests the list customer dictionaries.
        /// </summary>
        [TestMethod]
        public void TestListCustomerDictionaries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListCustomerDictionaries();

            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);
        }

        /// <summary>
        /// Tests the list available dictionaries.
        /// </summary>
        [TestMethod]
        public void TestListAvailableDictionaries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            //list customer dictionaries
            var dictionaries = repository.ListCustomerDictionaries();
            Assert.IsNotNull(dictionaries);

            var dictionary = dictionaries.FirstOrDefault();
            Assert.IsNotNull(dictionary);

            var contactId = dictionary.ContactId;
            Assert.IsNotNull(contactId);

            var availableDictionaries = repository.ListAvailableDictionaries(contactId.Value);
            Assert.IsNotNull(availableDictionaries);
            Assert.IsTrue(availableDictionaries.Count > 0);
        }

        /// <summary>
        /// Tests the list detailed dictionaries.
        /// </summary>
        [TestMethod]
        public void TestListDetailedDictionaries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            //List Quintessence dictionaries till clusters
            var dictionariesTillClusters = repository.ListDetailedDictionaries(null, tillClusters: true);
            Assert.IsTrue(dictionariesTillClusters.Count >= 1);

            //List Quintessence dictionaries till competences
            var dictionariesTillCompetences = repository.ListDetailedDictionaries(null, tillCompetences: true);
            Assert.IsTrue(dictionariesTillCompetences.Count >= 1);

            //List Quintessence dictionaries till levels
            var dictionariesTillLevels = repository.ListDetailedDictionaries(null, tillLevels: true);
            Assert.IsTrue(dictionariesTillLevels.Count >= 1);

            //List Quintessence dictionaries till indicators
            var dictionariesTillIndicators = repository.ListDetailedDictionaries(null, tillIndicators: true);
            Assert.IsTrue(dictionariesTillIndicators.Count >= 1);
        }

        /// <summary>
        /// Tests the list dictionaries by contact id.
        /// </summary>
        [TestMethod]
        public void TestListDictionariesByContactId()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListCustomerDictionaries();
            Assert.IsNotNull(dictionaries);

            var dictionary = dictionaries.ElementAtOrDefault(new Random().Next(dictionaries.Count - 1));
            Assert.IsNotNull(dictionary);
            Assert.IsNotNull(dictionary.ContactId);

            dictionaries = repository.ListDictionariesByContactId(dictionary.ContactId.Value);

            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);
            Assert.IsTrue(dictionaries.All(d => d.ContactId == dictionary.ContactId));
        }

        /// <summary>
        /// Tests the name of the list dictionaries by contact.
        /// </summary>
        [TestMethod]
        public void TestListDictionariesByContactName()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListCustomerDictionaries();
            Assert.IsNotNull(dictionaries);

            var dictionary = dictionaries.ElementAtOrDefault(new Random().Next(dictionaries.Count - 1));
            Assert.IsNotNull(dictionary);
            Assert.IsNotNull(dictionary.Contact);

            dictionaries = repository.ListDictionariesByContactName(dictionary.Contact.Name);

            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);
            Assert.IsTrue(dictionaries.All(d => d.Contact.Name.Contains(dictionary.Contact.Name)));
        }

        /// <summary>
        /// Tests the list quintessence dictionaries.
        /// </summary>
        [TestMethod]
        public void TestListQuintessenceDictionaries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);
            Assert.IsTrue(dictionaries.All(d => !d.ContactId.HasValue));
        }

        /// <summary>
        /// Tests the retrieve dictionary detail.
        /// </summary>
        [TestMethod]
        public void TestRetrieveDictionaryDetail()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);

            var dictionary = dictionaries.ElementAt(new Random().Next(dictionaries.Count - 1));

            dictionary = repository.RetrieveDictionaryDetail(dictionary.Id, new List<int> { 1, 2, 3, 4 });

            Assert.IsNotNull(dictionary);

            Assert.IsNotNull(dictionary.DictionaryClusters);
            Assert.IsTrue(dictionary.DictionaryClusters.Any());
            Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryClusterTranslations).Any());

            Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryCompetences).Any());
            Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryCompetences.SelectMany(dco => dco.DictionaryCompetenceTranslations)).Any());

            Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryCompetences.SelectMany(dco => dco.DictionaryLevels)).Any());
            //Some dictionarylevels don't have translations
            //Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryCompetences.SelectMany(dco => dco.DictionaryLevels.SelectMany(dl => dl.DictionaryLevelTranslations))).Any());

            Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryCompetences.SelectMany(dco => dco.DictionaryLevels.SelectMany(dl => dl.DictionaryIndicators))).Any());
            Assert.IsTrue(dictionary.DictionaryClusters.SelectMany(dc => dc.DictionaryCompetences.SelectMany(dco => dco.DictionaryLevels.SelectMany(dl => dl.DictionaryIndicators.SelectMany(di => di.DictionaryIndicatorTranslations)))).Any());
        }

        /// <summary>
        /// Tests the retrieve dictionary.
        /// </summary>
        [TestMethod]
        public void TestRetrieveDictionary()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);

            var dictionary = dictionaries.ElementAt(new Random().Next(dictionaries.Count - 1));

            dictionary = repository.RetrieveDictionary(dictionary.Id);

            Assert.IsNotNull(dictionary);
        }

        /// <summary>
        /// Tests the list dictionary indicator matrix entries.
        /// </summary>
        [TestMethod]
        public void TestListDictionaryIndicatorMatrixEntries()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);

            var dictionary = dictionaries.ElementAt(new Random().Next(dictionaries.Count - 1));

            dictionary = repository.RetrieveDictionary(dictionary.Id);

            Assert.IsNotNull(dictionary);

            var entries = repository.ListDictionaryIndicatorMatrixEntries(dictionary.Id);

            Assert.IsNotNull(entries);
        }

        /// <summary>
        /// Tests the list indicators by dictionary level.
        /// </summary>
        [TestMethod]
        public void TestListIndicatorsByDictionaryLevel()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);

            var dictionary = dictionaries.ElementAt(new Random().Next(dictionaries.Count - 1));

            dictionary = repository.RetrieveDictionaryDetail(dictionary.Id);

            Assert.IsNotNull(dictionary);

            foreach (var level in dictionary.DictionaryClusters.SelectMany(dcl => dcl.DictionaryCompetences.SelectMany(dco => dco.DictionaryLevels)))
            {
                var indicators = repository.ListIndicatorsByDictionaryLevel(level.Id);
                Assert.IsNotNull(indicators);
                Assert.AreEqual(level.DictionaryIndicators.Count, indicators.Count);
            }
        }

        /// <summary>
        /// Tests the list levels by dictionary competence.
        /// </summary>
        [TestMethod]
        public void TestListLevelsByDictionaryCompetence()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);

            var dictionary = dictionaries.ElementAt(new Random().Next(dictionaries.Count - 1));

            dictionary = repository.RetrieveDictionaryDetail(dictionary.Id);

            Assert.IsNotNull(dictionary);

            foreach (var competence in dictionary.DictionaryClusters.SelectMany(dcl => dcl.DictionaryCompetences))
            {
                var levels = repository.ListLevelsByDictionaryCompetence(competence.Id);
                Assert.IsNotNull(levels);
                Assert.AreEqual(competence.DictionaryLevels.Count, levels.Count);
            }
        }

        /// <summary>
        /// Tests the list indicators by dictionary competence.
        /// </summary>
        [TestMethod]
        public void TestListIndicatorsByDictionaryCompetence()
        {
            var repository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var dictionaries = repository.ListQuintessenceDictionaries();
            Assert.IsNotNull(dictionaries);
            Assert.IsTrue(dictionaries.Count > 0);

            var dictionary = dictionaries.ElementAt(new Random().Next(dictionaries.Count - 1));

            dictionary = repository.RetrieveDictionaryDetail(dictionary.Id);

            Assert.IsNotNull(dictionary);

            foreach (var competence in dictionary.DictionaryClusters.SelectMany(dcl => dcl.DictionaryCompetences))
            {
                var indicators = repository.ListIndicatorsByDictionaryCompetence(competence.Id);
                Assert.IsNotNull(indicators);
                Assert.AreEqual(competence.DictionaryLevels.SelectMany(dl => dl.DictionaryIndicators).Count(), indicators.Count);
            }
        }
    }
}
