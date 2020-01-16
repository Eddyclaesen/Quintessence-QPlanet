using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Data.IntegrationTests.Base;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.Data.IntegrationTests
{
    [TestClass]
    public class QuintessenceDomQueryContextTests : TestBase
    {
        [TestMethod]
        public void TestListDocuments()
        {
            using(var context = Container.Resolve<IDomQueryContext>())
            {
                Assert.IsNotNull(context);

                var documents = context.ListDocumentsByContact(3);

                Assert.IsNotNull(documents);
            }
        }
    }
}
