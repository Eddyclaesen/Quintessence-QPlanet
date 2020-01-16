using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Host.Tests.Base;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.QPlanetService.Host.Tests
{
    public class SupplyChainManagementQueryServiceTests : ServiceTestBase
    {
        //[TestMethod]
        public void TestListActivityTypeProviles()
        {
            var response = Execute<ISupplyChainManagementQueryService, List<ActivityTypeProfileView>>(service => service.ListActivityTypeProfiles(new ListActivityTypeProfilesRequest()));

            Assert.IsNotNull(response);
        }
    }
}
