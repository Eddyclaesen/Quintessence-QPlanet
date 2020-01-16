using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class InfrastructureQueryServiceImplementationTests : QueryServiceImplementationTestBase<IInfrastructureQueryService>
    {
        [TestMethod]
        public void TestListLanguages()
        {
            var queryService = Container.Resolve<IInfrastructureQueryService>();
            var languages = queryService.ListLanguages();
            Assert.IsTrue(languages.Count > 0);
        }

        [TestMethod]
        public void TestListOffices()
        {
            var queryService = Container.Resolve<IInfrastructureQueryService>();
            var offices = queryService.ListOffices();
            Assert.IsTrue(offices.Count > 0);
        }

        [TestMethod]
        public void TestListAssessmentRooms()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveOffice()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestListAssessorColors()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveAssessmentRoom()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestRetrieveMailTemplateByCode()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            //Create MailTemplate
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
            service.CreateMailTemplate(createRequest);

            //Retrieve MailTemplate by code
            var mailTemplate = queryService.RetrieveMailTemplateByCode(code);
            Assert.IsNotNull(mailTemplate);
        }

        [TestMethod]
        public void TestRetrieveMailTemplate()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            //Create MailTemplate
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

            //Retrieve MailTemplate by code
            var mailTemplate = queryService.RetrieveMailTemplate(createdId);
            Assert.IsNotNull(mailTemplate);
        }

        [TestMethod]
        public void TestRetrieveMailTemplateTranslation()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            //Create MailTemplate
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

            //Retrieve MailTemplate by code
            var mailTemplate = queryService.RetrieveMailTemplate(createdId);
            Assert.IsNotNull(mailTemplate);
            Assert.IsTrue(mailTemplate.MailTemplateTranslations.Count > 0);

            //Retrieve first MailTemplateTranslation from the MailTemplate
            var firstMailTemplateTranslation = mailTemplate.MailTemplateTranslations.FirstOrDefault();
            Assert.IsNotNull(firstMailTemplateTranslation);

            //Retrieve MailTemplateTranslation
            var mailTemplateTranslation = queryService.RetrieveMailTemplateTranslation(firstMailTemplateTranslation.Id);
            Assert.IsNotNull(mailTemplateTranslation);
        }

        [TestMethod]
        public void TestListMailTemplates()
        {
            var service = Container.Resolve<IInfrastructureCommandService>();
            var queryService = Container.Resolve<IInfrastructureQueryService>();

            //Create MailTemplate
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
            service.CreateMailTemplate(createRequest);

            //List MailTemplates
            var mailTemplates = queryService.ListMailTemplates();
            Assert.IsTrue(mailTemplates.Count > 0);

        }
    }
}
