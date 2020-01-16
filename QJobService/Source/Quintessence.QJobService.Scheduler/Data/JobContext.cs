using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Quintessence.QJobService.Scheduler.Model;

namespace Quintessence.QJobService.Scheduler.Data
{
    public class JobContext : DbContext
    {
        public JobContext()
            : base("Quintessence")
        {
        }

        public IDbSet<JobDefinition> JobDefinitions { get; set; }
        public IDbSet<Job> Jobs { get; set; }
        public IDbSet<JobSchedule> JobSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
