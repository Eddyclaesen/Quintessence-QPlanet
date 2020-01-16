using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Microsoft.Practices.Unity;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class CandidateManagementServiceImplementationTests : CommandServiceImplementationTestBase<ICandidateManagementCommandService>
    {
        /// <summary>
        /// Tests the create candidate, update candidate and delete candidate.
        /// </summary>
        [TestMethod]
        public void TestCreateCandidateUpdateCandidateDeleteCandidate()
        {
            //Create the candidate
            var candidateCommandService = Container.Resolve<ICandidateManagementCommandService>();
            const string createFirstName = "John";
            const string createLastName = "Appleseed";
            const string createEmail = "john.appleseed@test.com";
            var createRequest = new CreateCandidateRequest
                {
                    FirstName = createFirstName,
                    LastName = createLastName,
                    Email = createEmail,
                    Gender = GenderType.M.ToString(),
                    LanguageId = 1
                };
            var createdId = candidateCommandService.CreateCandidate(createRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //List the candidates
            var candidateQueryService = Container.Resolve<ICandidateManagementQueryService>();
            var candidates = candidateQueryService.ListCandidates();
            Assert.IsTrue(candidates.Count >= 1);

            //Retrieve the created candidate
            var retrievedCandidate = candidateQueryService.RetrieveCandidate(createdId);
            Assert.IsNotNull(retrievedCandidate);

            //Update the candidate
            const string updateFirstName = "John U.";
            const string updateLastName = "Appleseed";
            const string updateEmail = "john.u.appleseed@test.com";

            var updateRequest = Mapper.DynamicMap<UpdateCandidateRequest>(retrievedCandidate);
            updateRequest.FirstName = updateFirstName;
            updateRequest.LastName = updateLastName;
            updateRequest.Email = updateEmail;
            candidateCommandService.UpdateCandidate(updateRequest);

            //Retrieve the updated candidate
            var updatedCandidate = candidateQueryService.RetrieveCandidate(createdId);
            Assert.IsNotNull(updatedCandidate);
            Assert.AreEqual(updateFirstName, updatedCandidate.FirstName);
            Assert.AreEqual(updateLastName, updatedCandidate.LastName);
            Assert.AreEqual(updateEmail, updatedCandidate.Email);

            //Delete the candidate
            candidateCommandService.DeleteCandidate(createdId);

            //Check if candidate is really deleted
            var checkCandidate = candidateQueryService.RetrieveCandidate(createdId);
            Assert.IsNull(checkCandidate);
        }

        /// <summary>
        /// Tests the change candidate language.
        /// </summary>
        [TestMethod]
        public void TestChangeCandidateLanguage()
        {
            var candidateCommandService = Container.Resolve<ICandidateManagementCommandService>();
            var candidateQueryService = Container.Resolve<ICandidateManagementQueryService>();

            //Create the candidate
            const string createFirstName = "John";
            const string createLastName = "Appleseed";
            const string createEmail = "john.appleseed@test.com";

            var createRequest = new CreateCandidateRequest
            {
                FirstName = createFirstName,
                LastName = createLastName,
                Email = createEmail,
                Gender = GenderType.M.ToString(),
                LanguageId = 1
            };
            var createdId = candidateCommandService.CreateCandidate(createRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            candidateCommandService.ChangeCandidateLanguage(2, createdId);

            var candidate = candidateQueryService.RetrieveCandidate(createdId);
            Assert.IsNotNull(candidate);
            Assert.AreEqual(2, candidate.LanguageId);
        }

        [TestMethod]
        public void TestCreateProgramComponent()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProgramComponentEnd()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProgramComponentStart()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestUpdateProgramComponent()
        {
            //TODO: Write test
        }

        [TestMethod]
        public void TestDeleteProgramComponent()
        {
            //TODO: Write test
        }
    }
}
