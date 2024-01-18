using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Core.Security;
using Quintessence.QService.Core.Testing;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class ProjectManagementServiceImplementationTests : ServiceImplementationTestBase
    {
        /// <summary>
        /// Test for creating a new Assessment & Development project.
        /// </summary>
        [TestMethod]
        public void TestCreateAssessmentDevelopmentProject()
        {
            try
            {
                const string projectName = "Test ACDC project";
                const string customerName = "DemoCompany";
                const string projectManagerFullName = "Stijn Van Eynde";
                const string customerAssistantFullName = "Thomas King";

                var projectId = CreateNewAssessmentDevelopmentProject(Container, projectName, customerName, projectManagerFullName, customerAssistantFullName);

                Assert.AreNotEqual(Guid.Empty, projectId);

                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();
                var project = projectManagementQueryService.RetrieveProjectDetail(projectId);

                Assert.IsInstanceOfType(project, typeof(AssessmentDevelopmentProjectView));

                var acdcProject = (AssessmentDevelopmentProjectView)project;

                Assert.AreEqual(projectName, acdcProject.Name);
                Assert.AreEqual(customerName, acdcProject.Contact.FullName);
                Assert.AreEqual(projectManagerFullName.ToLowerInvariant(), acdcProject.ProjectManagerFullName.ToLowerInvariant());
                Assert.AreEqual(customerAssistantFullName.ToLowerInvariant(), acdcProject.CustomerAssistantFullName.ToLowerInvariant());
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        /// <summary>
        /// Tests for creating a simple consultancy project.
        /// </summary>
        [TestMethod]
        public void TestCreateConsultancyProject()
        {
            try
            {
                const string projectName = "Test Consultancy project";
                const string customerName = "DemoCompany";
                const string projectManagerFullName = "Stijn Van Eynde";
                const string customerAssistantFullName = "Thomas King";

                var projectId = CreateNewConsultancyProject(Container, projectName, customerName, projectManagerFullName, customerAssistantFullName);

                Assert.AreNotEqual(Guid.Empty, projectId);

                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();
                var project = projectManagementQueryService.RetrieveProjectDetail(projectId);

                Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

                Assert.AreNotEqual(Guid.Empty, ((ConsultancyProjectView)project).ProjectPlanId);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        /// <summary>
        /// Tests the update of an assessment and development project
        /// </summary>
        [TestMethod]
        public void TestUpdateAssessmentDevelopmentProject()
        {
            try
            {
                const string projectName = "Test Consultancy project";
                const string customerName = "DemoCompany";
                const string projectManagerFullName = "Stijn Van Eynde";
                const string customerAssistantFullName = "Thomas King";

                var projectId = CreateNewAssessmentDevelopmentProject(Container, projectName, customerName, projectManagerFullName, customerAssistantFullName);

                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                var project = projectManagementQueryService.RetrieveProjectDetail(projectId);
                Assert.IsInstanceOfType(project, typeof(AssessmentDevelopmentProjectView));

                var acdcProject = (AssessmentDevelopmentProjectView)project;

                Assert.IsNull(acdcProject.DictionaryId);
                Assert.IsNull(acdcProject.MainProjectCategoryDetail);
                Assert.IsFalse(acdcProject.HasSubProjectCategoryDetails);

                var updateProject = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(acdcProject);
                Assert.AreEqual(acdcProject.Id, updateProject.Id);

                Console.WriteLine("Assign a dictionary (Q WB 2012) to the project");
                var dictionaryManagementQueryService = Container.Resolve<IDictionaryManagementQueryService>();

                var dictionaries = dictionaryManagementQueryService.ListQuintessenceDictionaries();
                Assert.IsNotNull(dictionaries);
                Assert.IsTrue(dictionaries.Count > 0);

                var dictionary = dictionaries.FirstOrDefault(d => d.Current || d.Name == "Q WB 2012");
                Assert.IsNotNull(dictionary);

                updateProject.DictionaryId = dictionary.Id;

                Console.WriteLine("Assign a main category (AC) to the project");
                var projectTypeCategories = projectManagementQueryService.ListAvailableProjectCategories(acdcProject.ProjectTypeId);
                var acProjectTypeCategory = projectTypeCategories.SingleOrDefault(ptc => ptc.IsMain && ptc.Code == "AC");
                Assert.IsNotNull(acProjectTypeCategory);

                updateProject.ProjectTypeCategoryId = acProjectTypeCategory.Id;

                Console.WriteLine("Assign 3 subcategories to the project");
                foreach (var projectTypeSubCategory in projectTypeCategories.Where(ptc => !ptc.IsMain).Take(3))
                    updateProject.SelectedProjectTypeCategoryIds.Add(projectTypeSubCategory.Id);

                var projectManagementCommandService = Container.Resolve<IProjectManagementCommandService>();
                projectManagementCommandService.UpdateAssessmentDevelopmentProject(updateProject);

                project = projectManagementQueryService.RetrieveProjectDetail(projectId);
                Assert.IsInstanceOfType(project, typeof(AssessmentDevelopmentProjectView));

                acdcProject = (AssessmentDevelopmentProjectView)project;

                Assert.AreEqual(dictionary.Id, acdcProject.DictionaryId);
                Assert.AreEqual(acProjectTypeCategory.Id, acdcProject.MainProjectCategoryDetail.ProjectTypeCategoryId);
                Assert.IsTrue(acdcProject.HasSubProjectCategoryDetails);
                Assert.AreEqual(3, acdcProject.ProjectCategoryDetails.Count(pcd => !pcd.ProjectTypeCategory.IsMain));
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        /// <summary>
        /// Tests the update of an consultancy project
        /// </summary>
        [TestMethod]
        public void TestUpdateConsultancyProject()
        {
            try
            {
                const string projectName = "Test Consultancy project";
                const string customerName = "DemoCompany";
                const string projectManagerFullName = "Stijn Van Eynde";
                const string customerAssistantFullName = "Thomas King";

                var projectId = CreateNewConsultancyProject(Container, projectName, customerName, projectManagerFullName, customerAssistantFullName);

                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                var project = projectManagementQueryService.RetrieveProjectDetail(projectId);
                Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

                var consultancyProject = (ConsultancyProjectView)project;

                const string functionInformation = "Function Information";
                const string departmentInformation = "Department Information";
                const string remarks = "Remarks";

                var updateProject = Mapper.DynamicMap<UpdateConsultancyProjectRequest>(consultancyProject);

                Console.WriteLine("Update fields");
                updateProject.ProjectInformation = functionInformation;
                updateProject.DepartmentInformation = departmentInformation;
                updateProject.Remarks = remarks;

                var projectManagementCommandService = Container.Resolve<IProjectManagementCommandService>();

                projectManagementCommandService.UpdateConsultancyProject(updateProject);

                project = projectManagementQueryService.RetrieveProjectDetail(projectId);
                Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

                consultancyProject = (ConsultancyProjectView)project;

                Assert.AreEqual(functionInformation, consultancyProject.ProjectInformation);
                Assert.AreEqual(departmentInformation, consultancyProject.DepartmentInformation);
                Assert.AreEqual(remarks, consultancyProject.Remarks);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void TestAddProjectCandidate()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var service = Container.Resolve<IProjectManagementCommandService>();

            //Create project to be sure there is at least one in the database
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test project", "Democompany", "thomas king", "thomas king");
            var project = queryService.RetrieveProject(projectId);

            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId).Where(ptc => ptc.IsMain);
            var projectTypeCategory = projectTypeCategories.FirstOrDefault(ptc => ptc.Code == "AC");

            var updateProject = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(project);
            updateProject.SelectedProjectTypeCategoryIds.Add(projectTypeCategory.Id);

            service.UpdateAssessmentDevelopmentProject(updateProject);

            //Create candidate linked to that project
            var request = new AddProjectCandidateRequest
                {
                    AppointmentId = 1000,
                    Code = "123456",
                    FirstName = "Sheldon",
                    LastName = "Cooper",
                    LanguageId = 1,
                    Gender = GenderType.M.ToString(),
                    ProjectId = project.Id
                };
            var projectCandidateId = service.AddProjectCandidate(request);

            //Retrieve added candidate
            var projectCandidate = queryService.RetrieveProjectCandidate(projectCandidateId);
            Assert.IsNotNull(projectCandidate);
        }

        //[TestMethod]
        public void TestCrudProject2Candidates()
        {
            //Create new project
            TestCreateAssessmentDevelopmentProject();

            //List projects
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();
            var listProjectsResponse = projectManagementQueryService.ListProjects(new ListProjectsRequest());
            var projects = listProjectsResponse.Projects;


            //List candidates
            var candidateManagementQueryService = Container.Resolve<ICandidateManagementQueryService>();
            var candidates = candidateManagementQueryService.ListCandidates();

            if (candidates.Count >= 1)
            {
                //first project from projects
                var project = projects.FirstOrDefault();
                Assert.IsNotNull(project);

                //first candidate from candidates
                var candidate = candidates.FirstOrDefault();
                Assert.IsNotNull(candidate);

                //Link project 2 candidate
                var projectManagementCommandService = Container.Resolve<IProjectManagementCommandService>();
                projectManagementCommandService.LinkProject2Candidate(project.Id, candidate.Id);

                //List projectCandidates by candidate id
                var request = new ListProjectCandidateDetailsRequest { CandidateId = candidate.Id };
                var projectCandidatesByCandidateId = projectManagementQueryService.ListProjectCandidateDetails(request);
                Assert.IsTrue(projectCandidatesByCandidateId.Count >= 1);

                //List projectCandidates by project id
                var listProjectCandidatesResponse = projectManagementQueryService.ListProjectCandidateDetailsByProjectId(project.Id);
                Assert.IsTrue(listProjectCandidatesResponse.Candidates.Count >= 1);
                Assert.IsNotNull(listProjectCandidatesResponse.Project);

                //Unlink project 2 candidate
                projectManagementCommandService.UnlinkProject2Candidate(project.Id, candidate.Id);
            }
            else
            {
                Assert.Fail("No candidates found...");
            }
        }

        /// <summary>
        /// Tests the create project plan phase.
        /// </summary>
        [TestMethod]
        public void TestCreateProjectPlanPhase()
        {
            var prmCommandService = Container.Resolve<IProjectManagementCommandService>();
            var scmCommandService = Container.Resolve<ISupplyChainManagementCommandService>();

            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            const string projectName = "Test Consultancy project Project plan";
            const string customerName = "kpmg";
            const string projectManagerFullName = "Stijn Van Eynde";
            const string customerAssistantFullName = "Thomas King";

            var projectId = CreateNewConsultancyProject(Container, projectName, customerName, projectManagerFullName, customerAssistantFullName);

            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var project = projectManagementQueryService.RetrieveProjectDetail(projectId);
            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = (ConsultancyProjectView)project;

            Assert.AreNotEqual(Guid.Empty, consultancyProject.ProjectPlanId);

            var activityTypes = scmQueryService.ListActivityTypes();

            //Add activity types
            foreach (var activityType in activityTypes)
            {
                var createNewActivityRequest = new CreateNewActivityRequest
                {
                    ActivityTypeId = activityType.Id,
                    ProjectId = projectId,
                    Name = activityType.Name
                };
                var activityId = scmCommandService.CreateNewActivity(createNewActivityRequest);

                var profiles = scmQueryService.ListActivityTypeProfiles(new ListActivityTypeProfilesRequest { ActivityTypeId = activityType.Id });

                foreach (var profile in profiles)
                {
                    var createNewActivityProfileRequest = Mapper.DynamicMap<CreateNewActivityProfileRequest>(profile);
                    createNewActivityProfileRequest.ActivityId = activityId;
                    scmCommandService.CreateNewActivityProfile(createNewActivityProfileRequest);
                }
            }

            var activities = scmQueryService.ListActivities(new ListActivitiesRequest { ProjectId = projectId });
            Assert.IsNotNull(activities);
            ExtendedAssert.IsLargerThan(0, activities.Count);
            Assert.IsTrue(activities.All(a => a.ActivityProfiles.Count > 0));
            Assert.AreEqual(activityTypes.Count, activities.Count);

            var createdProjectPlanPhaseIds = new List<Guid>();
            for (int i = 0; i < 5; i++)
            {
                var newProjectPlanPhaseRequest = new CreateNewProjectPlanPhaseRequest
                    {
                        ProjectPlanId = consultancyProject.ProjectPlanId,
                        Name = string.Format("Phase {0}: Name", i),
                        StartDate = DateTime.Now.AddDays((i - 1) * 30),
                        EndDate = DateTime.Now.AddDays((i) * 30),
                    };
                var newProjectPlanPhaseId = prmCommandService.CreateNewProjectPlanPhase(newProjectPlanPhaseRequest);
                createdProjectPlanPhaseIds.Add(newProjectPlanPhaseId);

                foreach (var activity in activities)
                {
                    foreach (var activityProfile in activity.ActivityProfiles)
                    {
                        var createNewProjectPlanActivityRequest = new CreateNewProjectPlanPhaseActivityRequest
                            {
                                ProjectPlanPhaseId = newProjectPlanPhaseId,
                                ActivityProfileId = activityProfile.Id,
                                Duration = 2,
                                Quantity = 15,
                                Deadline = newProjectPlanPhaseRequest.EndDate
                            };

                        prmCommandService.CreateNewProjectPlanPhaseEntry(createNewProjectPlanActivityRequest);
                    }
                }
            }

            var projectPlan = prmQueryService.RetrieveProjectPlanDetail(new RetrieveProjectPlanDetailRequest { ProjectPlanId = consultancyProject.ProjectPlanId });

            Assert.IsNotNull(projectPlan);
            Assert.AreEqual(5, projectPlan.ProjectPlanPhases.Count);
            Assert.IsTrue(projectPlan.ProjectPlanPhases.Select(ppf => ppf.Id).All(createdProjectPlanPhaseIds.Contains));
        }

        [TestMethod]
        public void TestAssignUnassignProjectRole()
        {
            var prmCommandService = Container.Resolve<IProjectManagementCommandService>();

            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
            var dicQueryService = Container.Resolve<IDictionaryManagementQueryService>();

            const string projectName = "Test ACDC project";
            const string customerName = "DemoCompany";
            const string projectManagerFullName = "Stijn Van Eynde";
            const string customerAssistantFullName = "Thomas King";

            var projectId = CreateNewAssessmentDevelopmentProject(Container, projectName, customerName, projectManagerFullName, customerAssistantFullName);

            //Get newly created project
            var project = (AssessmentDevelopmentProjectView)prmQueryService.RetrieveProjectDetail(projectId);
            Assert.IsNotNull(project);

            var projectTypeCategories = prmQueryService.ListAvailableProjectCategories(project.ProjectTypeId);
            var dictionaries = dicQueryService.ListAvailableDictionaries(project.ContactId);

            var request = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(project);

            var projectCategory = projectTypeCategories.FirstOrDefault(ptc => ptc.Code == "FA");

            Assert.IsNotNull(projectCategory);

            request.ProjectTypeCategoryId = projectCategory.Id;

            var dictionary = dictionaries.FirstOrDefault(d => d.Name.StartsWith("Q WB"));

            Assert.IsNotNull(dictionary);

            request.DictionaryId = dictionary.Id;

            prmCommandService.UpdateAssessmentDevelopmentProject(request);

            project = (AssessmentDevelopmentProjectView)prmQueryService.RetrieveProjectDetail(projectId);

            Assert.IsNotNull(project);
            Assert.IsNotNull(project.MainProjectCategoryDetail);
            Assert.IsNotNull(project.DictionaryId);

            //TODO: Create project role and add dictionary levels to project role
        }

        //[TestMethod]
        public void TestListUserProjectCandidates()
        {
            //REMARK: Before running this test, make sure you have done the following:
            //  1. Added an appointment in SO with the correct candidate formatting (e.g.: QA;EN;Thomas;King)
            //  2. Added the candidate (from the appointment in SO) to the project in Q-Planet in the 
            //     Candidates pane of the project detail
            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
            var startDate = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            var enddate = DateTime.Now;

            //Set current security context to user "Thomas King" (Change (if needed) to user that added the appointment)
            var securityQueryService = Container.Resolve<ISecurityQueryService>();
            var response = securityQueryService.SearchUser(new SearchUserRequest { Name = "Thomas King" });

            var securityContext = Container.Resolve<SecurityContext>();
            Assert.IsTrue(response.Users.Count > 0);
            var user = response.Users.FirstOrDefault();
            Assert.IsNotNull(user);
            securityContext.UserId = user.Id;

            var listUserProjectCandidatesRequest = new ListUserProjectCandidatesRequest
                {
                    StartDate = startDate,
                    EndDate = enddate
                };
            var userProjectCandidates = prmQueryService.ListUserProjectCandidates(listUserProjectCandidatesRequest);

            Assert.IsTrue(userProjectCandidates.Count >= 1);

        }

        [TestMethod]
        public void TestCrudFrameworkAgreement()
        {
            //Retrieve contact to test on
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var contacts = crmQueryService.ListContacts();
            Assert.IsTrue(contacts.Count >= 1);
            var contact = contacts.First();
            Assert.IsNotNull(contact);

            var prmCommandService = Container.Resolve<IProjectManagementCommandService>();
            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();

            //Create framework agreement
            const string frameworkAgreementName = "FrameworkAgreementTest";
            const string frameworkAgreementDescription = "FrameworkAgreement description";
            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(1);

            var createFrameworkAgreementRequest = new CreateFrameworkAgreementRequest
                {
                    Name = frameworkAgreementName,
                    Description = frameworkAgreementDescription,
                    ContactId = contact.Id,
                    StartDate = startDate,
                    EndDate = endDate
                };

            var createdId = prmCommandService.CreateFrameworkAgreement(createFrameworkAgreementRequest);
            Assert.IsTrue(createdId != Guid.Empty);

            //Read created framework agreement
            var frameworkAgreement = prmQueryService.RetrieveFrameworkAgreement(createdId);
            Assert.IsNotNull(frameworkAgreement);
            Assert.AreEqual(frameworkAgreement.Name, frameworkAgreementName);
            Assert.AreEqual(frameworkAgreement.Description, frameworkAgreementDescription);
            Assert.AreEqual(frameworkAgreement.ContactId, contact.Id);
            Assert.AreEqual(frameworkAgreement.StartDate, startDate);
            Assert.AreEqual(frameworkAgreement.EndDate, endDate);

            //List framework agreement(s)
            var frameworkAgreements = prmQueryService.ListFrameworkAgreementsByYear(startDate.Year);
            Assert.IsTrue(frameworkAgreements.Count >= 1);

            //Update framework agreement
            const string updatedName = "updated" + frameworkAgreementName;
            const string updatedDescription = "updated" + frameworkAgreementDescription;

            var updateFrameworkAgreementRequest = new UpdateFrameworkAgreementRequest
            {
                Id = frameworkAgreement.Id,
                Name = updatedName,
                Description = updatedDescription,
                ContactId = contact.Id,
                StartDate = startDate,
                EndDate = endDate,
                AuditVersionid = frameworkAgreement.Audit.VersionId
            };

            var updatedId = prmCommandService.UpdateFrameworkAgreement(updateFrameworkAgreementRequest);
            Assert.IsTrue(updatedId != Guid.Empty);

            //Read updated framework agreement
            var updatedFrameworkAgreement = prmQueryService.RetrieveFrameworkAgreement(updatedId);
            Assert.IsNotNull(updatedFrameworkAgreement);
            Assert.AreEqual(updatedFrameworkAgreement.Name, updatedName);
            Assert.AreEqual(updatedFrameworkAgreement.Description, updatedDescription);
            Assert.AreEqual(updatedFrameworkAgreement.ContactId, contact.Id);
            Assert.AreEqual(updatedFrameworkAgreement.StartDate, startDate);
            Assert.AreEqual(updatedFrameworkAgreement.EndDate, endDate);

            //Delete framework agreement
            prmCommandService.DeleteFrameworkAgreement(updatedId);

            //Check if framework agreement was deleted
            Assert.IsNull(prmQueryService.RetrieveFrameworkAgreement(updatedId));

        }

        [TestMethod]
        public void TestListFrameworkAgreementYears()
        {
            //Retrieve contact to test on
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var contacts = crmQueryService.ListContacts();
            Assert.IsTrue(contacts.Count >= 1);
            var contact = contacts.First();
            Assert.IsNotNull(contact);

            var prmCommandService = Container.Resolve<IProjectManagementCommandService>();
            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();

            //Create some framework agreements
            const string frameworkAgreementName = "FrameworkAgreementTest";
            const string frameworkAgreementDescription = "FrameworkAgreement description";

            for (int i = 0; i < 5; i++)
            {
                var createFrameworkAgreementRequest = new CreateFrameworkAgreementRequest
            {
                Name = frameworkAgreementName + i,
                Description = frameworkAgreementDescription + i,
                ContactId = contact.Id,
                StartDate = DateTime.Now.Date.AddYears(-(i)),
                EndDate = DateTime.Now.Date.AddDays(1).AddYears(-(i))
            };

                var createdId = prmCommandService.CreateFrameworkAgreement(createFrameworkAgreementRequest);
                Assert.IsTrue(createdId != Guid.Empty);
            }

            //Test list framework agreement years
            var years = prmQueryService.ListFrameworkAgreementYears();
            Assert.IsTrue(years.Count >= 1);
        }

        [TestMethod]
        public void TestCreateReportRecipient()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //Create project to be sure there is at least one in the database
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test project", "Democompany", "thomas king", "thomas king");
            var project = queryService.RetrieveProject(projectId);

            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId).Where(ptc => ptc.IsMain);
            var projectTypeCategory = projectTypeCategories.FirstOrDefault(ptc => ptc.Code == "AC");

            var updateProject = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(project);
            updateProject.SelectedProjectTypeCategoryIds.Add(projectTypeCategory.Id);

            service.UpdateAssessmentDevelopmentProject(updateProject);

            //Create candidate linked to that project
            var request = new AddProjectCandidateRequest
            {
                AppointmentId = 1000,
                Code = "123456",
                FirstName = "Sheldon",
                LastName = "Cooper",
                LanguageId = 1,
                Gender = GenderType.M.ToString(),
                ProjectId = project.Id
            };
            var projectCandidateId = service.AddProjectCandidate(request);

            //Retrieve added candidate
            var projectCandidate = queryService.RetrieveProjectCandidate(projectCandidateId);
            Assert.IsNotNull(projectCandidate);

            //Retrieve CRM Email
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var crmEmails = crmQueryService.ListCrmEmails();
            Assert.IsTrue(crmEmails.Count > 0);
            var crmEmail = crmEmails.FirstOrDefault();
            Assert.IsNotNull(crmEmail);

            //Create report recipient
            var createReportRecipientRequest = new CreateProjectCandidateReportRecipientRequest
                {
                    ProjectCandidateId = projectCandidate.Id,
                    CrmEmailId = crmEmail.Id
                };
            var createdId = service.CreateProjectCandidateReportRecipient(createReportRecipientRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);
        }

        [TestMethod]
        public void TestDeleteReportRecipient()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var service = Container.Resolve<IProjectManagementCommandService>();

            //Create project to be sure there is at least one in the database
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test project", "Democompany", "thomas king", "thomas king");
            var project = queryService.RetrieveProject(projectId);

            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId).Where(ptc => ptc.IsMain);
            var projectTypeCategory = projectTypeCategories.FirstOrDefault(ptc => ptc.Code == "AC");

            var updateProject = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(project);
            updateProject.SelectedProjectTypeCategoryIds.Add(projectTypeCategory.Id);

            service.UpdateAssessmentDevelopmentProject(updateProject);

            //Create candidate linked to that project
            var request = new AddProjectCandidateRequest
            {
                AppointmentId = 1000,
                Code = "123456",
                FirstName = "Sheldon",
                LastName = "Cooper",
                LanguageId = 1,
                Gender = GenderType.M.ToString(),
                ProjectId = project.Id
            };
            var projectCandidateId = service.AddProjectCandidate(request);

            //Retrieve added candidate
            var projectCandidate = queryService.RetrieveProjectCandidate(projectCandidateId);
            Assert.IsNotNull(projectCandidate);

            //Retrieve CRM Email
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var crmEmails = crmQueryService.ListCrmEmails();
            Assert.IsTrue(crmEmails.Count > 0);
            var crmEmail = crmEmails.FirstOrDefault();
            Assert.IsNotNull(crmEmail);

            //Create report recipient
            var createReportRecipientRequest = new CreateProjectCandidateReportRecipientRequest
            {
                ProjectCandidateId = projectCandidate.Id,
                CrmEmailId = crmEmail.Id
            };
            var createdId = service.CreateProjectCandidateReportRecipient(createReportRecipientRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //Delete report recipient
            service.DeleteProjectCandidateReportRecipient(createdId);

            //Check if report recipient was deleted
            var reportRecipients = queryService.ListProjectCandidateReportRecipientsByProjectCandidateId(projectCandidateId);
            Assert.IsFalse(reportRecipients.Any(rr => rr.Id == createdId));
        }

      
        [TestMethod]
        public void TestListProjectCandidateReportRecipientsByProjectCandidateId()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var service = Container.Resolve<IProjectManagementCommandService>();

            //Create project to be sure there is at least one in the database
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test project", "Democompany", "thomas king", "thomas king");
            var project = queryService.RetrieveProject(projectId);

            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId).Where(ptc => ptc.IsMain);
            var projectTypeCategory = projectTypeCategories.FirstOrDefault(ptc => ptc.Code == "AC");

            var updateProject = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(project);
            updateProject.SelectedProjectTypeCategoryIds.Add(projectTypeCategory.Id);

            service.UpdateAssessmentDevelopmentProject(updateProject);

            //Create candidate linked to that project
            var request = new AddProjectCandidateRequest
            {
                AppointmentId = 1000,
                Code = "123456",
                FirstName = "Sheldon",
                LastName = "Cooper",
                LanguageId = 1,
                Gender = GenderType.M.ToString(),
                ProjectId = project.Id
            };
            var projectCandidateId = service.AddProjectCandidate(request);

            //Retrieve added candidate
            var projectCandidate = queryService.RetrieveProjectCandidate(projectCandidateId);
            Assert.IsNotNull(projectCandidate);

            //Retrieve CRM Email
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var crmEmails = crmQueryService.ListCrmEmails();
            Assert.IsTrue(crmEmails.Count > 0);
            var crmEmail = crmEmails.FirstOrDefault();
            Assert.IsNotNull(crmEmail);

            //Create report recipient
            var createReportRecipientRequest = new CreateProjectCandidateReportRecipientRequest
            {
                ProjectCandidateId = projectCandidate.Id,
                CrmEmailId = crmEmail.Id
            };
            var createdId = service.CreateProjectCandidateReportRecipient(createReportRecipientRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //List report recipients
            var reportRecipients = queryService.ListProjectCandidateReportRecipientsByProjectCandidateId(projectCandidate.Id);
            Assert.IsTrue(reportRecipients.Count > 0);
        }

        [TestMethod]
        public void TestListProjectCategoryDetails()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var service = Container.Resolve<IProjectManagementCommandService>();

            //Create project to be sure there is at least one in the database
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Test project", "Democompany", "thomas king", "thomas king");
            var project = queryService.RetrieveProject(projectId);

            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId).Where(ptc => ptc.IsMain);
            Assert.IsTrue(projectTypeCategories.Any());
            var projectTypeCategory = projectTypeCategories.FirstOrDefault(ptc => ptc.Code == "AC");
            Assert.IsNotNull(projectTypeCategory);

            var updateProject = Mapper.DynamicMap<UpdateAssessmentDevelopmentProjectRequest>(project);
            updateProject.SelectedProjectTypeCategoryIds.Add(projectTypeCategory.Id);

            service.UpdateAssessmentDevelopmentProject(updateProject);

            var projectCategoryDetails = queryService.ListProjectCategoryDetails(projectId);
            Assert.IsTrue(projectCategoryDetails.Any());
        }

        [TestMethod]
        public void TestListReportStatuses()
        {
            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
            var reportStatuses = prmQueryService.ListReportStatuses();
            Assert.IsTrue(reportStatuses.Count > 0);
        }

        [TestMethod]
        public void TestRetrieveReportStatusByCode()
        {
            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
            var reportStatuses = prmQueryService.ListReportStatuses();
            Assert.IsTrue(reportStatuses.Count > 0);

            var firstReportStatus = reportStatuses.FirstOrDefault();
            Assert.IsNotNull(firstReportStatus);

            var reportStatus = prmQueryService.RetrieveReportStatusByCode(firstReportStatus.Code);
            Assert.IsNotNull(reportStatus);
            Assert.AreEqual(firstReportStatus.Id, reportStatus.Id);
            Assert.AreEqual(firstReportStatus.Code, reportStatus.Code);
            Assert.AreEqual(firstReportStatus.Name, reportStatus.Name);
            Assert.AreEqual(firstReportStatus.SortOrder, reportStatus.SortOrder);
        }

        [TestMethod]
        public void TestListProjectTypeCategoryLevels()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);
        }

        [TestMethod]
        public void TestCreateProjectTypeCategoryUnitPrice()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            //Create project type category unit price
            const decimal unitPrice = 1500m;
            var createProjectTypeCategoryUnitPriceRequest = new CreateProjectTypeCategoryUnitPriceRequest
                {
                    ProjectTypeCategoryId = projectTypeCategory.Id,
                    ProjectTypeCategoryLevelId = projectTypeCategoryLevel.Id,
                    UnitPrice = unitPrice
                };
            var createdId = service.CreateProjectTypeCategoryUnitPrice(createProjectTypeCategoryUnitPriceRequest);

            //Retrieve created project type category unit price
            var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(projectTypeCategoryUnitPrice);
        }

        [TestMethod]
        public void TestRetrieveProjectTypeCategoryUnitPrice()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            //Create project type category unit price
            const decimal unitPrice = 1500m;
            var createProjectTypeCategoryUnitPriceRequest = new CreateProjectTypeCategoryUnitPriceRequest
            {
                ProjectTypeCategoryId = projectTypeCategory.Id,
                ProjectTypeCategoryLevelId = projectTypeCategoryLevel.Id,
                UnitPrice = unitPrice
            };
            var createdId = service.CreateProjectTypeCategoryUnitPrice(createProjectTypeCategoryUnitPriceRequest);

            //Retrieve created project type category unit price
            var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(projectTypeCategoryUnitPrice);
        }

        [TestMethod]
        public void TestUpdateProjectTypeCategoryUnitPrice()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            //Create project type category unit price
            const decimal unitPrice = 1500m;
            var createProjectTypeCategoryUnitPriceRequest = new CreateProjectTypeCategoryUnitPriceRequest
            {
                ProjectTypeCategoryId = projectTypeCategory.Id,
                ProjectTypeCategoryLevelId = projectTypeCategoryLevel.Id,
                UnitPrice = unitPrice
            };
            var createdId = service.CreateProjectTypeCategoryUnitPrice(createProjectTypeCategoryUnitPriceRequest);

            //Retrieve created project type category unit price
            var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(projectTypeCategoryUnitPrice);
            Assert.AreEqual(projectTypeCategory.Id, projectTypeCategoryUnitPrice.ProjectTypeCategoryId);
            Assert.AreEqual(projectTypeCategoryLevel.Id, projectTypeCategoryUnitPrice.ProjectTypeCategoryLevelId);
            Assert.AreEqual(unitPrice, projectTypeCategoryUnitPrice.UnitPrice);

            //Update project type category unit price
            const decimal newUnitPrice = 1500.5m;
            var updateProjectTypeCategoryUnitPriceRequest =
                Mapper.DynamicMap<UpdateProjectTypeCategoryUnitPriceRequest>(projectTypeCategoryUnitPrice);
            updateProjectTypeCategoryUnitPriceRequest.UnitPrice = newUnitPrice;
            service.UpdateProjectTypeCategoryUnitPrice(updateProjectTypeCategoryUnitPriceRequest);

            //Retrieve updated project type category unit price
            var updatedProjectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(updatedProjectTypeCategoryUnitPrice);
            Assert.AreEqual(projectTypeCategoryUnitPrice.ProjectTypeCategoryId, updatedProjectTypeCategoryUnitPrice.ProjectTypeCategoryId);
            Assert.AreEqual(projectTypeCategoryUnitPrice.ProjectTypeCategoryLevelId, updatedProjectTypeCategoryUnitPrice.ProjectTypeCategoryLevelId);
            Assert.AreNotEqual(projectTypeCategoryUnitPrice.UnitPrice, updatedProjectTypeCategoryUnitPrice.UnitPrice);
            Assert.AreEqual(newUnitPrice, updatedProjectTypeCategoryUnitPrice.UnitPrice);
        }

        [TestMethod]
        public void TestDeleteProjectTypeCategoryUnitPrice()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            //Create project type category unit price
            const decimal unitPrice = 1500m;
            var createProjectTypeCategoryUnitPriceRequest = new CreateProjectTypeCategoryUnitPriceRequest
            {
                ProjectTypeCategoryId = projectTypeCategory.Id,
                ProjectTypeCategoryLevelId = projectTypeCategoryLevel.Id,
                UnitPrice = unitPrice
            };
            var createdId = service.CreateProjectTypeCategoryUnitPrice(createProjectTypeCategoryUnitPriceRequest);

            //Retrieve created project type category unit price
            var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(projectTypeCategoryUnitPrice);

            //Delete project type category unit price
            service.DeleteProjectTypeCategoryUnitPrice(projectTypeCategoryUnitPrice.Id);

            //Check if project type category unit price was deleted
            Assert.IsNull(queryService.RetrieveProjectTypeCategoryUnitPrice(projectTypeCategoryUnitPrice.Id));
        }

        [TestMethod]
        public void TestListProjectTypeCategoryUnitPriceOverview()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            //Create project type category unit price
            const decimal unitPrice = 1500m;
            var createProjectTypeCategoryUnitPriceRequest = new CreateProjectTypeCategoryUnitPriceRequest
            {
                ProjectTypeCategoryId = projectTypeCategory.Id,
                ProjectTypeCategoryLevelId = projectTypeCategoryLevel.Id,
                UnitPrice = unitPrice
            };
            var createdId = service.CreateProjectTypeCategoryUnitPrice(createProjectTypeCategoryUnitPriceRequest);

            //Retrieve created project type category unit price
            var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(projectTypeCategoryUnitPrice);

            var projectTypeCategoryUnitPriceOverviewResponse = queryService.ListProjectTypeCategoryUnitPriceOverview();
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse);
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategories);
            Assert.IsTrue(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategories.Count > 0);
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryLevels);
            Assert.IsTrue(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryLevels.Count > 0);
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryUnitPrices);
            Assert.IsTrue(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryUnitPrices.Count > 0);
        }

        [TestMethod]
        public void TestUpdateProjectTypeCategoryUnitPrices()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            //Create project type category unit price
            const decimal unitPrice = 1500m;
            var createProjectTypeCategoryUnitPriceRequest = new CreateProjectTypeCategoryUnitPriceRequest
            {
                ProjectTypeCategoryId = projectTypeCategory.Id,
                ProjectTypeCategoryLevelId = projectTypeCategoryLevel.Id,
                UnitPrice = unitPrice
            };
            var createdId = service.CreateProjectTypeCategoryUnitPrice(createProjectTypeCategoryUnitPriceRequest);

            //Retrieve created project type category unit price
            var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPrice(createdId);
            Assert.IsNotNull(projectTypeCategoryUnitPrice);

            var projectTypeCategoryUnitPriceOverviewResponse = queryService.ListProjectTypeCategoryUnitPriceOverview();
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse);
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategories);
            Assert.IsTrue(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategories.Count > 0);
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryLevels);
            Assert.IsTrue(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryLevels.Count > 0);
            Assert.IsNotNull(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryUnitPrices);
            Assert.IsTrue(projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryUnitPrices.Count > 0);

            var updateProjectTypeCategoryUnitPriceRequests =
                projectTypeCategoryUnitPriceOverviewResponse.ProjectTypeCategoryUnitPrices.Select(
                    Mapper.DynamicMap<UpdateProjectTypeCategoryUnitPriceRequest>).ToList();

            Assert.IsTrue(updateProjectTypeCategoryUnitPriceRequests.Count > 0);

            const decimal updateUnitPrice = 1500.5m;

            foreach (var updateProjectTypeCategoryUnitPriceRequest in updateProjectTypeCategoryUnitPriceRequests)
            {
                updateProjectTypeCategoryUnitPriceRequest.UnitPrice = updateUnitPrice;
            }

            service.UpdateProjectTypeCategoryUnitPrices(updateProjectTypeCategoryUnitPriceRequests);

            //Check if update was successful
            foreach (var updateProjectTypeCategoryUnitPriceRequest in updateProjectTypeCategoryUnitPriceRequests)
            {
                var updatedProjectTypeCategoryUnitPrice =
                    queryService.RetrieveProjectTypeCategoryUnitPrice(updateProjectTypeCategoryUnitPriceRequest.Id);
                Assert.IsNotNull(updatedProjectTypeCategoryUnitPrice);
                Assert.AreEqual(updatedProjectTypeCategoryUnitPrice.UnitPrice, updateUnitPrice);
            }
        }

        [TestMethod]
        public void TestListProjectTypeCategoryUnitPricesByCategory()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            //List project type category levels
            var projectTypeCategoryLevels = queryService.ListProjectTypeCategoryLevels();
            Assert.IsTrue(projectTypeCategoryLevels.Count > 0);

            //Retrieve first project type category level
            var projectTypeCategoryLevel = projectTypeCategoryLevels.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategoryLevel);

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //List project type categories
            var projectTypeCategories = queryService.ListAvailableProjectCategories(project.ProjectTypeId);
            Assert.IsTrue(projectTypeCategories.Count > 0);

            //Retrieve first project type category
            var projectTypeCategory = projectTypeCategories.FirstOrDefault();
            Assert.IsNotNull(projectTypeCategory);

            var projectTypeCategoryUnitPrices =
                queryService.ListProjectTypeCategoryUnitPricesByCategory(projectTypeCategory.Id);
            Assert.IsTrue(projectTypeCategoryUnitPrices.Count > 0);
            foreach (var projectTypeCategoryUnitPrice in projectTypeCategoryUnitPrices)
            {
                Assert.IsNotNull(projectTypeCategoryUnitPrice.ProjectTypeCategoryLevel);
            }

        }

        [TestMethod]
        public void TestCreateProjectProducts()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //Retrieve product types
            var productTypes = scmQueryService.ListProductTypes();
            Assert.IsTrue(productTypes.Count > 0);
            var productType = productTypes.FirstOrDefault();
            Assert.IsNotNull(productType);

            //Create ProjectProduct
            var invoiceAmount = productType.UnitPrice;
            var createRequest = new CreateProjectProductRequest
                {
                    ProjectId = projectId,
                    ProductTypeId = productType.Id,
                    InvoiceAmount = invoiceAmount
                };
            var createRequests = new List<CreateProjectProductRequest> { createRequest };
            service.CreateProjectProducts(createRequests);

            var projectProducts = queryService.ListProjectProducts(projectId);
            Assert.IsTrue(projectProducts.Count > 0);
        }

        [TestMethod]
        public void TestListProjectProducts()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //Retrieve product types
            var productTypes = scmQueryService.ListProductTypes();
            Assert.IsTrue(productTypes.Count > 0);
            var productType = productTypes.FirstOrDefault();
            Assert.IsNotNull(productType);

            //Create ProjectProduct
            var invoiceAmount = productType.UnitPrice;
            var createRequest = new CreateProjectProductRequest
            {
                ProjectId = projectId,
                ProductTypeId = productType.Id,
                InvoiceAmount = invoiceAmount
            };
            service.CreateProjectProduct(createRequest);

            var projectProducts = queryService.ListProjectProducts(projectId);
            Assert.IsTrue(projectProducts.Count > 0);
        }

        [TestMethod]
        public void TestCreateProjectProduct()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //Retrieve product types
            var productTypes = scmQueryService.ListProductTypes();
            Assert.IsTrue(productTypes.Count > 0);
            var productType = productTypes.FirstOrDefault();
            Assert.IsNotNull(productType);

            //Create ProjectProduct
            var invoiceAmount = productType.UnitPrice;
            const int invoiceStatusCode = (int)InvoiceStatusType.Planned;
            var createRequest = new CreateProjectProductRequest
            {
                ProjectId = projectId,
                ProductTypeId = productType.Id,
                InvoiceAmount = invoiceAmount
            };
            var createdId = service.CreateProjectProduct(createRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //Retrieve created ProjectProduct
            var projectProduct = queryService.RetrieveProjectProduct(createdId);
            Assert.IsNotNull(projectProduct);
            Assert.AreEqual(projectId, projectProduct.ProjectId);
            Assert.AreEqual(productType.Id, projectProduct.ProductTypeId);
            Assert.AreEqual(invoiceAmount, projectProduct.InvoiceAmount);
            Assert.AreEqual(invoiceStatusCode, projectProduct.InvoiceStatusCode);
        }

        [TestMethod]
        public void TestRetrieveProjectProduct()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //Retrieve product types
            var productTypes = scmQueryService.ListProductTypes();
            Assert.IsTrue(productTypes.Count > 0);
            var productType = productTypes.FirstOrDefault();
            Assert.IsNotNull(productType);

            //Create ProjectProduct
            var invoiceAmount = productType.UnitPrice;
            const int invoiceStatusCode = (int)InvoiceStatusType.Planned;
            var createRequest = new CreateProjectProductRequest
            {
                ProjectId = projectId,
                ProductTypeId = productType.Id,
                InvoiceAmount = invoiceAmount
            };
            var createdId = service.CreateProjectProduct(createRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //Retrieve created ProjectProduct
            var projectProduct = queryService.RetrieveProjectProduct(createdId);
            Assert.IsNotNull(projectProduct);
            Assert.AreEqual(projectId, projectProduct.ProjectId);
            Assert.AreEqual(productType.Id, projectProduct.ProductTypeId);
            Assert.AreEqual(invoiceAmount, projectProduct.InvoiceAmount);
            Assert.AreEqual(invoiceStatusCode, projectProduct.InvoiceStatusCode);
        }

        [TestMethod]
        public void TestDeleteProjectProduct()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create ACDC project
            var projectId = CreateNewAssessmentDevelopmentProject(Container, "Testproject", "DemoCompany", "Thomas King", "Thomas King");
            var project = queryService.RetrieveProject(projectId);
            Assert.IsNotNull(project);

            //Retrieve product types
            var productTypes = scmQueryService.ListProductTypes();
            Assert.IsTrue(productTypes.Count > 0);
            var productType = productTypes.FirstOrDefault();
            Assert.IsNotNull(productType);

            //Create ProjectProduct
            var invoiceAmount = productType.UnitPrice;
            var createRequest = new CreateProjectProductRequest
            {
                ProjectId = projectId,
                ProductTypeId = productType.Id,
                InvoiceAmount = invoiceAmount
            };
            var createdId = service.CreateProjectProduct(createRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //Retrieve created ProjectProduct
            var projectProduct = queryService.RetrieveProjectProduct(createdId);
            Assert.IsNotNull(projectProduct);

            //Delete ProjectProduct
            service.DeleteProjectProduct(projectProduct.Id);
            Assert.IsNull(queryService.RetrieveProjectProduct(createdId));
        }

        [TestMethod]
        public void TestCreateProjectEvaluation()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Create ProjectEvaluation
            const string lessonsLearned = "Lessons I learned.";
            const string evaluation = "The evaluation I give.";
            var createRequest = new CreateProjectEvaluationRequest
                {
                    CrmProjectId = crmProject.Id,
                    LessonsLearned = lessonsLearned,
                    Evaluation = evaluation
                };
            var createdId = service.CreateProjectEvaluation(createRequest);

            //Retrieve ProjectEvaluation
            var projectEvaluation = queryService.RetrieveProjectEvaluation(createdId);
            Assert.IsNotNull(projectEvaluation);
            Assert.AreEqual(crmProject.Id, projectEvaluation.CrmProjectId);
        }

        [TestMethod]
        public void TestUpdateProjectEvaluation()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Create ProjectEvaluation
            const string lessonsLearned = "Lessons I learned.";
            const string evaluation = "The evaluation I give.";
            var createRequest = new CreateProjectEvaluationRequest
            {
                CrmProjectId = crmProject.Id,
                LessonsLearned = lessonsLearned,
                Evaluation = evaluation
            };
            var createdId = service.CreateProjectEvaluation(createRequest);

            //Retrieve ProjectEvaluation
            var projectEvaluation = queryService.RetrieveProjectEvaluation(createdId);
            Assert.IsNotNull(projectEvaluation);
            Assert.AreEqual(crmProject.Id, projectEvaluation.CrmProjectId);

            //Update ProjectEvaluation
            const string updateLessonsLearned = "Updated lessons I learned.";
            const string updateEvaluation = "The updated evaluation I give.";
            var updateRequest = new UpdateProjectEvaluationRequest();
            Mapper.DynamicMap(projectEvaluation, updateRequest);
            updateRequest.LessonsLearned = updateLessonsLearned;
            updateRequest.Evaluation = updateEvaluation;
            service.UpdateProjectEvaluation(updateRequest);

            //Retrieve updated ProjectEvaluation
            var updatedProjectEvaluation = queryService.RetrieveProjectEvaluation(createdId);
            Assert.IsNotNull(updatedProjectEvaluation);
            Assert.AreEqual(crmProject.Id, updatedProjectEvaluation.CrmProjectId);
        }

        [TestMethod]
        public void TestRetrieveProjectEvaluationByCrmProject()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Create ProjectEvaluation
            const string lessonsLearned = "Lessons I learned.";
            const string evaluation = "The evaluation I give.";
            var createRequest = new CreateProjectEvaluationRequest
            {
                CrmProjectId = crmProject.Id,
                LessonsLearned = lessonsLearned,
                Evaluation = evaluation
            };
            var createdId = service.CreateProjectEvaluation(createRequest);

            //Retrieve ProjectEvaluation for that CRM project
            var projectEvaluation = queryService.RetrieveProjectEvaluationByCrmProject(crmProject.Id);
            Assert.IsNotNull(projectEvaluation);
        }

        [TestMethod]
        public void TestRetrieveEmptyProjectEvaluationByCrmProject()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Retrieve ProjectEvaluation for that CRM project (no need to create one, because if it doesn't exist, an empty one will be created)
            var projectEvaluation = queryService.RetrieveProjectEvaluationByCrmProject(crmProject.Id);
            Assert.IsNotNull(projectEvaluation);
        }

        [TestMethod]
        public void TestCreateEvaluationForm()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Create EvaluationForm
            const string firstName = "Thomas",
                         lastName = "King",
                         email = "thomas.king@quintessence.be",
                         gender = "M";
            const int languageId = 1,
                      evaluationFormTypeId = 10;
            var createRequest = new CreateEvaluationFormRequest
            {
                CrmProjectId = crmProject.Id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Email = email,
                LanguageId = languageId,
                EvaluationFormTypeId = evaluationFormTypeId
            };
            var createdId = service.CreateEvaluationForm(createRequest);

            //Retrieve ProjectEvaluation
            var evaluationForm = queryService.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest(){Id = createdId});
            Assert.IsNotNull(evaluationForm);
            Assert.AreEqual(firstName, evaluationForm.FirstName);
            Assert.AreEqual(lastName, evaluationForm.LastName);
            Assert.AreEqual(gender, evaluationForm.Gender);
            Assert.AreEqual(email, evaluationForm.Email);
            Assert.AreEqual(evaluationFormTypeId, evaluationForm.EvaluationFormTypeId);
            Assert.AreEqual(languageId, evaluationForm.LanguageId);
            Assert.AreEqual(crmProject.Id, evaluationForm.CrmProjectId);
            Assert.IsTrue(evaluationForm.VerificationCode.Length == 6);
            Assert.AreEqual(10, evaluationForm.MailStatusTypeId);
        }

        [TestMethod]
        public void TestRetrieveEvaluationForm()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Create EvaluationForm
            const string firstName = "Thomas",
                         lastName = "King",
                         email = "thomas.king@quintessence.be",
                         gender = "M";
            const int languageId = 1,
                      evaluationFormTypeId = 10;
            var createRequest = new CreateEvaluationFormRequest
            {
                CrmProjectId = crmProject.Id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Email = email,
                LanguageId = languageId,
                EvaluationFormTypeId = evaluationFormTypeId
            };
            var createdId = service.CreateEvaluationForm(createRequest);

            //Retrieve ProjectEvaluation
            var evaluationForm = queryService.RetrieveEvaluationForm(new RetrieveEvaluationFormRequest() { Id = createdId });
            Assert.IsNotNull(evaluationForm);
            Assert.AreEqual(firstName, evaluationForm.FirstName);
            Assert.AreEqual(lastName, evaluationForm.LastName);
            Assert.AreEqual(gender, evaluationForm.Gender);
            Assert.AreEqual(email, evaluationForm.Email);
            Assert.AreEqual(evaluationFormTypeId, evaluationForm.EvaluationFormTypeId);
            Assert.AreEqual(languageId, evaluationForm.LanguageId);
            Assert.AreEqual(crmProject.Id, evaluationForm.CrmProjectId);
            Assert.IsTrue(evaluationForm.VerificationCode.Length == 6);
            Assert.AreEqual(10, evaluationForm.MailStatusTypeId);
        }

        [TestMethod]
        public void TestCreateEvaluationFormVerificationCode()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var verificationCode = queryService.CreateEvaluationFormVerificationCode();
            Assert.IsNotNull(verificationCode.Length == 6);
        }

        [TestMethod]
        public void TestListEvaluationForms()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Create EvaluationForm
            const string firstName = "Thomas",
                         lastName = "King",
                         email = "thomas.king@quintessence.be",
                         gender = "M";
            const int languageId = 1,
                      evaluationFormTypeId = 10;
            var createRequest = new CreateEvaluationFormRequest
            {
                CrmProjectId = crmProject.Id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Email = email,
                LanguageId = languageId,
                EvaluationFormTypeId = evaluationFormTypeId
            };
            service.CreateEvaluationForm(createRequest);

            //List EvaluationForm(s) for the CRM project
            var evaluationForms = queryService.ListEvaluationFormsByCrmProject(crmProject.Id);
            Assert.IsTrue(evaluationForms.Count > 0);
        }

        [TestMethod]
        public void TestListEvaluationFormTypes()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var evaluationFormTypes = queryService.ListEvaluationFormTypes();
            Assert.IsTrue(evaluationFormTypes.Count > 0);
        }

        [TestMethod]
        public void TestListMailStatusTypes()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var mailStatusTypes = queryService.ListMailStatusTypes();
            Assert.IsTrue(mailStatusTypes.Count > 0);
        }

        [TestMethod]
        public void TestListComplaintTypes()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var complaintTypes = queryService.ListComplaintTypes();
            Assert.IsTrue(complaintTypes.Count > 0);
        }

        [TestMethod]
        public void TestRetrieveProjectComplaint()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Retrieve ComplaintType
            var complaintTypes = queryService.ListComplaintTypes();
            Assert.IsTrue(complaintTypes.Count > 0);
            var complaintType = complaintTypes.FirstOrDefault(ct => ct.Code == "VARIOUS");
            Assert.IsNotNull(complaintType);

            //Create ProjectComplaint
            const string subject = "My Complaint",
                         followUp = "The complaint is in progress.";
            var complaintDate = new DateTime(2012,1,1);
            const int complaintSeverityTypeId = (int)ComplaintSeverityType.Low;
            var complaintTypeId = complaintType.Id;
            var createRequest = new CreateProjectComplaintRequest
            {
                CrmProjectId = crmProject.Id,
                Subject = subject,
                //SubmitterId = submitter,
                //Details = details,
                FollowUp = followUp,
                ComplaintDate = complaintDate,
                ComplaintSeverityTypeId = complaintSeverityTypeId,
                ComplaintTypeId = complaintTypeId
            };
            var createdId = service.CreateProjectComplaint(createRequest);

            //Retrieve ProjectComplaint
            var projectComplaint = queryService.RetrieveProjectComplaint(createdId);
            Assert.IsNotNull(projectComplaint);
        }

        [TestMethod]
        public void TestCreateProjectComplaint()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Retrieve ComplaintType
            var complaintTypes = queryService.ListComplaintTypes();
            Assert.IsTrue(complaintTypes.Count > 0);
            var complaintType = complaintTypes.FirstOrDefault(ct => ct.Code == "VARIOUS");
            Assert.IsNotNull(complaintType);

            //Create ProjectComplaint
            const string subject = "My Complaint",
                         followUp = "The complaint is in progress.";
            var complaintDate = new DateTime(2012, 1, 1);
            const int complaintSeverityTypeId = (int)ComplaintSeverityType.Low;
            var complaintTypeId = complaintType.Id;
            var createRequest = new CreateProjectComplaintRequest
            {
                CrmProjectId = crmProject.Id,
                Subject = subject,
                //Submitter = submitter,
                //Details = details,
                FollowUp = followUp,
                ComplaintDate = complaintDate,
                ComplaintSeverityTypeId = complaintSeverityTypeId,
                ComplaintTypeId = complaintTypeId
            };
            var createdId = service.CreateProjectComplaint(createRequest);

            //Retrieve ProjectComplaint
            var projectComplaint = queryService.RetrieveProjectComplaint(createdId);
            Assert.IsNotNull(projectComplaint);
        }

        [TestMethod]
        public void TestRetrieveProjectComplaintByCrmProject()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Retrieve ComplaintType
            var complaintTypes = queryService.ListComplaintTypes();
            Assert.IsTrue(complaintTypes.Count > 0);
            var complaintType = complaintTypes.FirstOrDefault(ct => ct.Code == "VARIOUS");
            Assert.IsNotNull(complaintType);

            //Retrieve ProjectComplaint by CRM project (no need to create one, because if it doesn't exist, an empty one will be created). 
            //var projectComplaint = queryService.RetrieveProjectComplaintByCrmProject(crmProject.Id);
            //Assert.IsNotNull(projectComplaint);
        }

        [TestMethod]
        public void TestUpdateProjectComplaint()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Retrieve ComplaintType
            var complaintTypes = queryService.ListComplaintTypes();
            Assert.IsTrue(complaintTypes.Count > 0);
            var complaintType = complaintTypes.FirstOrDefault(ct => ct.Code == "VARIOUS");
            Assert.IsNotNull(complaintType);

            //Create ProjectComplaint
            const string subject = "My Complaint",
                         followUp = "The complaint is in progress.";
            var complaintDate = new DateTime(2012, 1, 1);
            const int complaintSeverityTypeId = (int)ComplaintSeverityType.Low;
            var complaintTypeId = complaintType.Id;
            var createRequest = new CreateProjectComplaintRequest
            {
                CrmProjectId = crmProject.Id,
                Subject = subject,
                //Submitter = submitter,
                //Details = details,
                FollowUp = followUp,
                ComplaintDate = complaintDate,
                ComplaintSeverityTypeId = complaintSeverityTypeId,
                ComplaintTypeId = complaintTypeId
            };
            var createdId = service.CreateProjectComplaint(createRequest);

            //Retrieve ProjectComplaint
            var projectComplaint = queryService.RetrieveProjectComplaint(createdId);
            Assert.IsNotNull(projectComplaint);

            //Update ProjectComplaint
            const string updateSubject = "My Complaint",
                         updateFollowUp = "The complaint is in progress.";
            const int updateComplaintSeverityTypeId = (int)ComplaintSeverityType.Medium;
            //Retrieve other complaint type for update
            var updateComplaintType = complaintTypes.FirstOrDefault(ct => ct.Code == "QUALITY");
            Assert.IsNotNull(updateComplaintType);
            var updateComplaintTypeId = updateComplaintType.Id;
            var updateRequest = new UpdateProjectComplaintRequest();
            Mapper.DynamicMap(projectComplaint, updateRequest);
            updateRequest.CrmProjectId = crmProject.Id;
            updateRequest.Subject = updateSubject;
            //updateRequest.Submitter = updateSubmitter;
            //updateRequest.Details = updateDetails;
            updateRequest.FollowUp = updateFollowUp;
            updateRequest.ComplaintSeverityTypeId = updateComplaintSeverityTypeId;
            updateRequest.ComplaintTypeId = updateComplaintTypeId;
            service.UpdateProjectComplaint(updateRequest);

            //Retrieve updated ProjectComplaint
            var updatedProjectComplaint = queryService.RetrieveProjectComplaint(createdId);
            Assert.IsNotNull(updatedProjectComplaint);
        }

        [TestMethod]
        public void TestRetrieveProjectComplaintOverview()
        {
            var service = Container.Resolve<IProjectManagementCommandService>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM projects
            var crmProjects = crmQueryService.ListActiveProjects(6712); //Contact id 6712 = DemoCompany (=company for testing purposes)
            Assert.IsTrue(crmProjects.Count > 0);
            var crmProject = crmProjects.FirstOrDefault();
            Assert.IsNotNull(crmProject);

            //Retrieve ProjectComplaint overview for that CRM project (no need to create one, because if it doesn't exist, an empty one will be created).
            //var response = queryService.RetrieveProjectComplaintOverview(crmProject.Id);
            //Assert.IsNotNull(response.ProjectComplaint);
            //Assert.IsTrue(response.ComplaintTypes.Count > 0);
        }

        [TestMethod]
        public void TestListProjectCandidateOverviewEntries()
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();
            var candidateOverviewEntries = queryService.ListProjectCandidateOverviewEntries(new ListProjectCandidateOverviewRequest());
            Assert.IsTrue(candidateOverviewEntries.Entries.Count > 0);
        }

        #region Helper methods

        public static Guid CreateNewAssessmentDevelopmentProject(IUnityContainer container, string projectName, string customerName, string projectManagerFullName, string customerAssistantFullName)
        {
            var createNewProjectRequest = new CreateNewProjectRequest();
            var projectQueryService = container.Resolve<IProjectManagementQueryService>();
            var customerRelationshipQueryService = container.Resolve<ICustomerRelationshipManagementQueryService>();
            var securityQueryService = container.Resolve<ISecurityQueryService>();

            var contact = customerRelationshipQueryService.SearchContacts(new SearchContactRequest { CustomerName = customerName }).FirstOrDefault();

            Assert.IsNotNull(contact);

            var crmProject = customerRelationshipQueryService.ListActiveProjects(contact.Id).FirstOrDefault();

            Assert.IsNotNull(crmProject);

            var projectManagerUser = securityQueryService.SearchUser(new SearchUserRequest { Name = projectManagerFullName }).Users.FirstOrDefault();
            var customerAssistantUser = securityQueryService.SearchUser(new SearchUserRequest { Name = customerAssistantFullName }).Users.FirstOrDefault();

            Assert.IsNotNull(projectManagerUser);
            Assert.IsNotNull(customerAssistantUser);

            var projectType = projectQueryService.ListProjectTypes().FirstOrDefault(pt => pt.Code == "ACDC");

            Assert.IsNotNull(projectType);

            createNewProjectRequest.ContactId = contact.Id;
            createNewProjectRequest.CrmProjectId = crmProject.Id;
            createNewProjectRequest.ProjectManagerUserId = projectManagerUser.Id;
            createNewProjectRequest.CustomerAssistantUserId = customerAssistantUser.Id;
            createNewProjectRequest.ProjectTypeId = projectType.Id;
            createNewProjectRequest.ProjectName = projectName;

            var commandService = container.Resolve<IProjectManagementCommandService>();

            return commandService.CreateNewProject(createNewProjectRequest);
        }

        public static Guid CreateNewConsultancyProject(IUnityContainer container, string projectName, string customerName, string projectManagerFullName, string customerAssistantFullName)
        {
            var createNewProjectRequest = new CreateNewProjectRequest();
            var projectQueryService = container.Resolve<IProjectManagementQueryService>();
            var customerRelationshipQueryService = container.Resolve<ICustomerRelationshipManagementQueryService>();
            var securityQueryService = container.Resolve<ISecurityQueryService>();

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

            var commandService = container.Resolve<IProjectManagementCommandService>();

            return commandService.CreateNewProject(createNewProjectRequest);
        }

        #endregion
    }
}
