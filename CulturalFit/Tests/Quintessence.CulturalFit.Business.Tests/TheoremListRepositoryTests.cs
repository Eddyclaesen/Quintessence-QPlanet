using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.Business.Tests.Base;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.Business.Tests
{
    [TestClass]
    public class TheoremListRepositoryTests : BaseBusiness
    {
        [TestMethod]
        public void TestListTemplates()
        {
            var repository = Container.Resolve<ITheoremListRepository>();

            try
            {
                //Retrieve all templates
                var templates = repository.ListTemplates();

                Assert.IsTrue(templates.Count > 0);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void TestListLanguages()
        {
            var repository = Container.Resolve<ITheoremListRepository>();

            try
            {
                //Retrieve all languages
                var languages = repository.ListLanguages();

                Assert.IsTrue(languages.Count > 0);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        //[TestMethod]
        //public void TestFillInTheoremList()
        //{
        //    var theoremListRepository = Container.Resolve<ITheoremListRepository>();
        //    var crmRepository = Container.Resolve<ICrmRepository>();

        //    try
        //    {
        //        var templates = theoremListRepository.ListTemplates();
        //        Assert.IsNotNull(templates);

        //        var template = templates.FirstOrDefault();
        //        Assert.IsNotNull(template);

        //        var types = theoremListRepository.ListTheoremListRequestTypes();
        //        Assert.IsNotNull(types);

        //        var type = types.FirstOrDefault(t => t.Id == 1);
        //        Assert.IsNotNull(type);

        //        var contacts = crmRepository.ListContacts();
        //        Assert.IsNotNull(contacts);

        //        var contact = contacts.FirstOrDefault(c => c.Id == 3);
        //        Assert.IsNotNull(contact);

        //        var theoremListRequest = theoremListRepository.PrepareTheoremListRequest(template.Id, contact.Id, contact.Name, type.Id);

        //        theoremListRepository.Save(theoremListRequest);

        //        theoremListRequest = theoremListRepository.RetrieveTheoremListRequest(theoremListRequest.Id);

        //        Assert.IsNotNull(theoremListRequest);

        //        Assert.AreEqual(3, theoremListRequest.ContactId);
        //        Assert.AreEqual(1, theoremListRequest.TheoremListRequestTypeId);

        //        Assert.IsNotNull(theoremListRequest.TheoremLists);
        //        Assert.AreEqual(1, theoremListRequest.TheoremLists.Count);

        //        var theoremList = theoremListRequest.TheoremLists.FirstOrDefault();

        //        Assert.IsNotNull(theoremList);

        //        theoremList = theoremListRepository.RetrieveTheoremList(theoremList.Id);

        //        //Mark 5 theorems as most applicable
        //        var mostApplicableIds = new List<Guid>();
        //        foreach (var theorem in theoremList.Theorems.Take(5))
        //        {
        //            mostApplicableIds.Add(theorem.Id);
        //            theorem.IsMostApplicable = true;
        //        }

        //        //Mark 5 theorems as least applicable
        //        var leastApplicableIds = new List<Guid>();
        //        foreach (var theorem in theoremList.Theorems.Skip(10).Take(5))
        //        {
        //            leastApplicableIds.Add(theorem.Id);
        //            theorem.IsLeastApplicable = true;
        //        }

        //        theoremListRepository.Save(theoremList);

        //        //Get the theorem list by the id of the newly created theorem list
        //        var retrievedTheoremList = theoremListRepository.RetrieveTheoremList(theoremList.Id);

        //        Assert.IsNotNull(retrievedTheoremList);

        //        //Check if the selected theorems are filled in
        //        Assert.IsFalse(retrievedTheoremList.Theorems.Where(t => t.IsMostApplicable).Any(t => !mostApplicableIds.Contains(t.Id)));
        //        Assert.AreEqual(5, retrievedTheoremList.Theorems.Count(t => t.IsMostApplicable));

        //        //Check if the selected theorems are filled in
        //        Assert.IsFalse(retrievedTheoremList.Theorems.Where(t => t.IsLeastApplicable).Any(t => !leastApplicableIds.Contains(t.Id)));
        //        Assert.AreEqual(5, retrievedTheoremList.Theorems.Count(t => t.IsLeastApplicable));
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.Message);
        //    }
        //}

        //[TestMethod]
        //public void PrepareSeveralRequests()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        TestPrepareTheoremListRequestAsIsEmployee();
        //        TestPrepareTheoremListRequestAsIsNoEmployees();
        //        TestPrepareTheoremListRequestAsIsToBeNoEmployees();
        //    }
        //}

        //[TestMethod]
        //public void TestPrepareTheoremListRequestAsIsEmployee()
        //{
        //    var theoremListRepository = Container.Resolve<ITheoremListRepository>();
        //    var crmRepository = Container.Resolve<ICrmRepository>();

        //    try
        //    {
        //        var templates = theoremListRepository.ListTemplates();
        //        Assert.IsNotNull(templates);

        //        var template = templates.FirstOrDefault();
        //        Assert.IsNotNull(template);

        //        var types = theoremListRepository.ListTheoremListRequestTypes();
        //        Assert.IsNotNull(types);

        //        var type = types.FirstOrDefault(t => t.Id == 1);
        //        Assert.IsNotNull(type);

        //        var contacts = crmRepository.ListContacts();
        //        Assert.IsNotNull(contacts);

        //        var contact = contacts.FirstOrDefault(c => c.Id == 880);
        //        Assert.IsNotNull(contact);

        //        var candidate = theoremListRepository.PrepareTheoremListCandidate("John", "Wilkinson", contact.Id);
        //        Assert.IsNotNull(candidate);
        //        Assert.AreEqual("John", candidate.FirstName);
        //        Assert.AreEqual("Wilkinson", candidate.LastName);

        //        var theoremListRequest = theoremListRepository.PrepareTheoremListRequest(template.Id, contact.Id, contact.Name, type.Id);
        //        theoremListRequest.TheoremListCandidate = candidate;
        //        theoremListRequest.TheoremListCandidateId = candidate.Id;

        //        theoremListRepository.Save(theoremListRequest);

        //        theoremListRequest = theoremListRepository.RetrieveTheoremListRequest(theoremListRequest.Id);

        //        Assert.IsNotNull(theoremListRequest);

        //        Assert.AreEqual(contact.Id, theoremListRequest.ContactId);
        //        Assert.AreEqual(type.Id, theoremListRequest.TheoremListRequestTypeId);

        //        Assert.IsNotNull(theoremListRequest.TheoremLists);
        //        Assert.AreEqual(1, theoremListRequest.TheoremLists.Count);

        //        var asIsTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 1);
        //        Assert.IsNotNull(asIsTheoremList);

        //        var toBeTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 2);
        //        Assert.IsNull(toBeTheoremList);
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.Message);
        //    }
        //}

        //[TestMethod]
        //public void TestPrepareTheoremListRequestAsIsNoEmployees()
        //{
        //    var theoremListRepository = Container.Resolve<ITheoremListRepository>();
        //    var crmRepository = Container.Resolve<ICrmRepository>();

        //    try
        //    {
        //        var templates = theoremListRepository.ListTemplates();
        //        Assert.IsNotNull(templates);

        //        var template = templates.FirstOrDefault();
        //        Assert.IsNotNull(template);

        //        var types = theoremListRepository.ListTheoremListRequestTypes();
        //        Assert.IsNotNull(types);

        //        var type = types.FirstOrDefault(t => t.Id == 1);
        //        Assert.IsNotNull(type);

        //        var contacts = crmRepository.ListContacts();
        //        Assert.IsNotNull(contacts);

        //        var contact = contacts.FirstOrDefault(c => c.Id == 880);
        //        Assert.IsNotNull(contact);

        //        var theoremListRequest = theoremListRepository.PrepareTheoremListRequest(template.Id, contact.Id, contact.Name, type.Id);

        //        theoremListRepository.Save(theoremListRequest);

        //        theoremListRequest = theoremListRepository.RetrieveTheoremListRequest(theoremListRequest.Id);

        //        Assert.IsNotNull(theoremListRequest);

        //        Assert.AreEqual(contact.Id, theoremListRequest.ContactId);
        //        Assert.AreEqual(type.Id, theoremListRequest.TheoremListRequestTypeId);

        //        Assert.IsNotNull(theoremListRequest.TheoremLists);
        //        Assert.AreEqual(1, theoremListRequest.TheoremLists.Count);

        //        var asIsTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 1);
        //        Assert.IsNotNull(asIsTheoremList);

        //        var toBeTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 2);
        //        Assert.IsNull(toBeTheoremList);
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.Message);
        //    }
        //}

        //[TestMethod]
        //public void TestPrepareTheoremListRequestAsIsToBeNoEmployees()
        //{
        //    var theoremListRepository = Container.Resolve<ITheoremListRepository>();
        //    var crmRepository = Container.Resolve<ICrmRepository>();

        //    try
        //    {
        //        var templates = theoremListRepository.ListTemplates();
        //        Assert.IsNotNull(templates);

        //        var template = templates.FirstOrDefault();
        //        Assert.IsNotNull(template);

        //        var languages = theoremListRepository.ListLanguages();
        //        Assert.IsNotNull(languages);

        //        var language = languages.FirstOrDefault(l => l.Id == 1);
        //        Assert.IsNotNull(language);

        //        var types = theoremListRepository.ListTheoremListRequestTypes();
        //        Assert.IsNotNull(types);

        //        var type = types.FirstOrDefault(t => t.Id == 2);
        //        Assert.IsNotNull(type);

        //        var contacts = crmRepository.ListContacts();
        //        Assert.IsNotNull(contacts);

        //        var contact = contacts.FirstOrDefault(c => c.Id == 880);
        //        Assert.IsNotNull(contact);

        //        var theoremListRequest = theoremListRepository.PrepareTheoremListRequest(template.Id, contact.Id, contact.Name, type.Id);

        //        theoremListRepository.Save(theoremListRequest);

        //        theoremListRequest = theoremListRepository.RetrieveTheoremListRequest(theoremListRequest.Id);

        //        Assert.IsNotNull(theoremListRequest);

        //        Assert.AreEqual(contact.Id, theoremListRequest.ContactId);
        //        Assert.AreEqual(type.Id, theoremListRequest.TheoremListRequestTypeId);

        //        Assert.IsNotNull(theoremListRequest.TheoremLists);
        //        Assert.AreEqual(2, theoremListRequest.TheoremLists.Count);

        //        var asIsTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 1);
        //        Assert.IsNotNull(asIsTheoremList);

        //        var toBeTheoremList = theoremListRequest.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 2);
        //        Assert.IsNotNull(toBeTheoremList);
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.Message);
        //    }
        //}

        //[TestMethod]
        //public void TestSearchTheoremListRequests()
        //{
        //    var theoremListRepository = Container.Resolve<ITheoremListRepository>();
        //    var crmRepository = Container.Resolve<ICrmRepository>();

        //    try
        //    {

        //        var templates = theoremListRepository.ListTemplates();
        //        Assert.IsNotNull(templates);

        //        var template = templates.FirstOrDefault();
        //        Assert.IsNotNull(template);

        //        var languages = theoremListRepository.ListLanguages();
        //        Assert.IsNotNull(languages);

        //        var language = languages.FirstOrDefault(l => l.Id == 1);
        //        Assert.IsNotNull(language);

        //        var types = theoremListRepository.ListTheoremListRequestTypes();
        //        Assert.IsNotNull(types);

        //        var type = types.FirstOrDefault(t => t.Id == 2);
        //        Assert.IsNotNull(type);

        //        var contacts = crmRepository.ListContacts();
        //        Assert.IsNotNull(contacts);

        //        var contact = contacts.FirstOrDefault(c => c.Id == 3);
        //        Assert.IsNotNull(contact);


        //        var theoremListRequest = theoremListRepository.PrepareTheoremListRequest(template.Id, contact.Id, contact.Name, type.Id);
        //        theoremListRepository.Save(theoremListRequest);
        //        var searchedRequests = theoremListRepository.SearchTheoremListRequests(theoremListRequest.VerificationCode,
        //                                                                               "",
        //                                                                               "",
        //                                                                               1);
        //        Assert.IsTrue(searchedRequests.Count == 1);

        //        var singleSearchedRequest = searchedRequests.Single();
        //        Assert.IsNotNull(singleSearchedRequest);

        //        Assert.AreEqual(singleSearchedRequest.VerificationCode, theoremListRequest.VerificationCode);
        //        Assert.AreEqual(singleSearchedRequest.Contact.Name, theoremListRequest.Contact.Name);
        //        //Assert.AreEqual(singleSearchedRequest.TheoremListCandidate.FullName, theoremListRequest.TheoremListCandidate.FullName);


        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.Message);
        //    }
        //}
    }
}
