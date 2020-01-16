using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Extensions;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Scm;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class SupplyChainManagementCommandRepository : CommandRepositoryBase<IScmCommandContext>, ISupplyChainManagementCommandRepository
    {
        public SupplyChainManagementCommandRepository(IUnityContainer container)
            : base(container)
        {
        }

        public Activity PrepareActivity(Guid projectId, Guid activityTypeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activityType = context.ActivityTypes.SingleOrDefault(at => at.Id == activityTypeId);

                        if (activityType == null)
                            throw new ArgumentOutOfRangeException("activityTypeId");

                        Activity activity = null;
                        switch (activityType.Name)
                        {
                            case "Consulting":
                                activity = context.Create<ActivityDetailConsulting>();
                                break;

                            case "Support":
                                activity = context.Create<ActivityDetailSupport>();
                                break;

                            case "Workshop":
                                activity = context.Create<ActivityDetailWorkshop>();
                                break;

                            case "Coaching":
                                activity = context.Create<ActivityDetailCoaching>();
                                break;

                            case "Training":
                                activity = context.Create<ActivityDetailTraining>();
                                break;

                            default:
                                activity = context.Create<ActivityDetail>();
                                break;
                        }

                        activity.ProjectId = projectId;
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

        public ActivityProfile PrepareActivityProfile()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activity = context.Create<ActivityProfile>();
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

        public Product PrepareProduct()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var product = context.Create<Product>();
                        return product;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ActivityDetail RetrieveActivityDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var activityDetail = context.ActivityDetails.SingleOrDefault(ad => ad.Id == id);
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

        public List<ActivityDetailTraining2TrainingType> ListActivityDetailTraining2TrainingTypes(Guid activityDetailTrainingId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ActivityDetailTraining2TrainingTypes
                            .Where(link => link.ActivityDetailTrainingId == activityDetailTrainingId)
                            .ToList();
                        return links;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnlinkActivityDetailTraining2TrainingType(ActivityDetailTraining2TrainingType activityDetailTraining2TrainingType)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ActivityDetailTraining2TrainingTypes
                            .SingleOrDefault(e => e.ActivityDetailTrainingId == activityDetailTraining2TrainingType.ActivityDetailTrainingId
                                                    && e.TrainingTypeId == activityDetailTraining2TrainingType.TrainingTypeId);
                        context.ActivityDetailTraining2TrainingTypes.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkActivityDetailTraining2TrainingType(Guid activityDetailTrainingId, Guid trainingTypeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ActivityDetailTraining2TrainingTypes
                            .SingleOrDefault(e => e.ActivityDetailTrainingId == activityDetailTrainingId && e.TrainingTypeId == trainingTypeId);

                        if (link == null)
                        {
                            link = context.ActivityDetailTraining2TrainingTypes.Add(context.ActivityDetailTraining2TrainingTypes.Create());
                            link.TrainingTypeId = trainingTypeId;
                            link.ActivityDetailTrainingId = activityDetailTrainingId;
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ActivityType2Profile RetrieveActivityType2Profile(Guid activityTypeId, Guid profileId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ActivityType2Profiles.SingleOrDefault(e => e.ActivityTypeId == activityTypeId && e.ProfileId == profileId);
                        return link;
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