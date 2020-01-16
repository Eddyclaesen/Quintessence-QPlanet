using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.RepositoryTests
{
    [TestClass]
    public class ReportManagementQueryRepositoryTests : QueryRepositoryTestBase<IReportManagementQueryRepository>
    {
        /// <summary>
        /// Tests the list candidate report definitions.
        /// </summary>
        [TestMethod]
        public void TestListCandidateReportDefinitions()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();

            var reportDefinitions = repository.ListCandidateReportDefinitions();

            Assert.IsNotNull(reportDefinitions);
        }

        /// <summary>
        /// Tests the list candidate report definitions for customer.
        /// </summary>
        [TestMethod]
        public void TestListCandidateReportDefinitionsForCustomer()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();
            var customerRelationshipManagementQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            var contacts = customerRelationshipManagementQueryService.ListContacts();

            Assert.IsNotNull(contacts);

            var contact = contacts.FirstOrDefault();

            Assert.IsNotNull(contact);

            var reportDefinitions = repository.ListCandidateReportDefinitionsForCustomer(contact.Id);

            Assert.IsNotNull(reportDefinitions);
        }

        /// <summary>
        /// Tests the list candidate score report types.
        /// </summary>
        [TestMethod]
        public void TestListCandidateScoreReportTypes()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();

            var scoreTypes = repository.ListCandidateScoreReportTypes();

            Assert.IsNotNull(scoreTypes);
            Assert.AreEqual(2, scoreTypes.Count);
        }

        /// <summary>
        /// Tests the retrieve candidate report definition.
        /// </summary>
        [TestMethod]
        public void TestRetrieveCandidateReportDefinition()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();
            var customerRelationshipManagementQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var reportManagementCommandService = Container.Resolve<IReportManagementCommandService>();

            var contacts = customerRelationshipManagementQueryService.ListContacts();

            Assert.IsNotNull(contacts);

            var contact = contacts.FirstOrDefault();

            Assert.IsNotNull(contact);

            const string location = "/Some/Location";
            const string name = "SomeReport";
            var request = new CreateNewCandidateReportDefinitionRequest
                {
                    ContactId = contact.Id,
                    Location = location,
                    Name = name
                };

            var candidateReportDefinitionId = reportManagementCommandService.CreateNewCandidateReportDefinition(request);

            Assert.AreNotEqual(Guid.Empty, candidateReportDefinitionId);

            var candidateReportDefinition = repository.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);
            Assert.AreEqual(candidateReportDefinitionId, candidateReportDefinition.Id);
            Assert.IsNotNull(candidateReportDefinition.ContactId);
            Assert.AreEqual(contact.Id, candidateReportDefinition.ContactId.Value);
            Assert.AreEqual(location, candidateReportDefinition.Location);
            Assert.AreEqual(name, candidateReportDefinition.Name);
        }

        /// <summary>
        /// Tests the list report types.
        /// </summary>
        [TestMethod]
        public void TestListReportTypes()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();

            var reportTypes = repository.ListReportTypes();

            Assert.IsNotNull(reportTypes);
            Assert.IsTrue(reportTypes.Count > 0);
        }

        /// <summary>
        /// Tests the type of the list report definitions by report.
        /// </summary>
        [TestMethod]
        public void TestListReportDefinitionsByReportType()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();
            var commandService = Container.Resolve<IReportManagementCommandService>();

            var reportTypes = repository.ListReportTypes();

            foreach (var reportType in reportTypes)
            {
                var reportDefinitions = repository.ListReportDefinitionsByReportType(reportType.Id);

                Assert.IsNotNull(reportDefinitions);

                if (reportDefinitions.Count == 0)
                {
                    var request = new CreateNewReportDefinitionRequest
                        {
                            Name = "Some report name",
                            Location = "some report location",
                            ReportTypeId = reportType.Id
                        };
                    commandService.CreateNewReportDefinition(request);

                    reportDefinitions = repository.ListReportDefinitionsByReportType(reportType.Id);

                    Assert.IsNotNull(reportDefinitions);
                }

                Assert.IsTrue(reportDefinitions.Count > 0);
            }
        }

        /// <summary>
        /// Tests the list report definitions.
        /// </summary>
        [TestMethod]
        public void TestListReportDefinitions()
        {
            var repository = Container.Resolve<IReportManagementQueryRepository>();
            var commandService = Container.Resolve<IReportManagementCommandService>();

            var reportDefinitions = repository.ListReportDefinitions();

            Assert.IsNotNull(reportDefinitions);

            if (reportDefinitions.Count == 0)
            {

                var reportTypes = repository.ListReportTypes();
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

                reportDefinitions = repository.ListReportDefinitions();

                Assert.IsNotNull(reportDefinitions);
            }

            Assert.IsTrue(reportDefinitions.Count > 0);
        }
    }
}
