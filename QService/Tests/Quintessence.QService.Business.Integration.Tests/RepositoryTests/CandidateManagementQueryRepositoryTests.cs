using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Microsoft.Practices.Unity;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.Integration.Tests.RepositoryTests
{
    /// <summary>
    /// Tests the candidate management query repository
    /// </summary>
    [TestClass]
    public class CandidateManagementQueryRepositoryTests : QueryRepositoryTestBase<ICandidateManagementQueryRepository>
    {
        /// <summary>
        /// Tests the list candidates.
        /// </summary>
        [TestMethod]
        public void TestListCandidates()
        {
            var candidateManagementQueryRepository = Container.Resolve<ICandidateManagementQueryRepository>();

            var candidates = candidateManagementQueryRepository.ListCandidates();
            Assert.IsNotNull(candidates);

            //Prepare a candidate if none are found
            if (candidates.Count == 0)
            {
                var candidateManagementCommandService = Container.Resolve<ICandidateManagementCommandService>();
                var createCandidateRequest = new CreateCandidateRequest
                    {
                        FirstName = "Sheldon",
                        LastName = "Cooper",
                        Email = "sheldon.cooper@Caltech.edu",
                        Gender = GenderType.M.ToString(),
                        LanguageId = 1
                    };
                candidateManagementCommandService.CreateCandidate(createCandidateRequest);

                candidates = candidateManagementQueryRepository.ListCandidates();
                Assert.IsNotNull(candidates);
            }

            Assert.IsTrue(candidates.Count > 0);
        }

        /// <summary>
        /// Tests the full name of the list candidates by.
        /// </summary>
        [TestMethod]
        public void TestListCandidatesByFullName()
        {
            var candidateManagementQueryRepository = Container.Resolve<ICandidateManagementQueryRepository>();

            var candidates = candidateManagementQueryRepository.ListCandidates();
            Assert.IsNotNull(candidates);

            //Prepare a candidate if none are found
            if (candidates.Count == 0)
            {
                var candidateManagementCommandService = Container.Resolve<ICandidateManagementCommandService>();
                var createCandidateRequest = new CreateCandidateRequest
                {
                    FirstName = "Sheldon",
                    LastName = "Cooper",
                    Email = "sheldon.cooper@Caltech.edu",
                    Gender = GenderType.M.ToString(),
                    LanguageId = 1
                };
                candidateManagementCommandService.CreateCandidate(createCandidateRequest);

                candidates = candidateManagementQueryRepository.ListCandidates();
                Assert.IsNotNull(candidates);
            }

            Assert.IsTrue(candidates.Count > 0);

            var candidate = candidates.FirstOrDefault();
            Assert.IsNotNull(candidate);

            candidates = candidateManagementQueryRepository.ListCandidatesByFullName(candidate.FirstName, candidate.LastName);
            Assert.IsNotNull(candidates);

            Assert.IsTrue(candidates.All(c => c.FirstName.ToLowerInvariant() == candidate.FirstName.ToLowerInvariant() 
                && c.LastName.ToLowerInvariant() == candidate.LastName.ToLowerInvariant()));
        }

        /// <summary>
        /// Tests the retrieve candidate detail.
        /// </summary>
        [TestMethod]
        public void TestRetrieveCandidateDetail()
        {
            var candidateManagementQueryRepository = Container.Resolve<ICandidateManagementQueryRepository>();

            var candidates = candidateManagementQueryRepository.ListCandidates();
            Assert.IsNotNull(candidates);

            //Prepare a candidate if none are found
            if (candidates.Count == 0)
            {
                var candidateManagementCommandService = Container.Resolve<ICandidateManagementCommandService>();
                var createCandidateRequest = new CreateCandidateRequest
                {
                    FirstName = "Sheldon",
                    LastName = "Cooper",
                    Email = "sheldon.cooper@Caltech.edu",
                    Gender = GenderType.M.ToString(),
                    LanguageId = 1
                };
                candidateManagementCommandService.CreateCandidate(createCandidateRequest);

                candidates = candidateManagementQueryRepository.ListCandidates();
                Assert.IsNotNull(candidates);
            }

            Assert.IsTrue(candidates.Count > 0);

            //Retrieve the first candidate
            var firstCandidate = candidates.FirstOrDefault();
            Assert.IsNotNull(firstCandidate);


            //Retrieve candidate by id of first candidate
            var retrievedCandidate = candidateManagementQueryRepository.RetrieveCandidateDetail(firstCandidate.Id);
            Assert.AreEqual(firstCandidate.Id, retrievedCandidate.Id);
            Assert.AreEqual(firstCandidate.FirstName, retrievedCandidate.FirstName);
            Assert.AreEqual(firstCandidate.LastName, retrievedCandidate.LastName);
            Assert.AreEqual(firstCandidate.Gender, retrievedCandidate.Gender);
        }

        [TestMethod]
        public void ListProgramComponentsByAssessmentRoom()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void ListProgramComponentsByCandidate()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void ListProgramComponentsByOfficeId()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void CheckForCollisions()
        {
            //TODO: Write test
        }
    }
}
