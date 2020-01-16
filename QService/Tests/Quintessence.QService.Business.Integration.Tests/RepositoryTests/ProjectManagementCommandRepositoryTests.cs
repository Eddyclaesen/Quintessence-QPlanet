using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.RepositoryTests
{
    [TestClass]
    public class ProjectManagementCommandRepositoryTests : CommandRepositoryTestBase<IProjectManagementCommandRepository>
    {
        [TestMethod]
        public void TestCreateAssessmentDevelopmentProject()
        {
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test project", "INDAVER", "Stijn Van Eynde", "Thomas King");

            Assert.AreNotEqual(Guid.Empty, projectId);

            var assessmentDevelopmentProjectView = projectManagementQueryService.RetrieveAssessmentDevelopmentProjectDetail(projectId);

            Assert.IsNotNull(assessmentDevelopmentProjectView);
        }

        [TestMethod]
        public void TestPrepareProject() { }

        [TestMethod]
        public void TestSaveProjectCandidateCategoryDetailType() { }

        [TestMethod]
        public void TestLinkProject2CrmProject() { }

        [TestMethod]
        public void TestUnlinkProject2CrmProject() { }

        [TestMethod]
        public void TestRetrieveProject() { }

        [TestMethod]
        public void TestListProject2CrmProject() { }

        [TestMethod]
        public void TestListProjectCategoryDetails() { }

        [TestMethod]
        public void TestPrepareProjectCategoryDetail() { }

        [TestMethod]
        public void TestRetrieveProjectCategoryDetail() { }

        [TestMethod]
        public void TestSaveProject() { }

        [TestMethod]
        public void TestRetrieveProjectTypeCategory() { }

        [TestMethod]
        public void TestRetrieveProjectCategoryDetail2DictionaryIndicator() { }

        [TestMethod]
        public void TestLinkProjectCategoryDetail2DictionaryIndicator() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetail2DictionaryIndicatorById() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetail2DictionaryIndicatorByProjectCategoryDetailIdAndDictionaryIndicatorId() { }

        [TestMethod]
        public void TestRetrieveProjectCategoryDetail2SimulationCombination() { }

        [TestMethod]
        public void TestLinkProjectCategoryDetail2SimulationCombination() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetail2SimulationCombination() { }

        [TestMethod]
        public void TestRetrieveProjectCategoryDetail2Competence2Combination() { }

        [TestMethod]
        public void TestPrepareProjectCategoryDetail2Competence2Combination() { }

        [TestMethod]
        public void TestSaveProjectCategoryDetail() { }

        [TestMethod]
        public void TestDelete() { }

        [TestMethod]
        public void TestUnlinkProjectRole2DictionaryLevel() { }

        [TestMethod]
        public void TestLinkProjectRole2DictionaryLevel() { }

        [TestMethod]
        public void TestListProjectCategoryDetail2Competence2CombinationByCompetence() { }

        [TestMethod]
        public void TestRetrieveProject2Candidate() { }

        [TestMethod]
        public void TestLinkProject2Candidate() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetail2DictionaryLevel() { }

        [TestMethod]
        public void TestUnlinkProject2Candidate() { }

        [TestMethod]
        public void TestLinkSubProject() { }

        [TestMethod]
        public void TestLinkProjectCandidateCategoryDetailType1() { }

        [TestMethod]
        public void TestLinkProjectCandidateCategoryDetailType2() { }

        [TestMethod]
        public void TestLinkProjectCandidateCategoryDetailType3() { }

        [TestMethod]
        public void TestLinkProjectType2ProjectTypeCategory() { }

        [TestMethod]
        public void TestCreateSubcategoryDefaultValues() { }

        [TestMethod]
        public void TestRetrieveProjectCandidateCategoryDetailType() { }

        [TestMethod]
        public void TestSaveProjectCategoryDetail2Competence2Combination() { }

        [TestMethod]
        public void TestLinkProjectRole2DictionaryIndicator() { }

        [TestMethod]
        public void TestUpdateProjectRoleDictionaryIndicatorNorm() { }

        [TestMethod]
        public void TestDeleteProjectRoleDictionaryIndicator() { }

        [TestMethod]
        public void TestDeleteProjectCandidateScores() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetailDictionaryIndicators() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetailCompetenceSimulations() { }

        [TestMethod]
        public void TestDeleteProjectCategoryDetailSimulationCombinations() { }

        [TestMethod]
        public void TestPrepareProposal() { }

        [TestMethod]
        public void TestDeleteProjectDna2ProjectDnaType()
        {
            //TODO: write test
        }

        [TestMethod]
        public void TestRetrieveProjectTypeCategoryUnitPrice()
        {
            //TODO: write test
        }

        [TestMethod]
        public void TestPrepareProjectTypeCategoryUnitPrice()
        {
            //TODO: write test
        }

        [TestMethod]
        public void TestRetrieveProjectEvaluationByCrmProject()
        {
            //TODO: write test
        }
        
        [TestMethod]
        public void TestListProjectDnaProjectTypeDnas()
        {
            //TODO: write test
        }

        [TestMethod]
        public void TestGenerateType3Code()
        {
            //Unable to test because of 3-rd party database
        }

        /// <summary>
        /// Tests the mark document as important.
        /// </summary>
        [TestMethod]
        public void TestMarkDocumentAsImportant()
        {
            var repository = Container.Resolve<IProjectManagementCommandRepository>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var documentManagementQueryService = Container.Resolve<IDocumentManagementQueryService>();

            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test SharePoint Project", "kpmg", "Stijn Van Eynde", "Thomas King");

            var crmProjects = queryService.SearchCrmProjects(new SearchCrmProjectsRequest { ProjectId = projectId, WithRunningStatus = true });

            Assert.IsNotNull(crmProjects);

            foreach (var crmProject in crmProjects)
                repository.LinkProject2CrmProject(projectId, crmProject.CrmProjectId);

            var response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);

            if (response.Documents.Count > 10)
                response.Documents = response.Documents.Take(10).ToList();

            var documents = response.Documents;

            foreach (var document in documents)
                repository.MarkDocumentAsImportant(projectId, document.UniqueId);

            response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(documents.Count, response.Documents.Count(d => d.IsImportant));
        }

        /// <summary>
        /// Tests the unmark document as important.
        /// </summary>
        [TestMethod]
        public void TestUnmarkDocumentAsImportant()
        {
            var repository = Container.Resolve<IProjectManagementCommandRepository>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var documentManagementQueryService = Container.Resolve<IDocumentManagementQueryService>();

            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test SharePoint Project", "kpmg", "Stijn Van Eynde", "Thomas King");

            var crmProjects = queryService.SearchCrmProjects(new SearchCrmProjectsRequest { ProjectId = projectId, WithRunningStatus = true });

            Assert.IsNotNull(crmProjects);

            foreach (var crmProject in crmProjects)
                repository.LinkProject2CrmProject(projectId, crmProject.CrmProjectId);

            var response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);

            if (response.Documents.Count > 10)
                response.Documents = response.Documents.Take(10).ToList();

            var documents = response.Documents;

            foreach (var document in documents)
                repository.MarkDocumentAsImportant(projectId, document.UniqueId);

            response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(documents.Count, response.Documents.Count(d => d.IsImportant));

            foreach (var document in response.Documents.Where(d => d.IsImportant))
                repository.UnmarkDocumentAsImportant(projectId, document.UniqueId);

            response = documentManagementQueryService.ListProjectDocuments(new ListProjectDocumentsRequest { ProjectId = projectId });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Documents);
            Assert.AreEqual(0, response.Documents.Count(d => d.IsImportant));
        }

        #region Helper methods

        public static Guid CreateNewAssessmentDevelopmentProject(IUnityContainer container, string projectName, string customerName, string projectManagerFullName, string customerAssistantFullName)
        {
            var projectManagementCommandRepository = container.Resolve<IProjectManagementCommandRepository>();
            var projectManagementQueryService = container.Resolve<IProjectManagementQueryService>();

            var customerRelationshipManagementQueryService = container.Resolve<ICustomerRelationshipManagementQueryService>();

            var securityManagementQueryService = container.Resolve<ISecurityQueryService>();

            var dictionaryManagementQueryService = container.Resolve<IDictionaryManagementQueryService>();

            var reportManagementQueryService = container.Resolve<IReportManagementQueryService>();

            var projectType = projectManagementQueryService.ListProjectTypes().SingleOrDefault(pt => pt.Code == "ACDC");

            Assert.IsNotNull(projectType);

            var project = projectManagementCommandRepository.PrepareProject(projectType.Id, "Test project");

            Assert.IsNotNull(project);

            var assessmentDevelopmentProject = project as AssessmentDevelopmentProject;

            Assert.IsNotNull(assessmentDevelopmentProject);

            var contact = customerRelationshipManagementQueryService.SearchContacts(new SearchContactRequest { CustomerName = "lafarge" }).FirstOrDefault();

            Assert.IsNotNull(contact);

            var searchUserResponse = securityManagementQueryService.SearchUser(new SearchUserRequest { Name = "Stijn Van Eynde" });

            Assert.IsNotNull(searchUserResponse);
            Assert.IsTrue(searchUserResponse.Users.Count > 0);

            var user = searchUserResponse.Users.FirstOrDefault();
            Assert.IsNotNull(user);

            var dictionary = dictionaryManagementQueryService.ListAvailableDictionaries(contact.Id).FirstOrDefault();

            Assert.IsNotNull(dictionary);

            var candidateScoreReportType = reportManagementQueryService.ListCandidateScoreReportTypes().FirstOrDefault(csrt => csrt.Code == "CO");

            Assert.IsNotNull(candidateScoreReportType);

            assessmentDevelopmentProject.ContactId = contact.Id;
            assessmentDevelopmentProject.ProjectManagerId = user.Id;
            assessmentDevelopmentProject.CustomerAssistantId = user.Id;
            assessmentDevelopmentProject.DictionaryId = dictionary.Id;
            assessmentDevelopmentProject.CandidateScoreReportTypeId = candidateScoreReportType.Id;

            var projectId = projectManagementCommandRepository.Save(assessmentDevelopmentProject);

            Assert.AreNotEqual(Guid.Empty, projectId);

            return projectId;
        }

        #endregion
    }
}