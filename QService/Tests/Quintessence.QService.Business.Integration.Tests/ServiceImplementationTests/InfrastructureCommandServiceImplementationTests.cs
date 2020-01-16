using System.Linq;
using AutoMapper;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class InfrastructureCommandServiceImplementationTests : CommandServiceImplementationTestBase<IInfrastructureCommandService>
    {
        [TestMethod]
        public void TestCreateMailTemplate()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            const string name = "MyMailTemplate",
                         code = "TESTTEMPLATE",
                         fromAddress = "thomas.king@quintessence.be",
                         storedProcedureName = "RetrieveInformation",
                         bccAddress = "eddy.claesen@quintessence.be";

            var createRequest = new CreateMailTemplateRequest
                {
                    Name = name,
                    Code = code,
                    FromAddress = fromAddress,
                    BccAddress = bccAddress,
                    StoredProcedureName = storedProcedureName
                };
            var createdId = service.CreateMailTemplate(createRequest);

            //Retrieve created MailTemplate
            var mailTemplate = queryService.RetrieveMailTemplate(createdId);
            Assert.IsNotNull(mailTemplate);
            Assert.IsTrue(mailTemplate.MailTemplateTranslations.Count > 0);

            //List languages
            var languages = queryService.ListLanguages();
            Assert.IsTrue(languages.Count > 0);

            //Check if there is a MailTemplateTranslation for each language in the database
            Assert.AreEqual(languages.Count, mailTemplate.MailTemplateTranslations.Count);

            foreach (var language in languages)
            {
                Assert.IsNotNull(mailTemplate.MailTemplateTranslations.SingleOrDefault(mtt => mtt.LanguageId == language.Id));
            }
        }

        [TestMethod]
        public void TestUpdateMailTemplate()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            const string name = "MyMailTemplate",
                         code = "TESTTEMPLATE",
                         fromAddress = "thomas.king@quintessence.be",
                         storedProcedureName = "RetrieveInformation",
                         bccAddress = "eddy.claesen@quintessence.be";

            var createRequest = new CreateMailTemplateRequest
            {
                Name = name,
                Code = code,
                FromAddress = fromAddress,
                BccAddress = bccAddress,
                StoredProcedureName = storedProcedureName
            };
            var createdId = service.CreateMailTemplate(createRequest);

            //Retrieve created MailTemplate
            var mailTemplate = queryService.RetrieveMailTemplate(createdId);
            Assert.IsNotNull(mailTemplate);
            Assert.IsTrue(mailTemplate.MailTemplateTranslations.Count > 0);

            //Update MailTemplate and underlying translations
            var updateTranslationRequests =
                mailTemplate.MailTemplateTranslations.Select(Mapper.DynamicMap<UpdateMailTemplateTranslationRequest>).ToList();
            const string updateName = "MyUpdatedMailTemplate",
                         updatedFromAddress = "thomas.king@quintessence.be",
                         updatedBccAddress = "eddy.claesen@quintessence.be";

            var updateRequest = new UpdateMailTemplateRequest
            {
                Name = updateName,
                FromAddress = updatedFromAddress,
                BccAddress = updatedBccAddress,
                MailTemplateTranslations = updateTranslationRequests
            };
            service.UpdateMailTemplate(updateRequest);

            //Retrieve updated MailTemplate
            var updatedMailTemplate = queryService.RetrieveMailTemplate(createdId);
            Assert.IsNotNull(updatedMailTemplate);
            Assert.IsTrue(updatedMailTemplate.MailTemplateTranslations.Count > 0);
        }

        [TestMethod]
        public void TestCreateMailTemplateTranslation()
        {
            //Is tested in TestCreateMailTemplate (this creates a MailTemplate and corresponding MailTemplateTranslations
            //for each existing language in the database
        }

        [TestMethod]
        public void TestUpdateMailTemplateTranslation()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            const string name = "MyMailTemplate",
                         code = "TESTTEMPLATE",
                         fromAddress = "thomas.king@quintessence.be",
                         storedProcedureName = "RetrieveInformation",
                         bccAddress = "eddy.claesen@quintessence.be";

            var createRequest = new CreateMailTemplateRequest
                {
                    Name = name,
                    Code = code,
                    FromAddress = fromAddress,
                    BccAddress = bccAddress,
                    StoredProcedureName = storedProcedureName
                };
            var createdId = service.CreateMailTemplate(createRequest);

            //Retrieve created MailTemplate
            var mailTemplate = queryService.RetrieveMailTemplate(createdId);
            Assert.IsNotNull(mailTemplate);
            Assert.IsTrue(mailTemplate.MailTemplateTranslations.Count > 0);

            //List languages
            var languages = queryService.ListLanguages();
            Assert.IsTrue(languages.Count > 0);

            //Check if there is a MailTemplateTranslation for each language in the database
            Assert.AreEqual(languages.Count, mailTemplate.MailTemplateTranslations.Count);

            foreach (var language in languages)
            {
                Assert.IsNotNull(mailTemplate.MailTemplateTranslations.SingleOrDefault(mtt => mtt.LanguageId == language.Id));
            }

            var firstMailTemplateTranslation = mailTemplate.MailTemplateTranslations.FirstOrDefault();
            Assert.IsNotNull(firstMailTemplateTranslation);

            //Update MailTemplateTranslation
            const string updatedBody = "Updated body.",
                         updatedSubject = "Updated subject.";
            var updateRequest = Mapper.DynamicMap<UpdateMailTemplateTranslationRequest>(firstMailTemplateTranslation);
            updateRequest.Body = updatedBody;
            updateRequest.Subject = updatedSubject;
            service.UpdateMailTemplateTranslation(updateRequest);

            //Retrieve updated MailTemplateTranslation
            var updatedMailTemplateTranslation =
                queryService.RetrieveMailTemplateTranslation(firstMailTemplateTranslation.Id);
            Assert.IsNotNull(updatedMailTemplateTranslation);
            Assert.AreEqual(updatedBody, updatedMailTemplateTranslation.Body);
            Assert.AreEqual(updatedSubject, updatedMailTemplateTranslation.Subject);
        }
    }
}