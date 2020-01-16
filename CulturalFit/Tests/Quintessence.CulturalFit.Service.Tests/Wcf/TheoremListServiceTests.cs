//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Quintessence.CulturalFit.Service.Contracts.DataContracts;
//using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
//using Quintessence.CulturalFit.Service.Tests.Wcf.Base;

//namespace Quintessence.CulturalFit.Service.Tests.Wcf
//{
//    [TestClass]
//    public class TheoremListServiceTests
//    {
//        [TestMethod]
//        public void TestListTemplates()
//        {
//            try
//            {
//                var theoremListService = ServiceFactory.CreateChannel<ITheoremListService>();

//                var templates = theoremListService.ListTemplates();

//                Assert.IsNotNull(templates);
//            }
//            catch (Exception exception)
//            {
//                Assert.Fail(exception.Message + Environment.NewLine + exception.StackTrace);
//            }
//        }

//        [TestMethod]
//        public void TestCompleteFlowCustomerAsIsNoEmployees()
//        {
//            try
//            {
//                var theoremListService = ServiceFactory.CreateChannel<ITheoremListService>();
//                var crmService = ServiceFactory.CreateChannel<ICrmService>();

//                var templates = theoremListService.ListTemplates();
//                Assert.IsNotNull(templates);

//                var selectedTemplate = templates.FirstOrDefault();
//                Assert.IsNotNull(selectedTemplate);

//                var contacts = crmService.ListContacts();
//                Assert.IsNotNull(contacts);

//                var selectedContact = contacts.FirstOrDefault(c => c.Id == 3);
//                Assert.IsNotNull(selectedContact);

//                var types = theoremListService.RetrieveTheoremListRequestTypes();
//                Assert.IsNotNull(types);

//                var selectedType = types.FirstOrDefault(t => t.Id == 1); //As Is
//                Assert.IsNotNull(selectedType);

//                var request = theoremListService.CreateTheoremListRequest(new ContactRequest
//                                                                    {
//                                                                        ContactId = selectedContact.Id,
//                                                                        ContactName = selectedContact.Name,
//                                                                        TemplateId = selectedTemplate.Id,
//                                                                        Type = selectedType.Id
//                                                                    });
                
//                Assert.IsNotNull(request);

//                Assert.AreEqual(selectedContact.Id, request.ContactId);
//                Assert.IsNotNull(request.Contact);

//                Assert.AreEqual(selectedType.Id, request.TheoremListRequestTypeId);
//                Assert.IsNotNull(request.TheoremListRequestType);

//                var theoremList = request.TheoremLists.Single();
//                Assert.IsNotNull(theoremList);

//                //Validate TheoremList with selected template
//                var validationTemplate = theoremListService.RetrieveTemplate(selectedTemplate.Id);
//                Assert.IsNotNull(validationTemplate);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, theoremList.Theorems.Count);
//                Assert.IsFalse(theoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));

//                request = theoremListService.SaveTheoremListRequest(request);

//                Assert.IsNotNull(request);

//                Assert.AreEqual(selectedContact.Id, request.ContactId);
//                Assert.IsNotNull(request.Contact);

//                Assert.AreEqual(selectedType.Id, request.TheoremListRequestTypeId);
//                Assert.IsNotNull(request.TheoremListRequestType);

//                theoremList = request.TheoremLists.Single();
//                Assert.IsNotNull(theoremList);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, theoremList.Theorems.Count);
//                Assert.IsFalse(theoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));

//                theoremList.Theorems.Take(5).ToList().ForEach(t => t.IsMostApplicable = true);
//                theoremList.Theorems.Skip(5).Take(5).ToList().ForEach(t => t.IsLeastApplicable = true);

//                theoremListService.SaveTheoremList(theoremList);

//                request = theoremListService.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(request.Id));

//                Assert.IsNotNull(request);

//                theoremList = request.TheoremLists.FirstOrDefault(tl => tl.Id == theoremList.Id);

//                Assert.IsNotNull(theoremList);
//                Assert.AreEqual(5, theoremList.Theorems.Count(t => t.IsLeastApplicable));
//                Assert.AreEqual(5, theoremList.Theorems.Count(t => t.IsMostApplicable));
//            }
//            catch (Exception exception)
//            {
//                Assert.Fail(exception.Message + Environment.NewLine + exception.StackTrace);
//            }
//        }

//        [TestMethod]
//        public void TestCompleteFlowCustomerAsIsToBeNoEmployees()
//        {
//            try
//            {
//                var theoremListService = ServiceFactory.CreateChannel<ITheoremListService>();
//                var crmService = ServiceFactory.CreateChannel<ICrmService>();

//                var templates = theoremListService.ListTemplates();
//                Assert.IsNotNull(templates);

//                var selectedTemplate = templates.FirstOrDefault();
//                Assert.IsNotNull(selectedTemplate);

//                var contacts = crmService.ListContacts();
//                Assert.IsNotNull(contacts);

//                var selectedContact = contacts.FirstOrDefault(c => c.Id == 3);
//                Assert.IsNotNull(selectedContact);

//                var types = theoremListService.RetrieveTheoremListRequestTypes();
//                Assert.IsNotNull(types);

//                var selectedType = types.FirstOrDefault(t => t.Id == 2); //As Is & To Be
//                Assert.IsNotNull(selectedType);

//                var languages = theoremListService.ListLanguages();
//                Assert.IsNotNull(languages);

//                var selectedLanguage = languages.FirstOrDefault(l => l.Id == 1); //Nederlands
//                Assert.IsNotNull(selectedLanguage);

//                var requests = theoremListService.CreateTheoremListRequest(new ContactRequest
//                {
//                    ContactId = selectedContact.Id,
//                    ContactName = selectedContact.Name,
//                    TemplateId = selectedTemplate.Id,
//                    LanguageId = selectedLanguage.Id,
//                    Type = selectedType.Id
//                });

//                Assert.IsNotNull(requests);

//                var request = requests.FirstOrDefault();

//                Assert.IsNotNull(request);

//                Assert.AreEqual(selectedContact.Id, request.ContactId);
//                Assert.IsNotNull(request.Contact);

//                Assert.AreEqual(selectedType.Id, request.TheoremListRequestTypeId);
//                Assert.IsNotNull(request.TheoremListRequestType);

//                //Validate TheoremList with selected template
//                var validationTemplate = theoremListService.RetrieveTemplate(selectedTemplate.Id);
//                Assert.IsNotNull(validationTemplate);

//                requests = theoremListService.SaveTheoremListRequests(requests);

//                request = requests.Single(r => r.Id == request.Id);

//                //Validate the As Is TheoremList
//                var asIsTheoremList = request.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 1);
//                Assert.IsNotNull(asIsTheoremList);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, asIsTheoremList.Theorems.Count);
//                Assert.IsFalse(asIsTheoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));

//                asIsTheoremList.Theorems.Take(5).ToList().ForEach(t => t.IsMostApplicable = true);
//                asIsTheoremList.Theorems.Skip(5).Take(5).ToList().ForEach(t => t.IsLeastApplicable = true);

//                theoremListService.SaveTheoremList(asIsTheoremList);

//                //Validate the To Be TheoremList
//                var toBeTheoremList = request.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 2);
//                Assert.IsNotNull(toBeTheoremList);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, toBeTheoremList.Theorems.Count);
//                Assert.IsFalse(toBeTheoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));

//                toBeTheoremList.Theorems.Take(5).ToList().ForEach(t => t.IsMostApplicable = true);
//                toBeTheoremList.Theorems.Skip(5).Take(5).ToList().ForEach(t => t.IsLeastApplicable = true);

//                theoremListService.SaveTheoremList(toBeTheoremList);

//                //Validate filled in theorems
//                request = theoremListService.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(request.Id));

//                Assert.IsNotNull(request);

//                asIsTheoremList = request.TheoremLists.FirstOrDefault(tl => tl.Id == asIsTheoremList.Id);
//                Assert.IsNotNull(asIsTheoremList);

//                Assert.IsNotNull(asIsTheoremList);
//                Assert.AreEqual(5, asIsTheoremList.Theorems.Count(t => t.IsLeastApplicable));
//                Assert.AreEqual(5, asIsTheoremList.Theorems.Count(t => t.IsMostApplicable));

//                toBeTheoremList = request.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 2);
//                Assert.IsNotNull(toBeTheoremList);

//                Assert.IsNotNull(toBeTheoremList);
//                Assert.AreEqual(5, toBeTheoremList.Theorems.Count(t => t.IsLeastApplicable));
//                Assert.AreEqual(5, toBeTheoremList.Theorems.Count(t => t.IsMostApplicable));
//            }
//            catch (Exception exception)
//            {
//                Assert.Fail(exception.Message + Environment.NewLine + exception.StackTrace);
//            }
//        }

//        [TestMethod]
//        public void TestCompleteFlowCustomerAsIsWithEmployees()
//        {
//            try
//            {
//                var theoremListService = ServiceFactory.CreateChannel<ITheoremListService>();
//                var crmService = ServiceFactory.CreateChannel<ICrmService>();

//                var templates = theoremListService.ListTemplates();
//                Assert.IsNotNull(templates);

//                var selectedTemplate = templates.FirstOrDefault();
//                Assert.IsNotNull(selectedTemplate);

//                var contacts = crmService.ListContacts();
//                Assert.IsNotNull(contacts);

//                var selectedContact = contacts.FirstOrDefault(c => c.Id == 3);
//                Assert.IsNotNull(selectedContact);

//                var types = theoremListService.RetrieveTheoremListRequestTypes();
//                Assert.IsNotNull(types);

//                var selectedType = types.FirstOrDefault(t => t.Id == 1); //As Is
//                Assert.IsNotNull(selectedType);

//                var asIsType = types.FirstOrDefault(t => t.Id == 1);
//                Assert.IsNotNull(asIsType);

//                var requests = theoremListService.CreateTheoremListRequest(new ContactRequest
//                {
//                    ContactId = selectedContact.Id,
//                    ContactName = selectedContact.Name,
//                    TemplateId = selectedTemplate.Id,
//                    Type = selectedType.Id
//                });

//                Assert.IsNotNull(requests);

//                Assert.AreEqual(5, requests.Count);
//                Assert.AreEqual(5, requests.SelectMany(r => r.TheoremLists).Count());//1 for company, 1 per employee

//                var request = requests.FirstOrDefault(r => r.TheoremListCandidateId.HasValue);

//                Assert.IsNotNull(request);
//                Assert.IsNotNull(request.TheoremListCandidate);

//                Assert.AreEqual(selectedContact.Id, request.ContactId);
//                Assert.IsNotNull(request.Contact);

//                Assert.AreEqual(selectedType.Id, request.TheoremListRequestTypeId);
//                Assert.IsNotNull(request.TheoremListRequestType);

//                var theoremList = request.TheoremLists.Single();
//                Assert.IsNotNull(theoremList);

//                //Validate TheoremList with selected template
//                var validationTemplate = theoremListService.RetrieveTemplate(selectedTemplate.Id);
//                Assert.IsNotNull(validationTemplate);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, theoremList.Theorems.Count);
//                Assert.IsFalse(theoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));
                
//                requests = theoremListService.SaveTheoremListRequests(requests);

//                request = requests.Single(r => r.Id == request.Id);

//                Assert.IsNotNull(request);
//                Assert.IsNotNull(request.TheoremListCandidate);

//                Assert.AreEqual(selectedContact.Id, request.ContactId);
//                Assert.IsNotNull(request.Contact);

//                Assert.AreEqual(asIsType.Id, request.TheoremListRequestTypeId);
//                Assert.IsNotNull(request.TheoremListRequestType);

//                theoremList = request.TheoremLists.Single();
//                Assert.IsNotNull(theoremList);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, theoremList.Theorems.Count);
//                Assert.IsFalse(theoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));

//                theoremList.Theorems.Take(5).ToList().ForEach(t => t.IsMostApplicable = true);
//                theoremList.Theorems.Skip(5).Take(5).ToList().ForEach(t => t.IsLeastApplicable = true);

//                theoremListService.SaveTheoremList(theoremList);

//                request = theoremListService.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(request.Id));

//                Assert.IsNotNull(request);

//                theoremList = request.TheoremLists.FirstOrDefault(tl => tl.Id == theoremList.Id);

//                Assert.IsNotNull(theoremList);
//                Assert.AreEqual(5, theoremList.Theorems.Count(t => t.IsLeastApplicable));
//                Assert.AreEqual(5, theoremList.Theorems.Count(t => t.IsMostApplicable));
//            }
//            catch (Exception exception)
//            {
//                Assert.Fail(exception.Message + Environment.NewLine + exception.StackTrace);
//            }
//        }

//        [TestMethod]
//        public void TestCompleteFlowCustomerAsIsToBeWithEmployees()
//        {
//            try
//            {
//                var theoremListService = ServiceFactory.CreateChannel<ITheoremListService>();
//                var crmService = ServiceFactory.CreateChannel<ICrmService>();

//                var templates = theoremListService.ListTemplates();
//                Assert.IsNotNull(templates);

//                var selectedTemplate = templates.FirstOrDefault();
//                Assert.IsNotNull(selectedTemplate);

//                var contacts = crmService.ListContacts();
//                Assert.IsNotNull(contacts);

//                var selectedContact = contacts.FirstOrDefault(c => c.Id == 3);
//                Assert.IsNotNull(selectedContact);

//                var types = theoremListService.RetrieveTheoremListRequestTypes();
//                Assert.IsNotNull(types);

//                var selectedType = types.FirstOrDefault(t => t.Id == 2); //As Is & To Be
//                Assert.IsNotNull(selectedType);

//                var asIsType = types.FirstOrDefault(t => t.Id == 1);
//                Assert.IsNotNull(asIsType);

//                var languages = theoremListService.ListLanguages();
//                Assert.IsNotNull(languages);

//                var selectedLanguage = languages.FirstOrDefault(l => l.Id == 1); //Nederlands
//                Assert.IsNotNull(selectedLanguage);

//                var requests = theoremListService.CreateTheoremListRequest(new ContactRequest
//                {
//                    ContactId = selectedContact.Id,
//                    ContactName = selectedContact.Name,
//                    TemplateId = selectedTemplate.Id,
//                    LanguageId = selectedLanguage.Id,
//                    Type = selectedType.Id
//                });

//                Assert.IsNotNull(requests);

//                Assert.AreEqual(5, requests.Count);

//                Assert.AreEqual(6, requests.SelectMany(r => r.TheoremLists).Count()); //2 for company, 1 per employee

//                var request = requests.FirstOrDefault(r => r.TheoremListCandidateId.HasValue);

//                Assert.IsNotNull(request);
//                Assert.IsNotNull(request.TheoremListCandidate);

//                Assert.AreEqual(selectedContact.Id, request.ContactId);
//                Assert.IsNotNull(request.Contact);

//                Assert.AreEqual(asIsType.Id, request.TheoremListRequestTypeId);//Emplyee only have as is
//                Assert.IsNotNull(request.TheoremListRequestType);

//                //Validate TheoremList with selected template
//                var validationTemplate = theoremListService.RetrieveTemplate(selectedTemplate.Id);
//                Assert.IsNotNull(validationTemplate);

//                requests = theoremListService.SaveTheoremListRequests(requests);

//                request = requests.Single(r => r.Id == request.Id);

//                Assert.IsNotNull(request);
//                Assert.IsNotNull(request.TheoremListCandidate);

//                //Validate the As Is TheoremList
//                var asIsTheoremList = request.TheoremLists.FirstOrDefault(tl => tl.TheoremListTypeId == 1);
//                Assert.IsNotNull(asIsTheoremList);

//                Assert.AreEqual(validationTemplate.TheoremTemplates.Count, asIsTheoremList.Theorems.Count);
//                Assert.IsFalse(asIsTheoremList.Theorems.Any(t => t.TheoremTranslations.Count == 0));

//                asIsTheoremList.Theorems.Take(5).ToList().ForEach(t => t.IsMostApplicable = true);
//                asIsTheoremList.Theorems.Skip(5).Take(5).ToList().ForEach(t => t.IsLeastApplicable = true);

//                theoremListService.SaveTheoremList(asIsTheoremList);

//                //Validate filled in theorems
//                request = theoremListService.RetrieveTheoremListRequest(new RetrieveTheoremListRequestRequest(request.Id));

//                Assert.IsNotNull(request);

//                asIsTheoremList = request.TheoremLists.FirstOrDefault(tl => tl.Id == asIsTheoremList.Id);
//                Assert.IsNotNull(asIsTheoremList);

//                Assert.IsNotNull(asIsTheoremList);
//                Assert.AreEqual(5, asIsTheoremList.Theorems.Count(t => t.IsLeastApplicable));
//                Assert.AreEqual(5, asIsTheoremList.Theorems.Count(t => t.IsMostApplicable));
//            }
//            catch (AssertFailedException)
//            {
//                throw;
//            }
//            catch (Exception exception)
//            {
//                Assert.Fail(exception.Message + Environment.NewLine + exception.StackTrace);
//            }
//        }
//    }
//}