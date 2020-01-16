using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Scm;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : IScmCommandContext
    {
        public IDbSet<Activity> Activities { get; set; }
        public IDbSet<ActivityProfile> ActivityProfiles { get; set; }
        public IDbSet<Profile> Profiles { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ActivityType> ActivityTypes { get; set; }
        public IDbSet<ActivityDetailTrainingLanguage> ActivityDetailTrainingLanguages { get; set; }
        public IDbSet<ActivityDetailWorkshopLanguage> ActivityDetailWorkshopLanguages { get; set; }
        public IDbSet<ActivityDetail> ActivityDetails { get; set; }
        public IDbSet<ActivityDetailCoaching> ActivityDetailCoachings { get; set; }
        public IDbSet<ActivityDetailConsulting> ActivityDetailConsultings { get; set; }
        public IDbSet<ActivityDetailSupport> ActivityDetailSupports { get; set; }
        public IDbSet<ActivityDetailTraining> ActivityDetailTrainings { get; set; }
        public IDbSet<ActivityDetailWorkshop> ActivityDetailWorkshops { get; set; }
        public IDbSet<ActivityDetailTraining2TrainingType> ActivityDetailTraining2TrainingTypes { get; set; }
        public IDbSet<ActivityType2Profile> ActivityType2Profiles { get; set; }
        public IDbSet<ProductType> ProductTypes { get; set; }
    }
}
