using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Data.IntegrationTests.Base;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.Data.IntegrationTests
{
    [TestClass]
    public class QuintessenceRepQueryContextTests : TestBase
    {
        [TestMethod]
        public void TestIDbSetProperties()
        {
            var properties = typeof(IRepQueryContext).GetProperties();

            foreach (var property in properties)
            {
                using (var context = CreateContext<IRepQueryContext>())
                {
                    var result = Enumerable.ToList(((dynamic)property.GetValue(context)));

                    Assert.IsNotNull(result);
                }
            }
        }
    }
}