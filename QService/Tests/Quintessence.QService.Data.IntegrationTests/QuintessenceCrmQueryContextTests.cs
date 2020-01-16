using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Data.IntegrationTests.Base;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.Data.IntegrationTests
{
    [TestClass]
    public class QuintessenceCrmQueryContextTests : TestBase
    {
        [TestMethod]
        public void TestIDbSetProperties()
        {
            var properties = typeof(ICrmQueryContext).GetProperties();

            foreach (var property in properties)
            {
                using (var context = CreateContext<ICrmQueryContext>())
                {
                    var result = Enumerable.ToList(((dynamic)property.GetValue(context)));

                    Assert.IsNotNull(result);
                }
            }
        }
    }
}