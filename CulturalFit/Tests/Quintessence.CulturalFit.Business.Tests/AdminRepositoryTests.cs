using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.Business.Tests.Base;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Infra.ExtensionMethods;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Business.Tests
{
    [TestClass]
    public class AdminRepositoryTests : BaseBusiness
    {
        [TestMethod]
        public void TestGetCustomerRequestByProjectId()
        {

        }

        [TestMethod]
        public void TestListProjects()
        {
            var repository = Container.Resolve<IAdminRepository>();

            //List all projects
            var projects = repository.List<Project>();
            Assert.IsTrue(projects.Count > 0);

            //List all projects with associates and contacts
            var projectsAll = repository.List<Project>(pr => pr.Include(p => p.Associate)
                                                               .Include(p => p.Contact));
            Assert.IsTrue(projectsAll.Count > 0);
        }

        [TestMethod]
        public void TestListContacts()
        {
            var repository = Container.Resolve<IAdminRepository>();

            //List all contacts
            var contacts = repository.List<Contact>();
            Assert.IsTrue(contacts.Count > 0);
        }

        [TestMethod]
        public void TestGenericList()
        {
            var repository = Container.Resolve<IAdminRepository>();
            var requests = repository.List<TheoremListRequest>(set => set.Include(s => s.TheoremLists));

            Assert.IsNotNull(requests);
        }

        [TestMethod]
        public void TestGenericGetByIdGuid()
        {
            var theoremListRepository = Container.Resolve<ITheoremListRepository>();
            var requests = theoremListRepository.ListRecentTheoremListRequests(1);
            Assert.IsTrue(requests.Count >= 1);

            var firstRequest = requests.FirstOrDefault();
            Assert.IsNotNull(firstRequest);

            var adminRepository = Container.Resolve<IAdminRepository>();
            var retrievedRequest = adminRepository.GetById<TheoremListRequest>(firstRequest.Id);

            Assert.IsNotNull(retrievedRequest);

            Assert.AreEqual(retrievedRequest.Id, firstRequest.Id);
        }

        [TestMethod]
        public void TestGenericGetByIdInt()
        {
            var theoremListRepository = Container.Resolve<ITheoremListRepository>();
            var languages = theoremListRepository.ListLanguages();
            Assert.IsTrue(languages.Count >= 1);

            var firstLanguage = languages.FirstOrDefault();
            Assert.IsNotNull(firstLanguage);

            var adminRepository = Container.Resolve<IAdminRepository>();
            var retrievedLanguage = adminRepository.GetById<Language>(firstLanguage.Id);

            Assert.IsNotNull(retrievedLanguage);

            Assert.AreEqual(retrievedLanguage.Id, firstLanguage.Id);
        }

        [TestMethod]
        public void TestGenericFilter()
        {
            var theoremListRepository = Container.Resolve<ITheoremListRepository>();
            var languages = theoremListRepository.ListLanguages();
            Assert.IsTrue(languages.Count >= 1);

            var adminRepository = Container.Resolve<IAdminRepository>();
            var retrievedLanguages = adminRepository.Filter<Language>(l => l.GlobalizationName.ToLower() == "nl");

            Assert.IsTrue(retrievedLanguages.Count >= 1);

            var first = retrievedLanguages.FirstOrDefault();
            Assert.IsNotNull(first);
            Assert.AreEqual("nl", first.GlobalizationName.ToLower());

        }

        [TestMethod]
        public void TestGenericAdd()
        {

            var adminRepository = Container.Resolve<IAdminRepository>();
            var newContactPerson = new ContactPerson
                                       {
                                           Id = Guid.NewGuid(),
                                           FirstName = "John",
                                           LastName = "Appleseed",
                                           Email = "John.Appleseed@quintessence.be",
                                           Audit = new Audit
                                                       {
                                                           CreatedBy = "tki",
                                                           CreatedOn = DateTime.Now,
                                                           IsDeleted = false,
                                                           VersionId = Guid.NewGuid()
                                                       },
                                           Gender = 1,
                                           LanguageId = 1
                                       };
            var addedContactPerson = adminRepository.Add(newContactPerson);

            Assert.IsNotNull(addedContactPerson);
            Assert.AreEqual(addedContactPerson.Id, newContactPerson.Id);
            Assert.AreEqual(addedContactPerson.FirstName, newContactPerson.FirstName);
            Assert.AreEqual(addedContactPerson.LastName, newContactPerson.LastName);
            Assert.AreEqual(addedContactPerson.Email, newContactPerson.Email);
            Assert.AreEqual(addedContactPerson.Audit, newContactPerson.Audit);
            Assert.AreEqual(addedContactPerson.Gender, newContactPerson.Gender);
            Assert.AreEqual(addedContactPerson.LanguageId, newContactPerson.LanguageId);

        }

        [TestMethod]
        public void TestHasTheoremListRequests()
        {
            var repository = Container.Resolve<IAdminRepository>();

            var contacts = repository.List<Contact>();
            Assert.IsTrue(contacts.Count > 0);

            var hasRequest = false;
            var count = 0;

            while (!hasRequest)
            {
                var retrievedContact = contacts.ElementAt(count);
                Assert.IsNotNull(retrievedContact);

                var contact = repository.RetrieveContactWithRequests(retrievedContact.Id);
                Assert.IsNotNull(contact);
                Assert.AreEqual(contact.Id, retrievedContact.Id);
                if (repository.HasTheoremListRequests(contact.Id))
                    hasRequest = true;

                if (count >= contacts.Count)
                    break;

                count++;
            }

            Assert.IsTrue(hasRequest, "No contacts found that have a request! (to succeed this test, make a request for a contact)");


        }

        [TestMethod]
        public void TestSearchContactByProjectId()
        {
            var repository = Container.Resolve<IAdminRepository>();

            //List all projects
            var projects = repository.List<Project>(pr => pr.Include(p => p.Contact));
            Assert.IsTrue(projects.Count > 0);

            //Get the first project of that list
            var project = projects.FirstOrDefault();
            Assert.IsNotNull(project);
            //Check if contact of that project is not null
            Assert.IsNotNull(project.Contact);

            //Search contact by that project id
            var contact = repository.RetrieveContactByProjectId(project.Id);
            Assert.IsNotNull(contact);
            Assert.AreEqual(project.Contact.Name, contact.Name);
        }

        [TestMethod]
        public void TestRetrieveContactById()
        {
            var repository = Container.Resolve<IAdminRepository>();

            //List all contacts
            var contacts = repository.List<Contact>();
            Assert.IsTrue(contacts.Count > 0);

            var firstContact = contacts.FirstOrDefault();
            Assert.IsNotNull(firstContact);

            var retrievedContact = repository.GetById<Contact>(firstContact.Id);
            Assert.IsNotNull(retrievedContact);
            Assert.AreEqual(retrievedContact.Id, firstContact.Id);
        }

        [TestMethod]
        public void TestRetrieveContactWithRequests()
        {
            var repository = Container.Resolve<IAdminRepository>();

            //List all contacts
            var contacts = repository.List<Contact>();
            Assert.IsTrue(contacts.Count > 0);

            var hasRequest = false;
            var count = 0;

            while (!hasRequest)
            {
                var retrievedContact = contacts.ElementAt(count);
                Assert.IsNotNull(retrievedContact);

                var contact = repository.RetrieveContactWithRequests(retrievedContact.Id);
                Assert.IsNotNull(contact);
                Assert.AreEqual(contact.Id, retrievedContact.Id);
                if (contact.TheoremListRequests.Count >= 1)
                    hasRequest = true;

                if (count >= contacts.Count)
                    break;

                count++;
            }

            Assert.IsTrue(hasRequest, "No contacts found that have a request! (to succeed this test, make a request for a contact)");

        }

    }
}
