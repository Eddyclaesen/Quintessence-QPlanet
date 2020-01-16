using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.Business.Tests.Base;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.DataModel.Crm;

namespace Quintessence.CulturalFit.Business.Tests
{
    [TestClass]
    public class CrmRepositoryTests: BaseBusiness
    {
        [TestMethod]
        public void TestListParticipantsByProjectId()
        {
            var repository = Container.Resolve<ICrmRepository>();
            var participants = repository.Filter<Participant>(p => p.ProjectId == 11465);
            Assert.IsTrue(participants.Count >= 1);
        }
    }
}
