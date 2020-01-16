using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface ISupplyChainManagementQueryRepository : IQueryRepository
    {
        List<ActivityTypeProfileView> ListActivityTypeProfiles();
        List<ActivityTypeProfileView> ListActivityTypeProfiles(Guid activityTypeId);
        List<ActivityView> ListActivities(Guid projectId);
        ActivityView RetrieveActivity(Guid id);
        List<ProductView> ListProducts(Guid projectId);
        ActivityDetailView RetrieveActivityDetail(Guid id);
        List<TrainingTypeView> ListTrainingTypes();
        List<ActivityTypeView> ListActivityTypeDetails();
        List<ProfileView> ListProfiles();
        List<ActivityDetailTrainingCandidateView> ListActivityDetailTrainingCandidates(List<int> appointmentIds);
        List<ActivityDetailTrainingAppointmentView> ListActivityDetailTrainingCandidates(Guid activityDetailTrainingId);
    }
}