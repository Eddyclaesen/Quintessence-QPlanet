using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
    public class ReportManagementCommandServiceImplementationTests : CommandServiceImplementationTestBase<IReportManagementCommandService>
    {
        /// <summary>
        /// Tests the create new candidate report definition.
        /// </summary>
        [TestMethod]
        public void TestCreateNewCandidateReportDefinition()
        {
            var queryService = Container.Resolve<IReportManagementQueryService>();

            const string name = "Some weird name";
            const string location = "/Some/Weird/Location";

            var candidateReportDefinitionId = CreateNewCandidateReportDefinition(Container, name, location);

            Assert.AreNotEqual(Guid.Empty, candidateReportDefinitionId);

            var candidateReportDefinition = queryService.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);
            Assert.AreEqual(candidateReportDefinitionId, candidateReportDefinition.Id);
            Assert.AreEqual(location, candidateReportDefinition.Location);
            Assert.AreEqual(name, candidateReportDefinition.Name);
        }

        /// <summary>
        /// Tests the update candidate report definition.
        /// </summary>
        [TestMethod]
        public void TestUpdateCandidateReportDefinition()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            const string name = "Some weird name";
            const string updatedName = "Some other weird name";
            const string location = "/Some/Weird/Location";
            const string updatedLocation = "/Some/Other/Weird/Location";

            var candidateReportDefinitionId = CreateNewCandidateReportDefinition(Container, name, location);

            Assert.AreNotEqual(Guid.Empty, candidateReportDefinitionId);

            var candidateReportDefinition = queryService.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);

            Assert.AreNotEqual(updatedName, candidateReportDefinition.Name);
            Assert.AreNotEqual(updatedLocation, candidateReportDefinition.Location);

            var request = Mapper.DynamicMap<UpdateCandidateReportDefinitionRequest>(candidateReportDefinition);
            request.Name = updatedName;
            request.Location = updatedLocation;

            service.UpdateCandidateReportDefinition(request);

            candidateReportDefinition = queryService.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);

            Assert.AreEqual(updatedName, candidateReportDefinition.Name);
            Assert.AreEqual(updatedLocation, candidateReportDefinition.Location);
        }

        /// <summary>
        /// Tests the delete candidate report definition.
        /// </summary>
        [TestMethod]
        public void TestDeleteCandidateReportDefinition()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            var candidateReportDefinitionId = CreateNewCandidateReportDefinition(Container, "Some weird name", "/Some/Weird/Location");

            var candidateReportDefinition = queryService.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);

            service.DeleteCandidateReportDefinition(candidateReportDefinitionId);

            candidateReportDefinition = queryService.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNull(candidateReportDefinition);
        }

        /// <summary>
        /// Tests the create new candidate report definition field.
        /// </summary>
        [TestMethod]
        public void TestCreateNewCandidateReportDefinitionField()
        {
            var queryService = Container.Resolve<IReportManagementQueryService>();

            const string code = "CODE";
            const string name = "Code name";

            var candidateReportDefinitionFieldId = CreateNewCandidateReportDefinitionField(Container, name, code);

            Assert.AreNotEqual(Guid.Empty, candidateReportDefinitionFieldId);

            var candidateReportDefinitionField = queryService.RetrieveCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            Assert.IsNotNull(candidateReportDefinitionField);
            Assert.AreEqual(candidateReportDefinitionFieldId, candidateReportDefinitionField.Id);
            Assert.AreEqual(code, candidateReportDefinitionField.Code);
            Assert.AreEqual(name, candidateReportDefinitionField.Name);
        }

        /// <summary>
        /// Tests the update candidate report definition field.
        /// </summary>
        [TestMethod]
        public void TestUpdateCandidateReportDefinitionField()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            const string updatedCode = "UPDATECODE";
            const string updatedName = "Updated field name";

            var candidateReportDefinitionFieldId = CreateNewCandidateReportDefinitionField(Container, "Some field name", "Some field Code");

            Assert.AreNotEqual(Guid.Empty, candidateReportDefinitionFieldId);

            var candidateReportDefinitionField = queryService.RetrieveCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            Assert.IsNotNull(candidateReportDefinitionField);

            Assert.AreNotEqual(updatedCode, candidateReportDefinitionField.Code);
            Assert.AreNotEqual(updatedName, candidateReportDefinitionField.Name);

            var request = Mapper.DynamicMap<UpdateCandidateReportDefinitionFieldRequest>(candidateReportDefinitionField);
            request.Name = updatedName;
            request.Code = updatedCode;

            service.UpdateCandidateReportDefinitionField(request);

            candidateReportDefinitionField = queryService.RetrieveCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            Assert.IsNotNull(candidateReportDefinitionField);

            Assert.AreEqual(updatedCode, candidateReportDefinitionField.Code);
            Assert.AreEqual(updatedName, candidateReportDefinitionField.Name);
        }

        /// <summary>
        /// Tests the delete candidate report definition field.
        /// </summary>
        [TestMethod]
        public void TestDeleteCandidateReportDefinitionField()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            var candidateReportDefinitionFieldId = CreateNewCandidateReportDefinitionField(Container, "Some field name", "Some field Code");

            Assert.AreNotEqual(Guid.Empty, candidateReportDefinitionFieldId);

            var candidateReportDefinitionField = queryService.RetrieveCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            Assert.IsNotNull(candidateReportDefinitionField);

            service.DeleteCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            candidateReportDefinitionField = queryService.RetrieveCandidateReportDefinitionField(candidateReportDefinitionFieldId);

            Assert.IsNull(candidateReportDefinitionField);
        }

        /// <summary>
        /// Tests the create new report definition.
        /// </summary>
        [TestMethod]
        public void TestCreateNewReportDefinition()
        {
            var queryService = Container.Resolve<IReportManagementQueryService>();

            const string name = "Code name";
            const string location = "/Some/Location";

            var reportDefinitionId = CreateNewReportDefinition(Container, name, location, exportAsPdf: true);

            Assert.AreNotEqual(Guid.Empty, reportDefinitionId);

            var reportDefinition = queryService.RetrieveReportDefinition(reportDefinitionId);

            Assert.IsNotNull(reportDefinition);
            Assert.AreEqual(name, reportDefinition.Name);
            Assert.AreEqual(location, reportDefinition.Location);
            Assert.IsTrue(reportDefinition.IsActive);
            Assert.IsTrue(reportDefinition.ExportAsPdf);
            Assert.IsFalse(reportDefinition.ExportAsWord);
        }

        /// <summary>
        /// Tests the update report definition.
        /// </summary>
        [TestMethod]
        public void TestUpdateReportDefinition()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            const string updatedName = "Code name";
            const string updatedLocation = "/Some/Location";

            var reportDefinitionId = CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true);

            Assert.AreNotEqual(Guid.Empty, reportDefinitionId);

            var reportDefinition = queryService.RetrieveReportDefinition(reportDefinitionId);

            Assert.AreNotEqual(updatedName, reportDefinition.Name);
            Assert.AreNotEqual(updatedLocation, reportDefinition.Location);

            var request = Mapper.DynamicMap<UpdateReportDefinitionRequest>(reportDefinition);

            request.Name = updatedName;
            request.Location = updatedLocation;
            request.ExportAsWord = true;

            service.UpdateReportDefinition(request);

            reportDefinition = queryService.RetrieveReportDefinition(reportDefinitionId);

            Assert.AreEqual(updatedName, reportDefinition.Name);
            Assert.AreEqual(updatedLocation, reportDefinition.Location);
            Assert.IsTrue(request.ExportAsWord);
            Assert.IsTrue(request.ExportAsPdf);
            Assert.IsFalse(request.ExportAsExcel);
        }

        /// <summary>
        /// Tests the update report definitions.
        /// </summary>
        [TestMethod]
        public void TestUpdateReportDefinitions()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            var reportDefinitionIds = new List<Guid>();
            reportDefinitionIds.Add(CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true));
            reportDefinitionIds.Add(CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true));
            reportDefinitionIds.Add(CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true));
            reportDefinitionIds.Add(CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true));
            reportDefinitionIds.Add(CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true));

            Assert.AreEqual(5, reportDefinitionIds.Count);

            var reportDefinitions = queryService.ListReportDefinitions(new ListReportDefinitionsRequest());

            var requests = reportDefinitions
                .Where(rd => reportDefinitionIds.Contains(rd.Id))
                .Select(Mapper.DynamicMap<UpdateReportDefinitionRequest>)
                .ToList();

            Assert.AreEqual(5, requests.Count);

            requests.ForEach(r => r.ExportAsWord = true);

            service.UpdateReportDefinitions(requests);

            reportDefinitions = queryService.ListReportDefinitions(new ListReportDefinitionsRequest());

            var updatedReports = reportDefinitions
                .Where(rd => reportDefinitionIds.Contains(rd.Id))
                .ToList();

            Assert.AreEqual(5, updatedReports.Count);
            Assert.IsTrue(updatedReports.All(r => r.ExportAsWord));
        }

        /// <summary>
        /// Tests the delete report definition.
        /// </summary>
        [TestMethod]
        public void TestDeleteReportDefinition()
        {
            var service = Container.Resolve<IReportManagementCommandService>();
            var queryService = Container.Resolve<IReportManagementQueryService>();

            var reportDefinitionId = CreateNewReportDefinition(Container, "Name", "/Location", exportAsPdf: true);

            Assert.AreNotEqual(Guid.Empty, reportDefinitionId);

            var reportDefinition = queryService.RetrieveReportDefinition(reportDefinitionId);

            Assert.IsNotNull(reportDefinition);

            service.DeleteReportDefinition(reportDefinitionId);

            reportDefinition = queryService.RetrieveReportDefinition(reportDefinitionId);

            Assert.IsNull(reportDefinition);
        }

        #region HelperMethods
        /// <summary>
        /// Creates the new candidate report definition.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public static Guid CreateNewCandidateReportDefinition(IUnityContainer container, string name, string location)
        {
            var service = container.Resolve<IReportManagementCommandService>();
            var customerRelationshipQueryService = container.Resolve<ICustomerRelationshipManagementQueryService>();

            var contacts = customerRelationshipQueryService.ListContacts();

            Assert.IsNotNull(contacts);

            var contact = contacts.FirstOrDefault();

            Assert.IsNotNull(contact);

            var request = new CreateNewCandidateReportDefinitionRequest
            {
                ContactId = contact.Id,
                Location = location,
                Name = name
            };

            return service.CreateNewCandidateReportDefinition(request);
        }

        /// <summary>
        /// Creates the new candidate report definition field.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static Guid CreateNewCandidateReportDefinitionField(IUnityContainer container, string name, string code)
        {
            var service = container.Resolve<IReportManagementCommandService>();
            var queryService = container.Resolve<IReportManagementQueryService>();

            var candidateReportDefinitionId = CreateNewCandidateReportDefinition(container, "Some weird name", "/Some/Weird/Location");

            var candidateReportDefinition = queryService.RetrieveCandidateReportDefinition(candidateReportDefinitionId);

            Assert.IsNotNull(candidateReportDefinition);

            var request = new CreateNewCandidateReportDefinitionFieldRequest
            {
                CandidateReportDefinitionId = candidateReportDefinitionId,
                Code = code,
                Name = name
            };

            return service.CreateNewCandidateReportDefinitionField(request);
        }

        /// <summary>
        /// Creates the new report definition.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <param name="location">The location.</param>
        /// <param name="exportAsXml">if set to <c>true</c> [export as XML].</param>
        /// <param name="exportAsCsvl">if set to <c>true</c> [export as CSVL].</param>
        /// <param name="exportAsImg">if set to <c>true</c> [export as img].</param>
        /// <param name="exportAsPdf">if set to <c>true</c> [export as PDF].</param>
        /// <param name="exportAsMhtml">if set to <c>true</c> [export as MHTML].</param>
        /// <param name="exportAsHtml4">if set to <c>true</c> [export as HTML4].</param>
        /// <param name="exportAsHtml32">if set to <c>true</c> [export as HTML32].</param>
        /// <param name="exportAsExcel">if set to <c>true</c> [export as excel].</param>
        /// <param name="exportAsWord">if set to <c>true</c> [export as word].</param>
        /// <returns></returns>
        public static Guid CreateNewReportDefinition(IUnityContainer container, string name, string location,
                                                    bool exportAsXml = false, bool exportAsCsvl = false, bool exportAsImg = false, bool exportAsPdf = false,
                                                    bool exportAsMhtml = false, bool exportAsHtml4 = false, bool exportAsHtml32 = false, bool exportAsExcel = false, bool exportAsWord = false)
        {
            var service = container.Resolve<IReportManagementCommandService>();
            var queryService = container.Resolve<IReportManagementQueryService>();

            var reportTypes = queryService.ListReportTypes();

            Assert.IsNotNull(reportTypes);

            var reportType = reportTypes.FirstOrDefault(rt => rt.Code.Equals("project", StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(reportType, "Unable to retrieve ReportTypeId with code 'PROJECT'.");

            var request = new CreateNewReportDefinitionRequest
                {
                    ReportTypeId = reportType.Id,
                    Name = name,
                    Location = location,
                    ExportAsXml = exportAsXml,
                    ExportAsCsvl = exportAsCsvl,
                    ExportAsImg = exportAsImg,
                    ExportAsPdf = exportAsPdf,
                    ExportAsMhtml = exportAsMhtml,
                    ExportAsHtml4 = exportAsHtml4,
                    ExportAsHtml32 = exportAsHtml32,
                    ExportAsExcel = exportAsExcel,
                    ExportAsWord = exportAsWord
                };
            var reportDefinitionId = service.CreateNewReportDefinition(request);

            return reportDefinitionId;
        }
        #endregion
    }
}
