using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class ReportManagementQueryServiceImplementationTests : QueryServiceImplementationTestBase<IReportManagementQueryService>
    {
        /// <summary>
        /// Tests the list candidate report definitions.
        /// </summary>
        [TestMethod]
        public void TestListCandidateReportDefinitions()
        {
            var service = Container.Resolve<IReportManagementQueryService>();

            var candidateReportDefinitionId = ReportManagementCommandServiceImplementationTests.CreateNewCandidateReportDefinition(Container, "Some name", "/Some/Location");

            var candidateReportDefinitions = service.ListCandidateReportDefinitions();

            Assert.IsNotNull(candidateReportDefinitions);
            Assert.IsTrue(candidateReportDefinitions.Count > 0);
            Assert.IsTrue(candidateReportDefinitions.Any(crd => crd.Id == candidateReportDefinitionId));
        }

        /// <summary>
        /// Tests the retrieve candidate report definition.
        /// </summary>
        [TestMethod]
        public void TestRetrieveCandidateReportDefinition()
        {
            var service = Container.Resolve<IReportManagementQueryService>();

            const string name = "Some weird name";
            const string location = "/Some/Weird/Location";

            var candidateReportDefinitionId = ReportManagementCommandServiceImplementationTests.CreateNewCandidateReportDefinition(Container, name, location);

            var candidateReportDefinition = service.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);
            Assert.AreEqual(candidateReportDefinitionId, candidateReportDefinition.Id);
            Assert.AreEqual(location, candidateReportDefinition.Location);
            Assert.AreEqual(name, candidateReportDefinition.Name);
        }

        /// <summary>
        /// Tests the retrieve candidate report definition field.
        /// </summary>
        [TestMethod]
        public void TestRetrieveCandidateReportDefinitionField()
        {
            var service = Container.Resolve<IReportManagementQueryService>();

            const string name = "Name";
            const string code = "CODE";


            var candidateReportDefinitionFieldId = ReportManagementCommandServiceImplementationTests.CreateNewCandidateReportDefinitionField(Container, name, code);

            var candidateReportDefinitionField = service.RetrieveCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            Assert.IsNotNull(candidateReportDefinitionField);
            Assert.AreEqual(candidateReportDefinitionFieldId, candidateReportDefinitionField.Id);
            Assert.AreEqual(name, candidateReportDefinitionField.Name);
            Assert.AreEqual(code, candidateReportDefinitionField.Code);
        }

        /// <summary>
        /// Tests the list candidate report definitions for customer.
        /// </summary>
        [TestMethod]
        public void TestListCandidateReportDefinitionsForCustomer()
        {
            var service = Container.Resolve<IReportManagementQueryService>();

            ReportManagementCommandServiceImplementationTests.CreateNewCandidateReportDefinition(Container, "Some name", "/Some/Location");

            var candidateReportDefinitions = service.ListCandidateReportDefinitions();

            var candidateReportDefinitionForCustomer = candidateReportDefinitions.FirstOrDefault(crd => crd.ContactId.HasValue);

            Assert.IsNotNull(candidateReportDefinitionForCustomer);
            Assert.IsNotNull(candidateReportDefinitionForCustomer.ContactId);

            var customerCandidateReportDefinitions = service.ListCandidateReportDefinitionsForCustomer(candidateReportDefinitionForCustomer.ContactId.Value);

            Assert.IsNotNull(customerCandidateReportDefinitions);
            Assert.IsTrue(customerCandidateReportDefinitions.All(crd => crd.ContactId.HasValue && crd.ContactId == candidateReportDefinitionForCustomer.ContactId));
        }

        /// <summary>
        /// Tests the list candidate score report types.
        /// </summary>
        [TestMethod]
        public void TestListCandidateScoreReportTypes()
        {
            var service = Container.Resolve<IReportManagementQueryService>();

            var reportTypes = service.ListCandidateScoreReportTypes();

            Assert.IsNotNull(reportTypes);
            Assert.AreEqual(2, reportTypes.Count);
        }

        /// <summary>
        /// Tests the list report types.
        /// </summary>
        [TestMethod]
        public void TestListReportTypes()
        {
            var service = Container.Resolve<IReportManagementQueryService>();

            var reportTypes = service.ListReportTypes();

            Assert.IsNotNull(reportTypes);
            Assert.IsTrue(reportTypes.Count > 0);
        }

        /// <summary>
        /// Tests the list report definitions.
        /// </summary>
        [TestMethod]
        public void TestListReportDefinitions()
        {
            var service = Container.Resolve<IReportManagementQueryService>();
            var commandService = Container.Resolve<IReportManagementCommandService>();

            var reportTypes = service.ListReportTypes();

            foreach (var reportType in reportTypes)
            {
                var request = new ListReportDefinitionsRequest { ReportTypeId = reportType.Id };
                var reportDefinitions = service.ListReportDefinitions(request);

                Assert.IsNotNull(reportDefinitions);

                if (reportDefinitions.Count == 0)
                {
                    var createNewReportDefinitionRequest = new CreateNewReportDefinitionRequest
                    {
                        Name = "Some report name",
                        Location = "some report location",
                        ReportTypeId = reportType.Id
                    };
                    commandService.CreateNewReportDefinition(createNewReportDefinitionRequest);
                    
                    reportDefinitions = service.ListReportDefinitions(request);

                    Assert.IsNotNull(reportDefinitions);
                }

                Assert.IsTrue(reportDefinitions.Count > 0);
            }
        }

        /// <summary>
        /// Tests the retrieve report definition.
        /// </summary>
        [TestMethod]
        public void TestRetrieveReportDefinition()
        {
            var service = Container.Resolve<IReportManagementQueryService>();
            var commandService = Container.Resolve<IReportManagementCommandService>();

            var reportDefinitions = service.ListReportDefinitions(new ListReportDefinitionsRequest());

            Assert.IsNotNull(reportDefinitions);

            if (reportDefinitions.Count == 0)
            {
                var reportTypes = service.ListReportTypes();
                Assert.IsNotNull(reportTypes);

                var reportType = reportTypes.FirstOrDefault();
                Assert.IsNotNull(reportType);

                var request = new CreateNewReportDefinitionRequest
                {
                    Name = "Some report name",
                    Location = "some report location",
                    ReportTypeId = reportType.Id
                };
                commandService.CreateNewReportDefinition(request);

                reportDefinitions = service.ListReportDefinitions(new ListReportDefinitionsRequest());

                Assert.IsNotNull(reportDefinitions);
            }

            Assert.IsTrue(reportDefinitions.Count > 0);
        }

        /// <summary>
        /// Tests the generate report.
        /// </summary>
        [TestMethod]
        public void TestGenerateReport()
        {
            //Unable to perform integration test for this method
        }
    }
}
