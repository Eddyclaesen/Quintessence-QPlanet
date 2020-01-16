using System.Data.Entity;
using Quintessence.QService.DataModel.Scm;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    public interface IScmCommandContext : IQuintessenceCommandContext
    {
        IDbSet<Activity> Activities { get; set; }
        IDbSet<ActivityProfile> ActivityProfiles { get; set; }
        IDbSet<Profile> Profiles { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<ActivityType> ActivityTypes { get; set; }
        IDbSet<ActivityDetailTrainingLanguage> ActivityDetailTrainingLanguages { get; set; }
        IDbSet<ActivityDetailWorkshopLanguage> ActivityDetailWorkshopLanguages { get; set; }
        IDbSet<ActivityDetail> ActivityDetails { get; set; }
        IDbSet<ActivityDetailCoaching> ActivityDetailCoachings { get; set; }
        IDbSet<ActivityDetailConsulting> ActivityDetailConsultings { get; set; }
        IDbSet<ActivityDetailSupport> ActivityDetailSupports { get; set; }
        IDbSet<ActivityDetailTraining> ActivityDetailTrainings { get; set; }
        IDbSet<ActivityDetailWorkshop> ActivityDetailWorkshops { get; set; }
        IDbSet<ActivityDetailTraining2TrainingType> ActivityDetailTraining2TrainingTypes { get; set; }
        IDbSet<ActivityType2Profile> ActivityType2Profiles { get; set; }
        IDbSet<ProductType> ProductTypes { get; set; }
    }
}