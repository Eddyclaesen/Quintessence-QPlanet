using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    public interface IScmQueryContext : IQuintessenceQueryContext
    {
        DbQuery<ActivityTypeProfileView> ActivityTypeProfiles { get; }
        DbQuery<ActivityView> Activities { get; }
        DbQuery<ActivityTypeView> ActivityTypes { get; }
        DbQuery<ActivityProfileView> ActivityProfiles { get; }
        DbQuery<ProductView> Products { get; }
        DbQuery<ProductTypeView> ProductTypes { get; }
        DbQuery<ActivityDetailView> ActivityDetails { get; }
        DbQuery<TrainingTypeView> TrainingTypes { get; }
        DbQuery<ProfileView> Profiles { get; }
        DbQuery<ActivityDetailTrainingCandidateView> ActivityDetailTrainingCandidates { get; }

        IEnumerable<ActivityDetailTrainingAppointmentView> ListActivityDetailTrainingAppointments(Guid activityDetailTrainingId);
    }
}