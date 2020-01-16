using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Scm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Prm;
using Profile = Quintessence.QService.DataModel.Scm.Profile;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class SupplyChainManagementCommandService : SecuredUnityServiceBase, ISupplyChainManagementCommandService
    {
        public Guid CreateNewActivity(CreateNewActivityRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                LogTrace("Prepare activity {0} for project {1} (activity type: {2}).", request.Name, request.ProjectId, request.ActivityTypeId);

                var activity = repository.PrepareActivity(request.ProjectId, request.ActivityTypeId);

                Mapper.DynamicMap(request, activity);

                var activityType = repository.Retrieve<ActivityType>(request.ActivityTypeId);

                if (activityType.Name == "Training")
                {
                    var documentManagementCommandService = Container.Resolve<IDocumentManagementCommandService>();
                    var id = documentManagementCommandService.CreateNewTrainingChecklist();

                    var documentManagementQueryService = Container.Resolve<IDocumentManagementQueryService>();
                    var trainingChecklist = documentManagementQueryService.RetrieveTrainingChecklist(id);

                    ((ActivityDetailTraining)activity).ChecklistLink = trainingChecklist.EditLink;
                    ((ActivityDetailTraining)activity).Code = DateTime.Now.ToString("yyyyMMddhhmm");
                }

                repository.Save(activity);

                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();
                switch (activityType.Name)
                {
                    case "Training":
                        //Prepare language based sessions
                        foreach (var language in infrastructureQueryService.ListLanguages())
                        {
                            var activityDetailTrainingLanguage = repository.Prepare<ActivityDetailTrainingLanguage>();
                            activityDetailTrainingLanguage.ActivityDetailTrainingId = activity.Id;
                            activityDetailTrainingLanguage.LanguageId = language.Id;
                            activityDetailTrainingLanguage.SessionQuantity = 0;
                            repository.Save(activityDetailTrainingLanguage);
                        }

                        break;
                    case "Workshop":
                        //Prepare language based sessions
                        foreach (var language in infrastructureQueryService.ListLanguages())
                        {
                            var activityDetailWorkshopLanguage = repository.Prepare<ActivityDetailWorkshopLanguage>();
                            activityDetailWorkshopLanguage.ActivityDetailWorkshopId = activity.Id;
                            activityDetailWorkshopLanguage.LanguageId = language.Id;
                            activityDetailWorkshopLanguage.SessionQuantity = 0;
                            repository.Save(activityDetailWorkshopLanguage);
                        }

                        break;
                }

                return activity.Id;
            });
        }

        public Guid CreateNewActivityProfile(CreateNewActivityProfileRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();
                var queryService = Container.Resolve<ISupplyChainManagementQueryService>();

                var activity = queryService.RetrieveActivity(request.ActivityId);

                var profile = activity.ActivityProfiles.FirstOrDefault(ap => ap.ProfileId == request.ProfileId);
                if (profile != null)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("{0} '{1}' already contains profile '{2}'. Please create another activity or remove the profile.", activity.ActivityTypeName, activity.Name, profile.ProfileName));
                    return Guid.Empty;
                }

                var activityProfile = repository.PrepareActivityProfile();

                Mapper.DynamicMap(request, activityProfile);

                repository.Save(activityProfile);

                return activityProfile.Id;
            });
        }

        public void UpdateActivity(UpdateActivityRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                foreach (var updateActivityProfileRequest in request.ActivityProfiles)
                {
                    var activityProfile = repository.Retrieve<ActivityProfile>(updateActivityProfileRequest.Id);

                    Mapper.DynamicMap(updateActivityProfileRequest, activityProfile);

                    repository.Save(activityProfile);
                }

                var activity = repository.Retrieve<Activity>(request.Id);

                Mapper.DynamicMap(request, activity);

                repository.Save(activity);
            });
        }

        public void DeleteActivity(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var supplyChainManagementQueryService = Container.Resolve<ISupplyChainManagementQueryService>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var activity = supplyChainManagementQueryService.RetrieveActivity(id);

                var projectPlan = queryService.RetrieveProjectPlanDetail(new RetrieveProjectPlanDetailRequest { ProjectId = activity.ProjectId });

                if (projectPlan.ProjectPlanPhases.SelectMany(ppp => ppp.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseActivityView>()).Any(pppa => pppa.ActivityId == activity.Id))
                    ValidationContainer.RegisterEntityValidationFaultEntry("This activity is already used in the projectplan.");

                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                foreach (var activityProfile in activity.ActivityProfiles)
                    repository.Delete<ActivityProfile>(activityProfile.Id);

                repository.Delete<Activity>(id);
            });
        }

        public void UpdateActivityProfile(List<UpdateActivityProfileRequest> updateActivityProfileRequests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var scmCommandRepository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                foreach (var updateActivityProfileRequest in updateActivityProfileRequests)
                {
                    var activityProfile = scmCommandRepository.Retrieve<ActivityProfile>(updateActivityProfileRequest.Id);
                    Mapper.DynamicMap(updateActivityProfileRequest, activityProfile);
                    scmCommandRepository.Save(activityProfile);
                }
            });
        }

        public void UpdateActivities(List<UpdateActivityRequest> updateActivityRequests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var scmCommandRepository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                foreach (var updateActivityRequest in updateActivityRequests)
                {
                    if (updateActivityRequest.ActivityProfiles != null)
                    {
                        foreach (var updateActivityProfileRequest in updateActivityRequest.ActivityProfiles)
                        {
                            var activityProfile = scmCommandRepository.Retrieve<ActivityProfile>(updateActivityProfileRequest.Id);
                            Mapper.DynamicMap(updateActivityProfileRequest, activityProfile);
                            scmCommandRepository.Save(activityProfile);
                        }
                    }

                    var activity = scmCommandRepository.Retrieve<Activity>(updateActivityRequest.Id);
                    Mapper.DynamicMap(updateActivityRequest, activity);
                    scmCommandRepository.Save(activity);
                }
            });
        }

        public void DeleteActivityProfile(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var scmCommandRepository = Container.Resolve<ISupplyChainManagementCommandRepository>())
                {
                    var queryService = Container.Resolve<ISupplyChainManagementQueryService>();
                    var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                    var activityProfile = queryService.RetrieveActivityProfile(id);
                    var activity = queryService.RetrieveActivity(activityProfile.ActivityId);

                    var projectPlan = projectManagementQueryService.RetrieveProjectPlanDetail(new RetrieveProjectPlanDetailRequest { ProjectId = activity.ProjectId });

                    if (projectPlan.ProjectPlanPhases.SelectMany(ppp => ppp.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseActivityView>()).Any(pppa => pppa.ActivityId == activity.Id && pppa.ProfileId == activityProfile.ProfileId))
                        ValidationContainer.RegisterEntityValidationFaultEntry("This profile is already used in the projectplan.");

                    scmCommandRepository.Delete<ActivityProfile>(id);
                }
            });
        }

        public Guid CreateNewProduct(CreateNewProductRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                LogTrace("Prepare product {0} for project {1} (product type: {2}).", request.Name, request.ProjectId, request.ProductTypeId);

                var product = repository.PrepareProduct();

                Mapper.DynamicMap(request, product);

                repository.Save(product);

                return product.Id;
            });
        }

        public void DeleteProduct(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<ISupplyChainManagementCommandRepository>())
                {
                    var queryService = Container.Resolve<ISupplyChainManagementQueryService>();
                    var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                    var product = queryService.RetrieveProduct(id);

                    var projectPlan = projectManagementQueryService.RetrieveProjectPlanDetail(new RetrieveProjectPlanDetailRequest { ProjectId = product.ProjectId });

                    if (projectPlan.ProjectPlanPhases.SelectMany(ppp => ppp.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseProductView>()).Any(pppp => pppp.ProductId == product.Id))
                        ValidationContainer.RegisterEntityValidationFaultEntry("This product is already used in the projectplan.");

                    repository.Delete<Product>(id);
                }
            });
        }

        public void UpdateProducts(List<UpdateProductRequest> updateProductRequests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                using (var repository = Container.Resolve<ISupplyChainManagementCommandRepository>())
                {
                    foreach (var updateProductRequest in updateProductRequests)
                    {
                        var product = repository.Retrieve<Product>(updateProductRequest.Id);
                        Mapper.DynamicMap(updateProductRequest, product);
                        repository.Save(product);
                    }
                }
            });
        }

        public void UpdateActivityDetailTraining(UpdateActivityDetailTrainingRequest updateActivityDetailTrainingRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailTraining = (ActivityDetailTraining)repository.RetrieveActivityDetail(updateActivityDetailTrainingRequest.Id);

                Mapper.DynamicMap(updateActivityDetailTrainingRequest, activityDetailTraining);

                repository.Save(activityDetailTraining);

                foreach (var updateActivityDetailTrainingLanguageRequest in updateActivityDetailTrainingRequest.ActivityDetailTrainingLanguages)
                {
                    var activityDetailTrainingLanguage = repository.Retrieve<ActivityDetailTrainingLanguage>(updateActivityDetailTrainingLanguageRequest.Id);

                    Mapper.DynamicMap(updateActivityDetailTrainingLanguageRequest, activityDetailTrainingLanguage);

                    repository.Save(activityDetailTrainingLanguage);
                }

                var activityDetailTraining2TrainingTypes = repository.ListActivityDetailTraining2TrainingTypes(activityDetailTraining.Id);

                foreach (var activityDetailTraining2TrainingType in activityDetailTraining2TrainingTypes)
                {
                    if (!updateActivityDetailTrainingRequest.SelectedTrainingTypeIds.Contains(activityDetailTraining2TrainingType.TrainingTypeId))
                        repository.UnlinkActivityDetailTraining2TrainingType(activityDetailTraining2TrainingType);
                    else
                        updateActivityDetailTrainingRequest.SelectedTrainingTypeIds.Remove(activityDetailTraining2TrainingType.TrainingTypeId);
                }

                foreach (var trainingTypeId in updateActivityDetailTrainingRequest.SelectedTrainingTypeIds)
                {
                    repository.LinkActivityDetailTraining2TrainingType(updateActivityDetailTrainingRequest.Id, trainingTypeId);
                }
            });
        }

        public void UpdateActivityDetailCoaching(UpdateActivityDetailCoachingRequest updateActivityDetailCoachingRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailCoaching = (ActivityDetailCoaching)repository.RetrieveActivityDetail(updateActivityDetailCoachingRequest.Id);

                Mapper.DynamicMap(updateActivityDetailCoachingRequest, activityDetailCoaching);

                repository.Save(activityDetailCoaching);
            });
        }

        public void UpdateActivityDetailSupport(UpdateActivityDetailSupportRequest updateActivityDetailSupportRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailSupport = (ActivityDetailSupport)repository.RetrieveActivityDetail(updateActivityDetailSupportRequest.Id);

                Mapper.DynamicMap(updateActivityDetailSupportRequest, activityDetailSupport);

                repository.Save(activityDetailSupport);
            });
        }

        public void UpdateActivityDetailConsulting(UpdateActivityDetailConsultingRequest updateActivityDetailConsultingRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailConsulting = (ActivityDetailConsulting)repository.RetrieveActivityDetail(updateActivityDetailConsultingRequest.Id);

                Mapper.DynamicMap(updateActivityDetailConsultingRequest, activityDetailConsulting);

                repository.Save(activityDetailConsulting);
            });
        }

        public void UpdateActivityDetailWorkshop(UpdateActivityDetailWorkshopRequest updateActivityDetailWorkshopRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailWorkshop = (ActivityDetailWorkshop)repository.RetrieveActivityDetail(updateActivityDetailWorkshopRequest.Id);

                Mapper.DynamicMap(updateActivityDetailWorkshopRequest, activityDetailWorkshop);

                repository.Save(activityDetailWorkshop);

                foreach (var activityDetailWorkshopLanguageRequest in updateActivityDetailWorkshopRequest.ActivityDetailWorkshopLanguages)
                {
                    var activityDetailWorkshopLanguage = repository.Retrieve<ActivityDetailWorkshopLanguage>(activityDetailWorkshopLanguageRequest.Id);

                    Mapper.DynamicMap(activityDetailWorkshopLanguageRequest, activityDetailWorkshopLanguage);

                    repository.Save(activityDetailWorkshopLanguage);
                }
            });
        }

        public void UpdateActivityDetail(UpdateActivityDetailRequest updateActivityDetailRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetail = repository.RetrieveActivityDetail(updateActivityDetailRequest.Id);

                Mapper.DynamicMap(updateActivityDetailRequest, activityDetail);

                repository.Save(activityDetail);
            });
        }

        public void UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            using (var repository = Container.Resolve<ISupplyChainManagementCommandRepository>())
            {
                var product = repository.Retrieve<Product>(updateProductRequest.Id);
                Mapper.DynamicMap(updateProductRequest, product);
                repository.Save(product);
            }
        }

        public void CreateNewActivityDetailTrainingLanguage(CreateNewActivityDetailTrainingLanguageRequest createNewActivityDetailTrainingLanguageRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailTrainingLanguage = repository.Prepare<ActivityDetailTrainingLanguage>();
                Mapper.DynamicMap(createNewActivityDetailTrainingLanguageRequest, activityDetailTrainingLanguage);

                repository.Save(activityDetailTrainingLanguage);
            });
        }

        public void CreateNewActivityDetailWorkshopLanguage(CreateNewActivityDetailWorkshopLanguageRequest createNewActivityDetailWorkshopLanguageRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityDetailWorkshopLanguage = repository.Prepare<ActivityDetailWorkshopLanguage>();
                Mapper.DynamicMap(createNewActivityDetailWorkshopLanguageRequest, activityDetailWorkshopLanguage);

                repository.Save(activityDetailWorkshopLanguage);
            });
        }

        public void UpdateActivityTypeProfileRatesByIndex(int index)
        {
            LogTrace();

            ExecuteTransaction(() =>
                {
                    var queryService = Container.Resolve<ISupplyChainManagementQueryService>();
                    var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                    var activityTypes = queryService.ListActivityTypeDetails();

                    foreach (var activityTypeProfile in activityTypes.SelectMany(at => at.ActivityTypeProfiles))
                    {
                        var activityType2Profile = repository.RetrieveActivityType2Profile(activityTypeProfile.ActivityTypeId, activityTypeProfile.ProfileId);
                        activityType2Profile.DayRate *= ((decimal)index / 100);
                        activityType2Profile.HalfDayRate *= ((decimal)index / 100);
                        activityType2Profile.HourlyRate *= ((decimal)index / 100);
                        activityType2Profile.IsolatedHourlyRate *= ((decimal)index / 100);
                        repository.Save(activityType2Profile);
                    }
                });
        }

        public Guid CreateNewActivityType(CreateNewActivityTypeRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityType = repository.Prepare<ActivityType>();

                Mapper.DynamicMap(request, activityType);

                repository.Save(activityType);

                return activityType.Id;
            });
        }

        public void DeleteActivityType(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityType = repository.Retrieve<ActivityType>(id);

                if (activityType.IsSystem)
                    throw new InvalidOperationException(string.Format("Unable to delete activity type '{0}' because it is defined by the system.", activityType.Name));

                repository.Delete<ActivityType>(id);
            });
        }

        public void UpdateActivityType(UpdateActivtyTypeRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityType = repository.Retrieve<ActivityType>(request.Id);

                Mapper.DynamicMap(request, activityType);

                repository.Save(activityType);
            });
        }

        public void CreateNewActivityTypeProfile(CreateNewActivityTypeProfileRequest request)
        {
            LogTrace("Create new activity type profile");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityType2Profile = repository.Prepare<ActivityType2Profile>();

                Mapper.DynamicMap(request, activityType2Profile);

                repository.Save(activityType2Profile);
            });
        }

        public void UpdateActivityTypeProfile(UpdateActivtyTypeProfileRequest request)
        {
            LogTrace("Update activity type profile.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var activityType2Profile = repository.Retrieve<ActivityType2Profile>(request.Id);

                Mapper.DynamicMap(request, activityType2Profile);

                repository.Save(activityType2Profile);
            });
        }

        public void DeleteActivityTypeProfile(Guid id)
        {
            LogTrace("Delete activity type profile.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                repository.Delete<ActivityType2Profile>(id);
            });
        }

        public Guid CreateNewProfile(CreateNewProfileRequest request)
        {
            LogTrace("Create new profile.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var profile = repository.Prepare<Profile>();

                Mapper.DynamicMap(request, profile);

                repository.Save(profile);

                return profile.Id;
            });
        }

        public void UpdateProfile(UpdateProfileRequest request)
        {
            LogTrace("Update profile.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var profile = repository.Retrieve<Profile>(request.Id);

                Mapper.DynamicMap(request, profile);

                repository.Save(profile);
            });
        }

        public void DeleteProfile(Guid id)
        {
            LogTrace("Delete profile.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                repository.Delete<Profile>(id);
            });
        }

        public Guid CreateProductType(CreateProductTypeRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var productType = repository.Prepare<ProductType>();

                Mapper.DynamicMap(request, productType);

                repository.Save(productType);

                return productType.Id;
            });
        }

        public void DeleteProductType(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                repository.Delete<ProductType>(id);
            });
        }

        public void UpdateProductType(UpdateProductTypeRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();

                var productType = repository.Retrieve<ProductType>(request.Id);

                Mapper.DynamicMap(request, productType);

                repository.Save(productType);
            });
        }

        public void CopyActivity(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementCommandRepository>();
                var queryService = Container.Resolve<ISupplyChainManagementQueryService>();

                var activityToCopy = queryService.RetrieveActivity(id);

                var request = new CreateNewActivityRequest
                    {
                        ActivityTypeId = activityToCopy.ActivityTypeId,
                        Name = "Copy of " + activityToCopy.Name,
                        ProjectId = activityToCopy.ProjectId
                    };
                var activityId = CreateNewActivity(request);

                var activity = repository.Retrieve<Activity>(activityId);

                foreach (var activityProfileToCopy in activityToCopy.ActivityProfiles)
                {
                    var activityProfile = repository.Prepare<ActivityProfile>();
                    activityProfile.ActivityId = activity.Id;
                    activityProfile.DayRate = activityProfileToCopy.DayRate;
                    activityProfile.HalfDayRate = activityProfileToCopy.HalfDayRate;
                    activityProfile.HourlyRate = activityProfileToCopy.HourlyRate;
                    activityProfile.IsolatedHourlyRate = activityProfileToCopy.IsolatedHourlyRate;
                    activityProfile.ProfileId = activityProfileToCopy.ProfileId;
                    repository.Save(activityProfile);
                }
            });
        }
    }
}