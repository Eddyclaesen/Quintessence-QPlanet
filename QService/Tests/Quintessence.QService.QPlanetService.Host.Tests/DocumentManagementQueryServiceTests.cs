using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Host.Tests.Base;

namespace Quintessence.QService.QPlanetService.Host.Tests
{
    //[TestClass]
    public class DocumentManagementQueryServiceTests : ServiceTestBase
    {
        /// <summary>
        /// Tests the list project documents.
        /// </summary>
        [TestMethod]
        public void TestListProjectDocuments()
        {
            var response =
                Execute<IDocumentManagementQueryService, ListProjectDocumentsResponse>(
                    service => service.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = Guid.Parse("97bf640d-0106-4e23-9304-f8d0b18422a5") }));

            Assert.IsNotNull(response);
        }
    }
}
