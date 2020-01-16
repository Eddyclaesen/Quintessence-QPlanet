using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IScmQueryContext
    {
        public DbQuery<ActivityTypeProfileView> ActivityTypeProfiles { get { return Set<ActivityTypeProfileView>().AsNoTracking(); } }
        public DbQuery<ActivityView> Activities { get { return Set<ActivityView>().AsNoTracking(); } }
        public DbQuery<ActivityTypeView> ActivityTypes { get { return Set<ActivityTypeView>().AsNoTracking(); } }
        public DbQuery<ActivityProfileView> ActivityProfiles { get { return Set<ActivityProfileView>().AsNoTracking(); } }
        public DbQuery<ProductView> Products { get { return Set<ProductView>().AsNoTracking(); } }
        public DbQuery<ProductTypeView> ProductTypes { get { return Set<ProductTypeView>().AsNoTracking(); } }
        public DbQuery<ActivityDetailView> ActivityDetails { get { return Set<ActivityDetailView>().AsNoTracking(); } }
        public DbQuery<TrainingTypeView> TrainingTypes { get { return Set<TrainingTypeView>().AsNoTracking(); } }
        public DbQuery<ProfileView> Profiles { get { return Set<ProfileView>().AsNoTracking(); } }
        public DbQuery<ActivityDetailTrainingCandidateView> ActivityDetailTrainingCandidates { get { return Set<ActivityDetailTrainingCandidateView>().AsNoTracking(); } }

        public IEnumerable<ActivityDetailTrainingAppointmentView> ListActivityDetailTrainingAppointments(Guid activityDetailTrainingId)
        {
            var query = Database.SqlQuery<ActivityDetailTrainingAppointmentView>("ActivityDetailTraining_ListAppointments {0}", activityDetailTrainingId);
            return query;
        }
    }
}