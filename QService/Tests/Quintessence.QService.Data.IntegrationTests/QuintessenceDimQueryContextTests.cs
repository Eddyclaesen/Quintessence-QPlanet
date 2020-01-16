using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Data.IntegrationTests.Base;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.Data.QueryContext;

namespace Quintessence.QService.Data.IntegrationTests
{
    [TestClass]
    public class QuintessenceDimQueryContextTests : TestBase
    {
        [TestMethod]
        public void TestIDbSetProperties()
        {
            var properties = typeof(IDimQueryContext).GetProperties();

            foreach (var property in properties)
            {
                using (var context = CreateContext<IDimQueryContext>())
                {
                    var result = Enumerable.ToList(((dynamic)property.GetValue(context)));

                    Assert.IsNotNull(result);
                }
            }
        }

        [TestMethod]
        public void TestListDictionaries()
        {
            using (var context = CreateContext<IDimQueryContext>())
            {
                Assert.IsInstanceOfType(context, typeof(QuintessenceQueryContext));

                var dictionaries = context.Dictionaries
                    .Where(d => !d.Audit.IsDeleted).ToList();

                Assert.IsNotNull(dictionaries);
                Assert.IsTrue(dictionaries.Any());
            }
        }

        [TestMethod]
        public void TestListDictionariesWithClusters()
        {
            using (var context = CreateContext<IDimQueryContext>())
            {
                Assert.IsInstanceOfType(context, typeof(QuintessenceQueryContext));

                var dictionaries = context.Dictionaries
                    .Include(d => d.DictionaryClusters)
                    .Where(d => !d.Audit.IsDeleted).ToList();

                Assert.IsNotNull(dictionaries);
                Assert.IsTrue(dictionaries.Count > 0);
                Assert.IsTrue(dictionaries.SelectMany(d => d.DictionaryClusters).Any());
            }
        }

        [TestMethod]
        public void TestListDictionariesWithClustersWithCompetences()
        {
            using (var context = CreateContext<IDimQueryContext>())
            {
                Assert.IsInstanceOfType(context, typeof(QuintessenceQueryContext));

                var dictionaries = context.Dictionaries
                    .Include(d => d.DictionaryClusters.Select(dc => dc.DictionaryCompetences))
                    .Where(d => !d.Audit.IsDeleted).ToList();

                Assert.IsNotNull(dictionaries);
                Assert.IsTrue(dictionaries.Count > 0);
                Assert.IsTrue(dictionaries.SelectMany(d => d.DictionaryClusters).Any());
                Assert.IsTrue(dictionaries.SelectMany(d => d.DictionaryClusters).SelectMany(dc => dc.DictionaryCompetences).Any());
            }
        }
    }
}
