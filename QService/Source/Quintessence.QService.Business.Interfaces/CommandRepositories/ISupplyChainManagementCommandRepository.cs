using System;
using System.Collections.Generic;
using Quintessence.QService.DataModel.Scm;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface ISupplyChainManagementCommandRepository : ICommandRepository
    {
        Activity PrepareActivity(Guid projectId, Guid activityTypeId);
        ActivityProfile PrepareActivityProfile();
        Product PrepareProduct();
        ActivityDetail RetrieveActivityDetail(Guid id);
        List<ActivityDetailTraining2TrainingType> ListActivityDetailTraining2TrainingTypes(Guid activityDetailTrainingId);
        void UnlinkActivityDetailTraining2TrainingType(ActivityDetailTraining2TrainingType activityDetailTraining2TrainingType);
        void LinkActivityDetailTraining2TrainingType(Guid activityDetailTrainingId, Guid trainingTypeId);
        ActivityType2Profile RetrieveActivityType2Profile(Guid activityTypeId, Guid profileId);
    }
}