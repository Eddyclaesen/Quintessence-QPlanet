using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Cam;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.Integration.Tests.RepositoryTests
{
    /// <summary>
    /// Tests the candidate management command repository
    /// </summary>
    [TestClass]
    public class CandidateManagementCommandRepositoryTests : CommandRepositoryTestBase<ICandidateManagementCommandRepository>
    {
        /// <summary>
        /// Tests the create candidate.
        /// </summary>
        [TestMethod]
        public void TestCreateCandidate()
        {
            var repository = Container.Resolve<ICandidateManagementCommandRepository>();

            var candidate = repository.Prepare<Candidate>();
            Assert.IsNotNull(candidate);

            const string firstName = "Leonard";
            const string lastName = "Hofstadter";
            const string email = "leonard.hofstadter@caltech.edu";
            var gender = GenderType.M.ToString();

            candidate.FirstName = firstName;
            candidate.LastName = lastName;
            candidate.Email = email;
            candidate.Gender = gender;
            candidate.LanguageId = 1;

            repository.Save(candidate);

            candidate = repository.Retrieve<Candidate>(candidate.Id);
            Assert.IsNotNull(candidate);

            Assert.AreEqual(firstName, candidate.FirstName);
            Assert.AreEqual(lastName, candidate.LastName);
            Assert.AreEqual(email, candidate.Email);
            Assert.AreEqual(gender, candidate.Gender);
        }

        /// <summary>
        /// Tests the update candidate.
        /// </summary>
        [TestMethod]
        public void TestUpdateCandidate()
        {
            var repository = Container.Resolve<ICandidateManagementCommandRepository>();

            var candidate = repository.Prepare<Candidate>();
            Assert.IsNotNull(candidate);

            const string firstName = "Howard";
            const string lastName = "Wolowitz";
            const string email = "howard.wolowitz@caltech.edu";
            var gender = GenderType.M.ToString();

            candidate.FirstName = firstName;
            candidate.LastName = lastName;
            candidate.Email = email;
            candidate.Gender = gender;

            repository.Save(candidate);

            candidate = repository.Retrieve<Candidate>(candidate.Id);
            Assert.IsNotNull(candidate);

            candidate.FirstName = firstName.ToUpperInvariant();
            candidate.LastName = lastName.ToUpperInvariant();
            candidate.Email = email.ToUpperInvariant();
            candidate.Gender = gender;

            var versionId = candidate.Audit.VersionId;

            repository.Save(candidate);
            candidate = repository.Retrieve<Candidate>(candidate.Id);

            Assert.AreNotEqual(versionId, candidate.Audit.VersionId);

            Assert.AreEqual(firstName.ToUpperInvariant(), candidate.FirstName);
            Assert.AreEqual(lastName.ToUpperInvariant(), candidate.LastName);
            Assert.AreEqual(email.ToUpperInvariant(), candidate.Email);
        }

        /// <summary>
        /// Tests the delete candidate.
        /// </summary>
        [TestMethod]
        public void TestDeleteCandidate()
        {
            var repository = Container.Resolve<ICandidateManagementCommandRepository>();

            var candidate = repository.Prepare<Candidate>();
            Assert.IsNotNull(candidate);

            const string firstName = "Rajesh";
            const string lastName = "Koothrappali";
            const string email = "Rajesh.Koothrappali@caltech.edu";
            var gender = GenderType.M.ToString();

            candidate.FirstName = firstName;
            candidate.LastName = lastName;
            candidate.Email = email;
            candidate.Gender = gender;

            repository.Save(candidate);

            candidate = repository.Retrieve<Candidate>(candidate.Id);
            Assert.IsNotNull(candidate);

            var versionId = candidate.Audit.VersionId;

            repository.Delete(candidate);

            candidate = repository.Retrieve<Candidate>(candidate.Id);
            Assert.IsNotNull(candidate);

            Assert.AreNotEqual(versionId, candidate.Audit.VersionId);
            Assert.IsTrue(candidate.Audit.IsDeleted);
        }

        [TestMethod]
        public void TestRetrieveLinkedProgramComponent()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestDeleteProjectCandidateProgramComponents()
        {
            //TODO: Write test
        }
    }
}