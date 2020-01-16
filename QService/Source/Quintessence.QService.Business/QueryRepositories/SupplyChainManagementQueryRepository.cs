using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class SupplyChainManagementQueryRepository : QueryRepositoryBase<IScmQueryContext>, ISupplyChainManagementQueryRepository
    {
        public SupplyChainManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<ActivityTypeProfileView> ListActivityTypeProfiles()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activityTypeProfiles = context.ActivityTypeProfiles.ToList();
                        return activityTypeProfiles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ActivityTypeProfileView> ListActivityTypeProfiles(Guid activityTypeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activityTypeProfiles = context.ActivityTypeProfiles
                            .Where(atp => atp.ActivityTypeId == activityTypeId)
                            .ToList();

                        return activityTypeProfiles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ActivityView> ListActivities(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activities = context.Activities
                            .Include(a => a.ActivityProfiles)
                            .Where(a => a.ProjectId == projectId)
                            .ToList();

                        return activities;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ActivityView RetrieveActivity(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activity = context.Activities
                            .Include(a => a.ActivityProfiles)
                            .FirstOrDefault(a => a.Id == id);

                        return activity;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProductView> ListProducts(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activities = context.Products
                            .Where(a => a.ProjectId == projectId)
                            .ToList();

                        return activities;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ActivityDetailView RetrieveActivityDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activityDetail = context.ActivityDetails
                                .Include(a => a.ActivityProfiles)
                                .SingleOrDefault(a => a.Id == id);

                        if (activityDetail is ActivityDetailCoachingView)
                            activityDetail = context.ActivityDetails
                                .OfType<ActivityDetailCoachingView>()
                                .Include(a => a.ActivityProfiles)
                                .SingleOrDefault(a => a.Id == id);

                        if (activityDetail is ActivityDetailConsultingView)
                            activityDetail = context.ActivityDetails
                                .OfType<ActivityDetailConsultingView>()
                                .Include(a => a.ActivityProfiles)
                                .SingleOrDefault(a => a.Id == id);

                        if (activityDetail is ActivityDetailSupportView)
                            activityDetail = context.ActivityDetails
                                .OfType<ActivityDetailSupportView>()
                                .Include(a => a.ActivityProfiles)
                                .SingleOrDefault(a => a.Id == id);

                        if (activityDetail is ActivityDetailTrainingView)
                        {
                            activityDetail = context.ActivityDetails
                                .OfType<ActivityDetailTrainingView>()
                                .Include(a => a.ActivityProfiles)
                                .Include(a => a.ActivityDetailTrainingLanguages)
                                .Include(a => a.ActivityDetailTrainingTypes)
                                .SingleOrDefault(a => a.Id == id);
                        }

                        if (activityDetail is ActivityDetailWorkshopView)
                        {
                            activityDetail = context.ActivityDetails
                                .OfType<ActivityDetailWorkshopView>()
                                .Include(a => a.ActivityProfiles)
                                .Include(a => a.ActivityDetailWorkshopLanguages)
                                .SingleOrDefault(a => a.Id == id);
                        }

                        return activityDetail;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<TrainingTypeView> ListTrainingTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var trainingTypes = context.TrainingTypes.ToList();

                        return trainingTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ActivityTypeView> ListActivityTypeDetails()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var trainingTypes = context.ActivityTypes
                            .Include(at => at.ActivityTypeProfiles)
                            .ToList();

                        return trainingTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProfileView> ListProfiles()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var profiles = context.Profiles
                            .ToList();

                        return profiles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ActivityDetailTrainingCandidateView> ListActivityDetailTrainingCandidates(List<int> appointmentIds)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var profiles = context.ActivityDetailTrainingCandidates
                            .Where(adtc => appointmentIds.Contains(adtc.CrmAppointmentId))
                            .ToList();

                        return profiles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ActivityDetailTrainingAppointmentView> ListActivityDetailTrainingCandidates(Guid activityDetailTrainingId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var profiles = context.ListActivityDetailTrainingAppointments(activityDetailTrainingId)
                            .ToList();

                        return profiles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
