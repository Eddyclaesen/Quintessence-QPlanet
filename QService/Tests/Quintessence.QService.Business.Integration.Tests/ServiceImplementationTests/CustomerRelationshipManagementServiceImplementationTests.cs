using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class CustomerRelationshipManagementServiceImplementationTests : ServiceImplementationTestBase
    {
        [TestMethod]
        public void TestListCrmEmails()
        {
            //Retrieve CRM emails
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
            var crmEmails = crmQueryService.ListCrmEmails();
            Assert.IsTrue(crmEmails.Count > 0);
        }

        [TestMethod]
        public void TestListCrmEmailsByContactId()
        {
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve CRM contact
            var contacts = crmQueryService.ListContacts();
            Assert.IsTrue(contacts.Count > 0);

            //Retrieve CRM emails
            var i = 0;
            var hasEmails = false;
            while (!hasEmails)
            {
                var contact = contacts[i];
                Assert.IsNotNull(contact);
                var crmEmails = crmQueryService.ListCrmEmailsByContactId(contact.Id);
                if (crmEmails.Count > 0)
                    hasEmails = true;
                i++;
            }
            Assert.IsTrue(hasEmails);
        }

        [TestMethod]
        public void TestRetrieveFormattedCrmAppointment()
        {
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            var projectId =
                ProjectManagementServiceImplementationTests.CreateNewAssessmentDevelopmentProject(Container,
                                                                                                  "TestProject",
                                                                                                  "Lafarge",
                                                                                                  "Stijn Van Eynde",
                                                                                                  "Thomas King");

            var formattedCrmAppointment = crmQueryService.RetrieveFormattedCrmAppointment(120);

        }

        [TestMethod]
        public void TestRetrieveCrmProject()
        {
            var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

            //Retrieve all CRM projects to get an available id
            var crmProjects = crmQueryService.ListActiveProjects(6712); //DemoCompany = company for testing purposes
            Assert.IsTrue(crmProjects.Count > 0);
            var first = crmProjects.FirstOrDefault();
            Assert.IsNotNull(first);

            //Retrieve CRM project for first CRM project id
            var crmProject = crmQueryService.RetrieveCrmProject(first.Id);
            Assert.IsNotNull(crmProject);
        }
    }
}
