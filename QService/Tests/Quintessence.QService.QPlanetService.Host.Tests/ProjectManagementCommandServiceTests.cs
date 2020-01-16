using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Host.Tests.Base;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.QPlanetService.Host.Tests
{
    //[TestClass]
    public class ProjectManagementCommandServiceTests : ServiceTestBase
    {
        [TestMethod]
        public void TestCreateAndUpdateManyConsultancyProjects()
        {
            var stopwatch = Stopwatch.StartNew();
            Parallel.For(0, 25, index =>
            {
                try
                {
                    Thread.Sleep(new Random().Next(50));

                    var createNewProjectRequest = new CreateNewProjectRequest();

                    var contact = Execute<ICustomerRelationshipManagementQueryService, List<CrmContactView>>(service => service.SearchContacts(new SearchContactRequest { CustomerName = "lafarge" })).FirstOrDefault();

                    Assert.IsNotNull(contact);

                    var crmProject = Execute<ICustomerRelationshipManagementQueryService, List<CrmActiveProjectView>>(service => service.ListActiveProjects(contact.Id)).FirstOrDefault();

                    Assert.IsNotNull(crmProject);

                    var user = Execute<ISecurityQueryService, SearchUserResponse>(service => service.SearchUser(new SearchUserRequest { Name = "Stijn Van Eynde" })).Users.FirstOrDefault();

                    Assert.IsNotNull(user);

                    var projectType = Execute<IProjectManagementQueryService, List<ProjectTypeView>>(service => service.ListProjectTypes()).FirstOrDefault(pt => pt.Code == "CONS");

                    Assert.IsNotNull(projectType);

                    createNewProjectRequest.ContactId = contact.Id;
                    createNewProjectRequest.CrmProjectId = crmProject.Id;
                    createNewProjectRequest.ProjectManagerUserId = user.Id;
                    createNewProjectRequest.CustomerAssistantUserId = user.Id;
                    createNewProjectRequest.ProjectTypeId = projectType.Id;
                    createNewProjectRequest.ProjectName = "Test project";

                    var projectId = Execute<IProjectManagementCommandService, Guid>(service => service.CreateNewProject(createNewProjectRequest));

                    Assert.AreNotEqual(Guid.Empty, projectId);

                    var project = (ConsultancyProjectView)Execute<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(projectId));

                    Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

                    Assert.AreNotEqual(Guid.Empty, ((ConsultancyProjectView)project).ProjectPlanId);

                    Thread.Sleep(new Random().Next(50));

                    var updateConsultancyProjectRequest = Mapper.DynamicMap<UpdateConsultancyProjectRequest>(project);

                    updateConsultancyProjectRequest.StatusCode = (int)ProjectStatusCodeType.Ready;
                    updateConsultancyProjectRequest.PricingModelId = 2; //Fixed price

                    Execute<IProjectManagementCommandService>(service => service.UpdateConsultancyProject(updateConsultancyProjectRequest));

                    project = (ConsultancyProjectView)Execute<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(projectId));

                    Assert.AreEqual((int)ProjectStatusCodeType.Ready, project.StatusCode);
                    Assert.AreEqual(2, project.PricingModelId);
                }
                catch (Exception exception)
                {
                    Assert.Fail(exception.Message);
                }
            });
            stopwatch.Stop();
            Console.WriteLine("Elapsed milliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }
    }
}
