using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Business.Integration.Tests.Base;
using Quintessence.QService.Core.Testing;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.Business.Integration.Tests.ServiceImplementationTests
{
    [TestClass]
    public class SupplyChainManagementQueryServiceImplementationTests : QueryServiceImplementationTestBase<ISupplyChainManagementQueryService>
    {
        /// <summary>
        /// Tests the list activity type profiles.
        /// </summary>
        [TestMethod]
        public void TestListActivityTypeProfiles()
        {
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            var activityTypeProfiles = scmQueryService.ListActivityTypeProfiles(new ListActivityTypeProfilesRequest());

            Assert.IsNotNull(activityTypeProfiles);

            ExtendedAssert.IsLargerThan(0, activityTypeProfiles.Count);
        }

        /// <summary>
        /// Tests the list profiles.
        /// </summary>
        [TestMethod]
        public void TestListProfiles()
        {
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();
            var profiles = scmQueryService.ListProfiles();

            //Check if there are profiles
            Assert.IsTrue(profiles.Count >= 1);

            //Check if profiles aren't null
            Assert.IsTrue(profiles.All(p => p != null));
        }

        /// <summary>
        /// Tests the list activities.
        /// </summary>
        [TestMethod]
        public void TestListActivities()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewConsultancyProject(Container, "Test project", "democompany", "stijn van eynde", "stijn van eynde");

            var project = projectManagementQueryService.RetrieveProject(projectId);

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = (ConsultancyProjectView)project;

            Assert.IsNotNull(consultancyProject);

            SupplyChainManagementCommandServiceImplementationTests.CreateActivitiesForConsultancyProject(Container, consultancyProject.Id);

            var activities = service.ListActivities(new ListActivitiesRequest { ProjectId = project.Id });

            Assert.IsNotNull(activities);
            Assert.IsTrue(activities.Any());
            Assert.IsTrue(activities.All(a => a.ActivityProfiles.Count > 0));
        }

        /// <summary>
        /// Tests the list activity types.
        /// </summary>
        [TestMethod]
        public void TestListActivityTypes()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var activityTypes = service.ListActivityTypes();

            Assert.IsNotNull(activityTypes);
            Assert.IsTrue(activityTypes.Any());
            Assert.IsTrue(activityTypes.Any(at => at.IsSystem));
        }

        /// <summary>
        /// Tests the retrieve activity profile.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivityProfile()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewConsultancyProject(Container, "Test project", "democompany", "stijn van eynde", "stijn van eynde");

            var project = projectManagementQueryService.RetrieveProject(projectId);

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = (ConsultancyProjectView)project;

            Assert.IsNotNull(consultancyProject);

            SupplyChainManagementCommandServiceImplementationTests.CreateActivitiesForConsultancyProject(Container, consultancyProject.Id);

            var activities = service.ListActivities(new ListActivitiesRequest { ProjectId = project.Id });

            foreach (var ap in activities.SelectMany(a => a.ActivityProfiles))
            {
                var activityProfile = service.RetrieveActivityProfile(ap.Id);

                Assert.IsNotNull(activityProfile);
            }
        }

        /// <summary>
        /// Tests the list products.
        /// </summary>
        [TestMethod]
        public void TestListProducts()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewConsultancyProject(Container, "Test project", "democompany", "stijn van eynde", "stijn van eynde");

            var project = projectManagementQueryService.RetrieveProject(projectId);

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = (ConsultancyProjectView)project;

            Assert.IsNotNull(consultancyProject);

            SupplyChainManagementCommandServiceImplementationTests.CreateProductsForConsultancyProject(Container, consultancyProject.Id);

            var products = service.ListProducts(consultancyProject.Id);

            Assert.IsNotNull(products);
            Assert.IsTrue(products.Any());
        }

        /// <summary>
        /// Tests the list product types.
        /// </summary>
        [TestMethod]
        public void TestListProductTypes()
        {
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var productTypes = service.ListProductTypes();

            Assert.IsNotNull(productTypes);
            Assert.IsTrue(productTypes.Any());
        }

        /// <summary>
        /// Tests the retrieve product type.
        /// </summary>
        [TestMethod]
        public void TestRetrieveProductType()
        {
            var scmCommandservice = Container.Resolve<ISupplyChainManagementCommandService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create product type
            const string name = "TestProductType";
            const string description = "Description for TestProductType";
            const decimal unitPrice = 175.50m;
            var createProductTypeRequest = new CreateProductTypeRequest
            {
                Name = name,
                Description = description,
                UnitPrice = unitPrice
            };
            var createdId = scmCommandservice.CreateProductType(createProductTypeRequest);
            Assert.AreNotEqual(Guid.Empty, createdId);

            //Retrieve created product type
            var productType = scmQueryService.RetrieveProductType(createdId);
            Assert.IsNotNull(productType);
            Assert.AreEqual(createProductTypeRequest.Name, productType.Name);
            Assert.AreEqual(createProductTypeRequest.Description, productType.Description);
            Assert.AreEqual(createProductTypeRequest.UnitPrice, productType.UnitPrice);
        }

        /// <summary>
        /// Tests the retrieve activity.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivity()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewConsultancyProject(Container, "Test project", "democompany", "stijn van eynde", "stijn van eynde");

            var project = projectManagementQueryService.RetrieveProject(projectId);

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = (ConsultancyProjectView)project;

            Assert.IsNotNull(consultancyProject);

            SupplyChainManagementCommandServiceImplementationTests.CreateActivitiesForConsultancyProject(Container, consultancyProject.Id);

            var activities = service.ListActivities(new ListActivitiesRequest { ProjectId = project.Id });

            Assert.IsNotNull(activities);
            Assert.IsTrue(activities.Any());

            foreach (var a in activities)
            {
                var activity = service.RetrieveActivity(a.Id);
                Assert.IsNotNull(activity);
                Assert.IsTrue(
                    activity is ActivityDetailCoachingView 
                    || activity is ActivityDetailConsultingView 
                    || activity is ActivityDetailSupportView 
                    || activity is ActivityDetailTrainingView 
                    || activity is ActivityDetailWorkshopView);
            }
        }

        /// <summary>
        /// Tests the retrieve activity detail.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivityDetail()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();
            var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests.CreateNewConsultancyProject(Container, "Test project", "democompany", "stijn van eynde", "stijn van eynde");

            var project = projectManagementQueryService.RetrieveProject(projectId);

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(ConsultancyProjectView));

            var consultancyProject = (ConsultancyProjectView)project;

            Assert.IsNotNull(consultancyProject);

            SupplyChainManagementCommandServiceImplementationTests.CreateActivitiesForConsultancyProject(Container, consultancyProject.Id);

            var activities = service.ListActivities(new ListActivitiesRequest { ProjectId = project.Id });

            Assert.IsNotNull(activities);
            Assert.IsTrue(activities.Any());

            foreach (var a in activities)
            {
                var activityDetail = service.RetrieveActivityDetail(a.Id);
                Assert.IsNotNull(activityDetail);
                Assert.IsTrue(
                    activityDetail is ActivityDetailCoachingView
                    || activityDetail is ActivityDetailConsultingView
                    || activityDetail is ActivityDetailSupportView
                    || activityDetail is ActivityDetailTrainingView
                    || activityDetail is ActivityDetailWorkshopView);
            }
        }

        /// <summary>
        /// Tests the list training types.
        /// </summary>
        [TestMethod]
        public void TestListTrainingTypes()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var trainingTypes = service.ListTrainingTypes();

            Assert.IsNotNull(trainingTypes);
            Assert.IsTrue(trainingTypes.Any());
        }

        /// <summary>
        /// Tests the list activity type details.
        /// </summary>
        [TestMethod]
        public void TestListActivityTypeDetails()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var activityTypes = service.ListActivityTypeDetails();

            Assert.IsNotNull(activityTypes);
            Assert.IsTrue(activityTypes.Any());
            Assert.IsTrue(activityTypes.All(at => at.ActivityTypeProfiles.Any()));
        }

        /// <summary>
        /// Tests the type of the retrieve activity.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivityType()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var activityTypes = service.ListActivityTypes();

            Assert.IsNotNull(activityTypes);
            Assert.IsTrue(activityTypes.Any());

            foreach (var at in activityTypes)
            {
                var activityType = service.RetrieveActivityType(at.Id);
                Assert.IsNotNull(activityType);
            }
        }

        /// <summary>
        /// Tests the retrieve activity type profile.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivityTypeProfile()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var activityTypes = service.ListActivityTypeDetails();

            Assert.IsNotNull(activityTypes);
            Assert.IsTrue(activityTypes.Any());
            Assert.IsTrue(activityTypes.All(at => at.ActivityTypeProfiles.Any()));

            foreach (var atp in activityTypes.SelectMany(at => at.ActivityTypeProfiles))
            {
                var activityTypeProfile = service.RetrieveActivityTypeProfile(atp.Id);
                Assert.IsNotNull(activityTypeProfile);
            }
        }

        /// <summary>
        /// Tests the retrieve profile.
        /// </summary>
        [TestMethod]
        public void TestRetrieveProfile()
        {
            //Query services
            var service = Container.Resolve<ISupplyChainManagementQueryService>();

            var profiles = service.ListProfiles();

            Assert.IsNotNull(profiles);
            Assert.IsTrue(profiles.Any());

            foreach (var p in profiles)
            {
                var profile = service.RetrieveProfile(p.Id);
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod]
        public void TestCrudProfile()
        {
            var scmCommandService = Container.Resolve<ISupplyChainManagementCommandService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            //Create new profile
            const string createName = "ProfileName";
            var createNewProfileRequest = new CreateNewProfileRequest
                {
                    Name = createName
                };
            var profileId = scmCommandService.CreateNewProfile(createNewProfileRequest);

            //Check if profile is created
            var createdProfile = scmQueryService.RetrieveProfile(profileId);
            Assert.IsTrue(createdProfile != null);
            Assert.AreEqual(createName, createdProfile.Name);


            //Update newly created profile
            const string updateName = "UpdatedProfileName";
            var updateProfileRequest = new UpdateProfileRequest
                {
                    Id = profileId,
                    Name = updateName,
                    AuditVersionid = createdProfile.Audit.VersionId
                };
            scmCommandService.UpdateProfile(updateProfileRequest);

            //Retrieve updatedProfile
            var updatedProfile = scmQueryService.RetrieveProfile(profileId);
            Assert.IsNotNull(updatedProfile);
            Assert.AreEqual(updateName, updatedProfile.Name);

            //Delete profile
            scmCommandService.DeleteProfile(profileId);

            //Check if profile was deleted
            Assert.IsNull(scmQueryService.RetrieveProfile(profileId));

        }
    }
}
