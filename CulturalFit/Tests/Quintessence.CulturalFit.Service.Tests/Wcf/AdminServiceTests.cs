using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Reports;
using Quintessence.CulturalFit.Service.Contracts.DataContracts;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.Service.Tests.Wcf.Base;

namespace Quintessence.CulturalFit.Service.Tests.Wcf
{
    [TestClass]
    public class AdminServiceTests
    {
        [TestMethod]
        public void TestListContacts()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var contacts = service.ListContacts();
            Assert.IsTrue(contacts.Count >= 1);
        }

        [TestMethod]
        public void TestListProjects()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var projects = service.ListProjects();
            Assert.IsTrue(projects.Count >= 1);
        }

        [TestMethod]
        public void TestListContactPersons()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var contactPersons = service.ListContactPersons();
            Assert.IsTrue(contactPersons.Count >= 1);
        }

        [TestMethod]
        public void TestListParticipants()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var contactPersons = service.ListParticipants(8801);
            Assert.IsTrue(contactPersons.Count >= 1);
        }

        [TestMethod]
        public void TestRetrieveSettingByKey()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var setting = service.RetrieveSettingByKey("mailserver");
            Assert.IsNotNull(setting);
            Assert.IsTrue(setting.Key.ToLower() == "mailserver");
        }

        [TestMethod]
        public void TestGenerateReport()
        {
            var request = FindCompletedTheoremListRequest();
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var result = service.GenerateReport(request.Id, 1, OutputFormat.Pdf);
            Assert.IsNotNull(result);

            byte[] bytes = Convert.FromBase64String(result);
            Assert.IsNotNull(bytes);

            const string filepath = @"C:\Temp\test.pdf";
            System.IO.File.WriteAllBytes(filepath,bytes);

            Assert.IsTrue(System.IO.File.Exists(filepath));

        }

        [TestMethod]
        public void TestCreateCustomerRequest()
        {

            var service = ServiceFactory.CreateChannel<IAdminService>();
            var customerRequest = new CustomerRequest
                {
                    ContactId = 3,
                    ContactPersonFirstName = "John",
                    ContactPersonLastName = "Appleseed",
                    ContactPersonEmail = "john.appleseed@testing.be",
                    ContactPersonGender = 1,
                    Deadline = DateTime.Now.AddDays(1),
                    LanguageId = 1,
                    TheoremListRequestTypeId = 2,
                };
            var request = service.CreateCustomerRequest(customerRequest);
            Assert.IsNotNull(request);
            Assert.IsNull(request.TheoremListCandidate);
            Assert.AreEqual("John", request.ContactPerson.FirstName);
            Assert.AreEqual("Appleseed", request.ContactPerson.LastName);
            Assert.AreEqual("john.appleseed@testing.be", request.ContactPerson.Email);
            Assert.AreEqual(1, request.ContactPerson.Gender);
            Assert.AreEqual(DateTime.Now.AddDays(1).ToShortDateString(), request.Deadline.GetValueOrDefault().ToShortDateString());
            Assert.AreEqual(1, request.ContactPerson.LanguageId);
            Assert.AreEqual(DateTime.Now.ToShortDateString(), request.RequestDate.ToShortDateString());
            Assert.AreEqual(2, request.TheoremListRequestTypeId);
            Assert.AreEqual(3, request.ContactId);
        }

        [TestMethod]
        public void TestCreateCandidateRequest()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var candidateRequest = new CandidateRequest
            {
                ContactId = 3,
                FirstName = "John",
                LastName = "Appleseed",
                Deadline = DateTime.Now.AddDays(1),
                TheoremListRequestTypeId = 1,
            };
            var candidateRequests = new List<CandidateRequest> {candidateRequest};
            var request = new TheoremListRequest();
                service.CreateCandidateRequests(candidateRequests, 2295);
            Assert.IsNotNull(request);
            Assert.AreEqual("John", request.TheoremListCandidate.FirstName);
            Assert.AreEqual("Appleseed", request.TheoremListCandidate.LastName);
            Assert.IsNull(request.ContactPerson);
            Assert.AreEqual(DateTime.Now.AddDays(1).ToShortDateString(), request.Deadline.GetValueOrDefault().ToShortDateString());
            Assert.AreEqual(DateTime.Now.ToShortDateString(), request.RequestDate.ToShortDateString());
            Assert.AreEqual(1, request.TheoremListRequestTypeId);
            Assert.AreEqual(3, request.ContactId);
        }

        public TheoremListRequest FindCompletedTheoremListRequest()
        {
            var service = ServiceFactory.CreateChannel<IAdminService>();
            var contacts = service.ListContacts();
            Assert.IsTrue(contacts.Count >= 1);

            //retrieve first contact that has theoremlist requests
            var contact = contacts.FirstOrDefault(c => service.HasTheoremListRequests(c.Id));
            Assert.IsNotNull(contact);

            //retrieve contact with underlying requests
            var contactWithRequests = service.RetrieveContactWithRequests(contact.Id);
            Assert.IsNotNull(contactWithRequests);

            var requests = contactWithRequests.TheoremListRequests;
            Assert.IsNotNull(requests);

            var requestCompleted = requests.FirstOrDefault(r => r.TheoremLists.All(tl => tl.IsCompleted));
            Assert.IsNotNull(requestCompleted);

            return requestCompleted;
        }
    }
}
