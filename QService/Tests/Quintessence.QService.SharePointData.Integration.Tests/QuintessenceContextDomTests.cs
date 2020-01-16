using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.SharePointData.Integration.Tests.Base;

namespace Quintessence.QService.SharePointData.Integration.Tests
{
    [TestClass]
    public class QuintessenceContextDomTests : TestBase
    {
        [TestMethod]
        public void TestListDocuments()
        {
            using(var context = Container.Resolve<IDomQueryContext>())
            {
                Assert.IsNotNull(context);

                var documents = context.ListDocumentsByContact(3).ToList();

                Assert.IsNotNull(documents);

                documents = context.ListDocumentsByProject(3).ToList();

                Assert.IsNotNull(documents);
            }
        }
    }
}
