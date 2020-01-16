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
    public class SecurityManagementServiceImplementationTests : ServiceImplementationTestBase
    {
        [TestMethod]
        public void TestListCustomerAssistants()
        {
            var scmQueryService = Container.Resolve<ISecurityQueryService>();
            var customerAssistants = scmQueryService.ListCustomerAssistants();
            Assert.IsTrue(customerAssistants.Count > 0);

            Assert.IsTrue(customerAssistants.All(c => c.RoleCode == "CUSTA"));
        }
    }
}
