using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class SupplyChainManagementQueryService : SecuredUnityServiceBase, ISupplyChainManagementQueryService
    {
        public List<ActivityTypeProfileView> ListActivityTypeProfiles(ListActivityTypeProfilesRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                if (request.ActivityTypeId.HasValue)
                    return repository.ListActivityTypeProfiles(request.ActivityTypeId.Value);

                if (request.ActivityId.HasValue)
                {
                    var activity = repository.RetrieveActivity(request.ActivityId.Value);
                    return repository.ListActivityTypeProfiles(activity.ActivityTypeId);
                }

                return repository.ListActivityTypeProfiles();
            });
        }

        public List<ActivityView> ListActivities(ListActivitiesRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var scmRepository = Container.Resolve<ISupplyChainManagementQueryRepository>();
                var prmRepository = Container.Resolve<IProjectManagementQueryRepository>();

                if (request.ProjectPlanId.HasValue)
                {
                    var project = prmRepository.RetrieveProjectByProjectPlan(request.ProjectPlanId.Value);
                    return scmRepository.ListActivities(project.Id);
                }

                if (request.ProjectId.HasValue)
                    return scmRepository.ListActivities(request.ProjectId.Value);

                throw new ArgumentOutOfRangeException("request", "Please specify a project id or project plan id.");
            });
        }

        public List<ActivityTypeView> ListActivityTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.List<ActivityTypeView>();
            });
        }

        public ActivityProfileView RetrieveActivityProfile(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.Retrieve<ActivityProfileView>(id);
            });
        }

        public List<ProductView> ListProducts(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.ListProducts(projectId);
            });
        }

        public List<ProductTypeView> ListProductTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.List<ProductTypeView>();
            });
        }

        public ActivityView RetrieveActivity(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.RetrieveActivity(id);
            });
        }

        public ActivityDetailView RetrieveActivityDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                var activityDetail = repository.RetrieveActivityDetail(id);
                
                activityDetail.ExecuteForType<ActivityDetailTrainingView>(adt => activityDetail = EnsureLanguages(adt));
                activityDetail.ExecuteForType<ActivityDetailTrainingView>(adt => activityDetail = EnsureTrainingAppointments(adt));
                activityDetail.ExecuteForType<ActivityDetailTrainingView>(adt => activityDetail = EnsureCandidates(adt));
                activityDetail.ExecuteForType<ActivityDetailWorkshopView>(adw => activityDetail = EnsureLanguages(adw));

                return activityDetail;
            });
        }

        public List<TrainingTypeView> ListTrainingTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.ListTrainingTypes();
            });
        }

        public List<ActivityTypeView> ListActivityTypeDetails()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.ListActivityTypeDetails();
            });
        }

        public ActivityTypeView RetrieveActivityType(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.Retrieve<ActivityTypeView>(id);
            });
        }

        public List<ProfileView> ListProfiles()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.ListProfiles();
            });
        }

        public ActivityTypeProfileView RetrieveActivityTypeProfile(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.Retrieve<ActivityTypeProfileView>(id);
            });
        }

        public ProfileView RetrieveProfile(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

                return repository.Retrieve<ProfileView>(id);
            });
        }

        public ProductTypeView RetrieveProductType(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                using (var repository = Container.Resolve<ISupplyChainManagementQueryRepository>())
                {
                    return repository.Retrieve<ProductTypeView>(id);
                }
            });
        }

        public ProductView RetrieveProduct(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                using (var repository = Container.Resolve<ISupplyChainManagementQueryRepository>())
                {
                    return repository.Retrieve<ProductView>(id);   
                }
            });
        }

        private ActivityDetailTrainingView EnsureLanguages(ActivityDetailTrainingView activityDetailTraining)
        {
            var infQueryService = Container.Resolve<IInfrastructureQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();
            var scmCommandService = Container.Resolve<ISupplyChainManagementCommandService>();
            var languages = infQueryService.ListLanguages();

            var missingLanguages = languages.Except(languages
                .Where(l => activityDetailTraining.ActivityDetailTrainingLanguages
                    .Select(adtl => adtl.LanguageId).Contains(l.Id))).ToList();

            if (missingLanguages.Count > 0)
            {
                foreach (var language in missingLanguages)
                {
                    var request = new CreateNewActivityDetailTrainingLanguageRequest
                        {
                            ActivityDetailTrainingId = activityDetailTraining.Id,
                            LanguageId = language.Id
                        };
                    scmCommandService.CreateNewActivityDetailTrainingLanguage(request);
                }

                return (ActivityDetailTrainingView)scmQueryService.RetrieveActivityDetail(activityDetailTraining.Id);
            }
            return activityDetailTraining;
        }

        private ActivityDetailView EnsureTrainingAppointments(ActivityDetailTrainingView activityDetailTraining)
        {
            var queryService = Container.Resolve<ISupplyChainManagementQueryRepository>();

            activityDetailTraining.TrainingAppointments = queryService.ListActivityDetailTrainingCandidates(activityDetailTraining.Id);

            return activityDetailTraining;
        }

        private ActivityDetailView EnsureCandidates(ActivityDetailTrainingView activityDetailTraining)
        {
            var repository = Container.Resolve<ISupplyChainManagementQueryRepository>();

            if (activityDetailTraining.TrainingAppointments != null && activityDetailTraining.TrainingAppointments.Count > 0)
                activityDetailTraining.ActivityDetailTrainingCandidates = repository.ListActivityDetailTrainingCandidates(activityDetailTraining.TrainingAppointments.Select(ta => ta.Id).ToList());

            return activityDetailTraining;
        }

        private ActivityDetailWorkshopView EnsureLanguages(ActivityDetailWorkshopView activityDetailWorkshop)
        {
            var infQueryService = Container.Resolve<IInfrastructureQueryService>();
            var scmQueryService = Container.Resolve<ISupplyChainManagementQueryService>();
            var scmCommandService = Container.Resolve<ISupplyChainManagementCommandService>();
            var languages = infQueryService.ListLanguages();

            var missingLanguages = languages.Except(languages
                .Where(l => activityDetailWorkshop.ActivityDetailWorkshopLanguages
                    .Select(adtl => adtl.LanguageId).Contains(l.Id))).ToList();

            if (missingLanguages.Count > 0)
            {
                foreach (var language in missingLanguages)
                {
                    var request = new CreateNewActivityDetailWorkshopLanguageRequest
                    {
                        ActivityDetailWorkshopId = activityDetailWorkshop.Id,
                        LanguageId = language.Id
                    };
                    scmCommandService.CreateNewActivityDetailWorkshopLanguage(request);
                }

                return (ActivityDetailWorkshopView)scmQueryService.RetrieveActivityDetail(activityDetailWorkshop.Id);
            }
            return activityDetailWorkshop;
        }
    }
}
