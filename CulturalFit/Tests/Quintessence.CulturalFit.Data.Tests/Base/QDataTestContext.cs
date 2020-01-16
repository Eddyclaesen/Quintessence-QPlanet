using Quintessence.CulturalFit.Infra.Configuration;
using Quintessence.CulturalFit.Infra.Logging;

namespace Quintessence.CulturalFit.Data.Tests.Base
{
    public class QDataTestContext: QContext
    {
        public QDataTestContext(IConfiguration configuration)
            : base(configuration)
        {
            using (new DurationLog("Building database"))
            {
                if (Database.Exists())
                    DestroyDatabase();
                Database.Create();
            }
        }

        private void DestroyDatabase()
        {
            using (new DurationLog("Destroying database"))
            {
                Database.ExecuteSqlCommand("ALTER DATABASE [" + Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                Database.Delete();
            }
        }

        protected override void Dispose(bool disposing)
        {
            DestroyDatabase();
            base.Dispose(disposing);
        }
    }
}
