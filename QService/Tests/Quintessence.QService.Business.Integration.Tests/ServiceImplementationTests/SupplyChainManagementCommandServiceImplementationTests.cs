using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
    public class SupplyChainManagementCommandServiceImplementationTests : CommandServiceImplementationTestBase<ISupplyChainManagementCommandService>
    {
        /// <summary>
        /// Tests the create new activity.
        /// </summary>
        [TestMethod]
        public void TestCreateNewActivity()
        {
            //Query services
            var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests
                .CreateNewConsultancyProject(Container, "Test Consultancy Project", "DemoCompany", "Stijn Van Eynde", "Thomas King");

            var project = prmQueryService.RetrieveProjectDetail(projectId) as ConsultancyProjectView;
            Assert.IsNotNull(project);

            var activityTypes = scmQueryService.ListActivityTypes();

            CreateActivitiesForConsultancyProject(Container, project.Id);

            var activities = scmQueryService.ListActivities(new ListActivitiesRequest { ProjectId = projectId });
            Assert.IsNotNull(activities);
            ExtendedAssert.IsLargerThan(0, activities.Count);
            Assert.IsTrue(activities.All(a => a.ActivityProfiles.Count > 0));
            Assert.AreEqual(activityTypes.Count, activities.Count);
        }

        /// <summary>
        /// Tests the create new activity profile.
        /// </summary>
        [TestMethod]
        public void TestCreateNewActivityProfile()
        {
            var service = Container.Resolve<ISupplyChainManagementCommandService>();
            var queryService = Container.Resolve<ISupplyChainManagementQueryService>();

            var projectId = ProjectManagementServiceImplementationTests
                .CreateNewConsultancyProject(Container, "Test Consultancy Project", "DemoCompany", "Stijn Van Eynde", "Thomas King");

            var activityTypes = queryService.ListActivityTypeDetails();
            var activityProfileIds = new List<Guid>();

            foreach (var activityType in activityTypes)
            {
                var createNewActivityRequest = new CreateNewActivityRequest
                    {
                        ActivityTypeId = activityType.Id,
                        Name = activityType.Name,
                        ProjectId = projectId
                    };
                var activityId = service.CreateNewActivity(createNewActivityRequest);

                foreach (var profileId in activityType.ActivityTypeProfiles.Select(atp => atp.ProfileId))
                {
                    var createNewActivityProfileRequest = new CreateNewActivityProfileRequest
                        {
                            ActivityId = activityId,
                            DayRate = 1000,
                            HalfDayRate = 500,
                            HourlyRate = 125,
                            IsolatedHourlyRate = 150,
                            ProfileId = profileId
                        };
                    activityProfileIds.Add(service.CreateNewActivityProfile(createNewActivityProfileRequest));
                }
            }

            Assert.AreEqual(activityTypes.SelectMany(at => at.ActivityTypeProfiles).Count(), activityProfileIds.Count);
        }

        /// <summary>
        /// Tests the update activity.
        /// </summary>
        [TestMethod]
        public void TestUpdateActivity()
        {
            var service = Container.Resolve<ISupplyChainManagementCommandService>();
            var queryService = Container.Resolve<ISupplyChainManagementQueryService>();

            var projectId = CreateNewConsultancyProject<SupplyChainManagementCommandServiceImplementationTests>(Container);
            CreateActivitiesForConsultancyProject(Container, projectId);

            var activities = queryService.ListActivities(new ListActivitiesRequest { ProjectId = projectId });

            if (Mapper.FindTypeMapFor<ActivityView, UpdateActivityRequest>() == null)
                Mapper.CreateMap<ActivityView, UpdateActivityRequest>();

            if (Mapper.FindTypeMapFor<ActivityProfileView, UpdateActivityProfileRequest>() == null)
                Mapper.CreateMap<ActivityProfileView, UpdateActivityProfileRequest>();

            foreach (var a in activities)
            {
                var updateActivityRequest = Mapper.DynamicMap<UpdateActivityRequest>(a);
                updateActivityRequest.Name = updateActivityRequest.Name + " Updated";

                foreach (var updateActivityProfileRequest in updateActivityRequest.ActivityProfiles)
                {
                    updateActivityProfileRequest.DayRate = 2000;
                    updateActivityProfileRequest.HalfDayRate = 1000;
                    updateActivityProfileRequest.HourlyRate = 250;
                    updateActivityProfileRequest.IsolatedHourlyRate = 350;
                }

                Assert.IsNotNull(updateActivityRequest);

                service.UpdateActivity(updateActivityRequest);

                var activity = queryService.RetrieveActivity(a.Id);

                Assert.IsNotNull(activity);
                Assert.IsTrue(activity.Name.EndsWith("Updated"));
                Assert.IsTrue(activity.ActivityProfiles.All(ap => ap.DayRate == 2000));
                Assert.IsTrue(activity.ActivityProfiles.All(ap => ap.HalfDayRate == 1000));
                Assert.IsTrue(activity.ActivityProfiles.All(ap => ap.HourlyRate == 250));
                Assert.IsTrue(activity.ActivityProfiles.All(ap => ap.IsolatedHourlyRate == 350));
            }
        }

        [TestMethod]
        public void TestCreateProductType()
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

        [TestMethod]
        public void TestUpdateProductType()
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

            //Update product type
            const string updateName = "TestUpdateProductType";
            const string updateDescription = "Description for TestProductType";
            const decimal updateUnitPrice = 85.50m;
            var updateProductTypeRequest = Mapper.DynamicMap<UpdateProductTypeRequest>(productType);
            updateProductTypeRequest.Name = updateName;
            updateProductTypeRequest.Description = updateDescription;
            updateProductTypeRequest.UnitPrice = updateUnitPrice;
            scmCommandservice.UpdateProductType(updateProductTypeRequest);

            //Retrieve updated product type
            var updatedProductType = scmQueryService.RetrieveProductType(createdId);
            Assert.IsNotNull(updatedProductType);
            Assert.AreEqual(updateProductTypeRequest.Name, updatedProductType.Name);
            Assert.AreEqual(updateProductTypeRequest.Description, updatedProductType.Description);
            Assert.AreEqual(updateProductTypeRequest.UnitPrice, updatedProductType.UnitPrice);


        }

        [TestMethod]
        public void TestDeleteProductType()
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

            //Delete created product type
            scmCommandservice.DeleteProductType(createdId);

            //Check if product type was deleted
            Assert.IsNull(scmQueryService.RetrieveProductType(createdId));
        }

        [TestMethod]
        public void TestDeleteActivity()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivities()
        {
            //TODO:
        }

        [TestMethod]
        public void TestDeleteActivityProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestCreateNewProduct()
        {
            //TODO:
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateProducts()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityDetailTraining()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityDetailCoaching()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityDetailSupport()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityDetailConsulting()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityDetailWorkshop()
        {
            //TODO:
        }

        [TestMethod]
        public void TestCreateNewActivityDetailTrainingLanguage()
        {
            //TODO:
        }

        [TestMethod]
        public void TestCreateNewActivityDetailWorkshopLanguage()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityTypeProfileRatesByIndex()
        {
            //TODO:
        }

        [TestMethod]
        public void TestCreateNewActivityType()
        {
            //TODO:
        }

        [TestMethod]
        public void TestDeleteActivityType()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityType()
        {
            //TODO:
        }

        [TestMethod]
        public void TestCreateNewActivityTypeProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateActivityTypeProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestDeleteActivityTypeProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestCreateNewProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestUpdateProfile()
        {
            //TODO:
        }

        [TestMethod]
        public void TestDeleteProfile()
        {
            //TODO:
        }


        #region Helper methods
        /// <summary>
        /// Creates the new consultancy project.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static Guid CreateNewConsultancyProject<TType>(IUnityContainer container)
            where TType : ServiceImplementationTestBase
        {
            return ProjectManagementServiceImplementationTests
                .CreateNewConsultancyProject(container, "Test for " + typeof(Type).Name, "DemoCompany", "Stijn Van Eynde", "Thomas King");
        }

        /// <summary>
        /// Defines the project activities.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="projectId">The project id.</param>
        public static void CreateActivitiesForConsultancyProject(IUnityContainer container, Guid projectId)
        {
            var scmQueryService = container.Resolve<ISupplyChainManagementQueryService>();
            var scmCommandService = container.Resolve<ISupplyChainManagementCommandService>();

            var activityTypes = scmQueryService.ListActivityTypes();

            foreach (var activityType in activityTypes)
            {
                var createNewActivityRequest = new CreateNewActivityRequest
                {
                    ActivityTypeId = activityType.Id,
                    ProjectId = projectId,
                    Name = activityType.Name
                };
                var activityId = scmCommandService.CreateNewActivity(createNewActivityRequest);

                var profiles = scmQueryService.ListActivityTypeProfiles(new ListActivityTypeProfilesRequest { ActivityTypeId = activityType.Id });

                foreach (var createNewActivityProfileRequest in profiles.Select(Mapper.DynamicMap<CreateNewActivityProfileRequest>))
                {
                    createNewActivityProfileRequest.ActivityId = activityId;
                    scmCommandService.CreateNewActivityProfile(createNewActivityProfileRequest);
                }
            }
        }

        /// <summary>
        /// Creates the products for consultancy project.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="projectId">The project id.</param>
        public static void CreateProductsForConsultancyProject(IUnityContainer container, Guid projectId)
        {
            var scmQueryService = container.Resolve<ISupplyChainManagementQueryService>();
            var scmCommandService = container.Resolve<ISupplyChainManagementCommandService>();

            var productTypes = scmQueryService.ListProductTypes();

            foreach (var productType in productTypes)
            {
                var createNewProductRequest = new CreateNewProductRequest
                    {
                        Name = productType.Name,
                        ProductTypeId = productType.Id,
                        ProjectId = projectId,
                        UnitPrice = productType.UnitPrice
                    };
                scmCommandService.CreateNewProduct(createNewProductRequest);
            }
        }
        #endregion
    }
}
