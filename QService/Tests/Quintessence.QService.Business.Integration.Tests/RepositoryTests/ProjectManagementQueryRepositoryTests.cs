using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.Integration.Tests.RepositoryTests
{
    [TestClass]
    public class ProjectManagementQueryRepositoryTests : QueryRepositoryTestBase<IProjectManagementQueryRepository>
    {
        [TestMethod]
        public void TestListProjectRolesForQuintessence()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();
            var projectRolesForQuintessence = projectManagementQueryRepository.ListProjectRolesForQuintessence();
            Assert.IsTrue(projectRolesForQuintessence.Count >= 1);

            var firstProjectRole = projectRolesForQuintessence.FirstOrDefault();
            Assert.IsNotNull(firstProjectRole);
            Assert.IsNotNull(firstProjectRole.Name);
        }

        [TestMethod]
        public void TestListProjectRolesForContacts()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();
            var projectRolesForContact = projectManagementQueryRepository.ListProjectRolesForContacts();
            Assert.IsTrue(projectRolesForContact.Count >= 1);

            var firstProjectRole = projectRolesForContact.FirstOrDefault();
            Assert.IsNotNull(firstProjectRole);
            Assert.IsNotNull(firstProjectRole.Name);
        }

        [TestMethod]
        public void TestListProjectRoleDictionaryLevels()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();
            var dictionaryManagementQueryRepository = Container.Resolve<IDictionaryManagementQueryRepository>();

            var projectManagementCommandService = Container.Resolve<IProjectManagementCommandService>();

            var projectRolesForQuintessence = projectManagementQueryRepository.ListProjectRolesForQuintessence();
            Assert.IsTrue(projectRolesForQuintessence.Count >= 1);

            var firstProjectRole = projectRolesForQuintessence.FirstOrDefault(prfq => prfq.Name == "Operationeel");
            Assert.IsNotNull(firstProjectRole);

            var projectRoleDictionaryLevels = projectManagementQueryRepository.ListProjectRoleDictionaryLevels(firstProjectRole.Id, 1);

            //Add dictionary levels if none are found
            if (projectRoleDictionaryLevels.Count == 0)
            {
                var dictionaries = dictionaryManagementQueryRepository.SearchDictionaries("Q WB 2012");
                Assert.IsNotNull(dictionaries);

                var dictionary = dictionaries.FirstOrDefault();
                Assert.IsNotNull(dictionary);

                dictionary = dictionaryManagementQueryRepository.RetrieveDictionaryDetail(dictionary.Id);
                Assert.IsNotNull(dictionary);

                //take 5 competences
                var selectedLevels = dictionary.DictionaryClusters
                    .Select(dc => dc.DictionaryCompetences.FirstOrDefault())
                    .Take(5)
                    .Select(competence => competence.DictionaryLevels.FirstOrDefault().Id)
                    .ToList();

                projectManagementCommandService.LinkProjectRole2DictionaryLevels(firstProjectRole.Id, selectedLevels);

                projectRoleDictionaryLevels = projectManagementQueryRepository.ListProjectRoleDictionaryLevels(firstProjectRole.Id, 1);
            }

            foreach (var projectRoleDictionaryLevelGroup in projectRoleDictionaryLevels.GroupBy(prdl => prdl.DictionaryId))
                Assert.AreEqual(5, projectRoleDictionaryLevelGroup.Select(prdl => prdl.DictionaryLevelId).Distinct().Count());
        }

        [TestMethod]
        public void TestRetrieveProjectRole()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();
            var projectRolesForContact = projectManagementQueryRepository.ListProjectRolesForContacts();
            Assert.IsTrue(projectRolesForContact.Count >= 1);

            var firstProjectRole = projectRolesForContact.FirstOrDefault();
            Assert.IsNotNull(firstProjectRole);

            var retrievedProjectRole = projectManagementQueryRepository.Retrieve<ProjectRoleView>(firstProjectRole.Id);
            Assert.IsNotNull(retrievedProjectRole);
            Assert.AreEqual(firstProjectRole.Id, retrievedProjectRole.Id);
            Assert.AreEqual(firstProjectRole.Name, retrievedProjectRole.Name);
        }

        [TestMethod]
        public void TestListProjectTypes()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();

            var projectTypes = projectManagementQueryRepository.ListProjectTypes();
            Assert.IsNotNull(projectTypes);

            Assert.AreEqual(2, projectTypes.Count);
        }

        [TestMethod]
        public void TestRetrieveProjectDetailAssessmentDevelopment()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();

            const string projectName = "Test ACDC project";
            const string customerName = "DemoCompany";
            const string projectManagerFullName = "Stijn Van Eynde";
            const string customerAssistantFullName = "Thomas King";

            var projectId = CreateNewAssessmentDevelopmentProject(projectName, customerName, projectManagerFullName, customerAssistantFullName);

            Assert.AreNotEqual(Guid.Empty, projectId);

            var project = projectManagementQueryRepository.RetrieveProjectDetail(projectId);

            Assert.IsInstanceOfType(project, typeof(AssessmentDevelopmentProjectView));

            var assessmentDevelopmentProject = project as AssessmentDevelopmentProjectView;

            Assert.IsNotNull(assessmentDevelopmentProject);
            Assert.IsNotNull(assessmentDevelopmentProject.ProjectType);
            Assert.IsNotNull(assessmentDevelopmentProject.Contact);
            Assert.IsNotNull(assessmentDevelopmentProject.CustomerAssistant);
            Assert.IsNotNull(assessmentDevelopmentProject.ProjectManager);
            Assert.IsNotNull(assessmentDevelopmentProject.ProjectCategoryDetails);
        }

        [TestMethod]
        public void TestRetrieveProjectDetailConsultancy()
        {
            var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();

            const string projectName = "Test Consultancy project Project plan";
            const string customerName = "kpmg";
            const string projectManagerFullName = "Stijn Van Eynde";
            const string customerAssistantFullName = "Thomas King";

            var projectId = CreateNewConsultancyProject(projectName, customerName, projectManagerFullName, customerAssistantFullName);

            Assert.AreNotEqual(Guid.Empty, projectId);

            var project = projectManagementQueryRepository.RetrieveProjectDetail(projectId);

            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = project as ConsultancyProjectView;

            Assert.IsNotNull(consultancyProject);
            Assert.IsNotNull(consultancyProject.ProjectType);
            Assert.IsNotNull(consultancyProject.Contact);
            Assert.IsNotNull(consultancyProject.CustomerAssistant);
            Assert.IsNotNull(consultancyProject.ProjectManager);
            Assert.IsNotNull(consultancyProject.ProjectCategoryDetails);
        }

        [TestMethod]
        public void TestListRecentProjects()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectWithCrmProjects()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestSearchCrmProjects()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveAssessmentDevelopmentProjectDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveConsultancyProjectDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListAvailableProjectCategories()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveMainProjectCategoryDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveMainProjectCategoryDetailDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjects()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCategoryDetailDictionaryIndicators()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCategoryDetailSimulationCombinations()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCategoryDetailCompetenceSimulations()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectPlanDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectPlanPhaseDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListRelatedProjectPlanPhaseActivities()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectPlanPhaseEntry()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectPriceIndices()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectTimesheets()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectByProjectPlan()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectFixedPrices()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectProductsheets()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListActiveProjectPlanPhases()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectPlanPhaseProducts()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListSubProjects()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectRolesForQuintessenceAndContact()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectTypeCategory()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidates()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListSubcategories()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveMainProject()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateIndicatorSimulationScores()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateCompetenceSimulationScores()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateIndicatorSimulationFocusedScores()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectCandidate()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectCandidateDetail()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateClusterScores()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateCompetenceScores()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectCandidateResume()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListAdvices()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListUserProjectCandidates()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateDetails()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProposalsByYear()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProposalYears()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProposal()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListProjectCandidateResumes()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveFrameworkAgreement()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListFrameworkAgreementsByYear()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListFrameworkAgreementYears()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveProjectCandidateCategoryDetailType()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListReportRecipientsByProjectCandidateId()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectDocumentMetadatas()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectTypeCategories()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectTypeCategoryLevels()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectTypeCategoryUnitPrices()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectTypeCategoryUnitPricesByCategory()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectCandidatesWithCategoryDetailTypes()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectCandidateCategoryDetailTypes()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListProjectCandidateDetailsForPlanning()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestRetrieveProjectDna()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListDnaTypes()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestRetrieveProjectEvaluationByCrmProject()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestCreateEvaluationFormVerificationCode()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListEvaluationFormsByCrmProject()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListEvaluationFormTypes()
        {
            //TODO: Write test            
        }

        [TestMethod]
        public void TestListMailStatusTypes()
        {
            //TODO: Write test            
        }
        
        [TestMethod]
        public void TestRetrieveReportStatusByCode()
        {
            //TODO: Write test            
        }

        #region Helper methods

        private Guid CreateNewAssessmentDevelopmentProject(string projectName, string customerName, string projectManagerFullName, string customerAssistantFullName)
        {
            var createNewProjectRequest = new CreateNewProjectRequest();
            var projectQueryService = Container.Resolve<IProjectManagementQueryService>();
            var customerRelationshipQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var securityQueryService = Container.Resolve<ISecurityQueryService>();

            var contact = customerRelationshipQueryService.SearchContacts(new SearchContactRequest { CustomerName = customerName }).FirstOrDefault();

            Assert.IsNotNull(contact);

            var crmProject = customerRelationshipQueryService.ListActiveProjects(contact.Id).FirstOrDefault();

            Assert.IsNotNull(crmProject);

            var projectManagerUser = securityQueryService.SearchUser(new SearchUserRequest { Name = projectManagerFullName }).Users.FirstOrDefault();
            var customerAssistantUser = securityQueryService.SearchUser(new SearchUserRequest { Name = customerAssistantFullName }).Users.FirstOrDefault();

            Assert.IsNotNull(projectManagerUser);

            var projectType = projectQueryService.ListProjectTypes().FirstOrDefault(pt => pt.Code == "ACDC");

            Assert.IsNotNull(projectType);

            createNewProjectRequest.ContactId = contact.Id;
            createNewProjectRequest.CrmProjectId = crmProject.Id;
            createNewProjectRequest.ProjectManagerUserId = projectManagerUser.Id;
            createNewProjectRequest.CustomerAssistantUserId = customerAssistantUser.Id;
            createNewProjectRequest.ProjectTypeId = projectType.Id;
            createNewProjectRequest.ProjectName = projectName;

            var commandService = Container.Resolve<IProjectManagementCommandService>();

            return commandService.CreateNewProject(createNewProjectRequest);
        }

        private Guid CreateNewConsultancyProject(string projectName, string customerName, string projectManagerFullName, string customerAssistantFullName)
        {
            var createNewProjectRequest = new CreateNewProjectRequest();
            var projectQueryService = Container.Resolve<IProjectManagementQueryService>();
            var customerRelationshipQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var securityQueryService = Container.Resolve<ISecurityQueryService>();

            var contact = customerRelationshipQueryService.SearchContacts(new SearchContactRequest { CustomerName = customerName }).FirstOrDefault();

            Assert.IsNotNull(contact);

            var crmProject = customerRelationshipQueryService.ListActiveProjects(contact.Id).FirstOrDefault();

            Assert.IsNotNull(crmProject);

            var projectManagerUser = securityQueryService.SearchUser(new SearchUserRequest { Name = projectManagerFullName }).Users.FirstOrDefault();
            var customerAssistantUser = securityQueryService.SearchUser(new SearchUserRequest { Name = projectManagerFullName }).Users.FirstOrDefault();

            Assert.IsNotNull(projectManagerUser);

            var projectType = projectQueryService.ListProjectTypes().FirstOrDefault(pt => pt.Code == "CONS");

            Assert.IsNotNull(projectType);

            createNewProjectRequest.ContactId = contact.Id;
            createNewProjectRequest.CrmProjectId = crmProject.Id;
            createNewProjectRequest.ProjectManagerUserId = projectManagerUser.Id;
            createNewProjectRequest.CustomerAssistantUserId = customerAssistantUser.Id;
            createNewProjectRequest.ProjectTypeId = projectType.Id;
            createNewProjectRequest.ProjectName = projectName;

            var commandService = Container.Resolve<IProjectManagementCommandService>();

            return commandService.CreateNewProject(createNewProjectRequest);
        }

        #endregion
    }
}
