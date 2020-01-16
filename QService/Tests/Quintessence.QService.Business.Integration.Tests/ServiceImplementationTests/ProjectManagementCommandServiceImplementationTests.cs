using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class ProjectManagementServiceCommandImplementationTests : CommandServiceImplementationTestBase<IProjectManagementCommandService>
    {
        /// <summary>
        /// Tests the mark document as important.
        /// </summary>
        [TestMethod]
        public void TestMarkDocumentAsImportant()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var documentManagementQueryService = Container.Resolve<IDocumentManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewAssessmentDevelopmentProject(Container, "Test SharePoint Project", "kpmg", "Stijn Van Eynde", "Thomas King");

            var response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(0, response.Documents.Count(d => d.IsImportant));

            if (response.Documents.Count > 10)
                response.Documents = response.Documents.Take(10).ToList();

            var documents = response.Documents;

            foreach (var document in documents)
                service.MarkDocumentAsImportant(projectId, document.UniqueId);

            response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            var importantDocuments = response.Documents.Where(d => d.IsImportant).ToList();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(documents.Count, importantDocuments.Count);
        }

        /// <summary>
        /// Tests the unmark document as important.
        /// </summary>
        [TestMethod]
        public void TestUnmarkDocumentAsImportant()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var documentManagementQueryService = Container.Resolve<IDocumentManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewAssessmentDevelopmentProject(Container, "Test SharePoint Project", "kpmg", "Stijn Van Eynde", "Thomas King");

            var response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);

            if (response.Documents.Count > 10)
                response.Documents = response.Documents.Take(10).ToList();

            var documents = response.Documents;

            foreach (var document in documents)
                service.MarkDocumentAsImportant(projectId, document.UniqueId);

            response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(documents.Count, response.Documents.Count(d => d.IsImportant));

            foreach (var document in response.Documents.Where(d => d.IsImportant))
                service.UnmarkDocumentAsImportant(projectId, document.UniqueId);

            response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(0, response.Documents.Count(d => d.IsImportant));
        }

        [TestMethod]
        public void TestCreateNewProject() { }

        [TestMethod]
        public void TestLinkProject2CrmProject() { }

        [TestMethod]
        public void TestUnlinkProject2CrmProject() { }

        [TestMethod]
        public void TestUpdateAssessmentDevelopmentProject() { }

        [TestMethod]
        public void TestUpdateConsultancyProject() { }

        [TestMethod]
        public void TestLinkProject2Candidate() { }

        [TestMethod]
        public void TestUnlinkProject2Candidate() { }

        [TestMethod]
        public void TestLinkProjectCategoryDetail2DictionaryIndicators() { }

        [TestMethod]
        public void TestUpdateProjectRole() { }

        [TestMethod]
        public void TestUnlinkProjectCategoryDetail2DictionaryIndicator() { }

        [TestMethod]
        public void TestLinkProjectCategoryDetail2SimulationCombinations() { }

        [TestMethod]
        public void TestUnlinkProjectCategoryDetail2Combination() { }

        [TestMethod]
        public void TestLinkProjectCategoryDetail2Competence2Combination() { }

        [TestMethod]
        public void TestUnlinkProjectCategoryDetail2Competence2Combination() { }

        [TestMethod]
        public void TestCreateProjectRole() { }

        [TestMethod]
        public void TestUnlinkProjectRoleDictionaryLevel() { }

        [TestMethod]
        public void TestLinkProjectRole2DictionaryLevels() { }

        [TestMethod]
        public void TestUpdateProjectCategoryDetailSimulationRemarks() { }

        [TestMethod]
        public void TestUpdateProjectCategoryDetailMatrixRemarks() { }

        [TestMethod]
        public void TestAssignProjectRole() { }

        [TestMethod]
        public void TestUnlinkProjectCategoryDetail2DictionaryLevel() { }

        [TestMethod]
        public void TestCopyProjectRole() { }

        [TestMethod]
        public void TestCreateNewProjectPlanPhase() { }

        [TestMethod]
        public void TestCreateNewProjectPlanPhaseEntry() { }

        [TestMethod]
        public void TestUpdateProjectPlanPhaseEntry() { }

        [TestMethod]
        public void TestDeleteProjectPlanPhaseEntry() { }

        [TestMethod]
        public void TestUpdateProjectPlanPhaseEntryDeadline() { }

        [TestMethod]
        public void TestCreateNewProjectPriceIndex() { }

        [TestMethod]
        public void TestUpdateProjectPriceIndex() { }

        [TestMethod]
        public void TestDeleteProjectPriceIndex() { }

        [TestMethod]
        public void TestSaveTimesheetEntries() { }

        [TestMethod]
        public void TestDeleteTimesheetEntry() { }

        [TestMethod]
        public void TestCreateNewProjectFixedPrice() { }

        [TestMethod]
        public void TestUpdateProjectFixedPrice() { }

        [TestMethod]
        public void TestDeleteProjectFixedPrice() { }

        [TestMethod]
        public void TestSaveProductsheetEntries() { }

        [TestMethod]
        public void TestDeleteProductsheetEntry() { }

        [TestMethod]
        public void TestUnassignProjectRole() { }

        [TestMethod]
        public void TestAddProjectCandidate() { }

        [TestMethod]
        public void TestDeleteProjectCandidate() { }

        [TestMethod]
        public void TestUpdateProjectCategoryDetailType1() { }

        [TestMethod]
        public void TestUpdateProjectCategoryDetailType2() { }

        [TestMethod]
        public void TestUpdateProjectCategoryDetailType3() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateCategoryDetailType() { }

        [TestMethod]
        public void TestCreateSubcategory() { }

        [TestMethod]
        public void TestUpdateSubcategory() { }

        [TestMethod]
        public void TestUpdateProjectCandidateCategoryDetailTypes() { }

        [TestMethod]
        public void TestLinkCandidateReportDefinition2Project() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateIndicatorSimulationScore() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateCompetenceSimulationScore() { }

        [TestMethod]
        public void TestUpdateProjectCandidateCompetenceSimulationScore() { }

        [TestMethod]
        public void TestMarkDictionaryIndicatorAsStandard() { }

        [TestMethod]
        public void TestMarkDictionaryIndicatorAsDistinctive() { }

        [TestMethod]
        public void TestDeleteProjectRoleDictionaryIndicator() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateClusterScore() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateCompetenceScore() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateIndicatorScore() { }

        [TestMethod]
        public void TestUpdateProjectCandidateCompetenceScores() { }

        [TestMethod]
        public void TestUpdateProjectCandidateClusterScores() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateResume() { }

        [TestMethod]
        public void TestUpdateProjectCandidateResume() { }

        [TestMethod]
        public void TestCreateNewProjectCandidateResumeField() { }

        [TestMethod]
        public void TestCreateNewProposal() { }

        [TestMethod]
        public void TestUpdateProposal() { }

        [TestMethod]
        public void TestCreateFrameworkAgreement() { }

        [TestMethod]
        public void TestUpdateFrameworkAgreement() { }

        [TestMethod]
        public void TestDeleteFrameworkAgreement() { }

        [TestMethod]
        public void TestUpdateProjectCandidatesDetails() { }

        [TestMethod]
        public void TestCreateProjectCandidateReportRecipient() { }

        [TestMethod]
        public void TestDeleteProjectCandidateReportRecipient() { }

        [TestMethod]
        public void TestUpdateProjectCandidateReportRecipient() { }

        [TestMethod]
        public void TestCreateProjectCandidateReportRecipients() { }

        [TestMethod]
        public void TestCreateProjectProduct()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestDeleteProjectProduct()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestCreateProjectEvaluation()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProjectEvaluation()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestCreateEvaluationForm()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProjectReporting()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestCancelProjectCandidate()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateCancelledProjectCandidates()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProjectCategoryDetailUnitPrices()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestCreateProjectTypeCategoryUnitPrice()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProjectTypeCategoryUnitPrice()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestDeleteProjectTypeCategoryUnitPrice()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestCreateNewProjectDna()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProjectDna()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProjectTypeCategoryUnitPrices()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateInvoicing()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestCreateProjectProducts()
        {
            //TODO: Write test
        }
    }
}
