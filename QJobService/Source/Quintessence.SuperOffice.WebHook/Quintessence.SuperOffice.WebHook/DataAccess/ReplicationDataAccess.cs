using System;

namespace Quintessence.SuperOffice.WebHook.DataAccess
{
    public class ReplicationDataAccess : IReplicationDataAccess
    {
        public void RegisterCrmReplicationSuperOfficeEvent(string eventType, string objectType, string objectId, string objectChanges, int? byAssociateId)
        {
            CrmReplicationSuperOfficeEvent crmEvent = new CrmReplicationSuperOfficeEvent
            {
                EventType = eventType,
                ObjectType = objectType,
                ObjectId = objectId,
                ObjectChanges = objectChanges,
                ByAssociateId = byAssociateId,
                Source = "SO",
                ReceivedUtc = DateTime.UtcNow,
                ProcessCount = 0
            };

            using (var dbCtx = new QuintessenceEntities())
            {
                dbCtx.CrmReplicationSuperOfficeEvent.Add(crmEvent);
                dbCtx.SaveChanges();
            }
        }
    }
}