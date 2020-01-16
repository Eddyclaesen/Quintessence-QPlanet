using System;

namespace Quintessence.TeamLeader.WebHook.DataAccess
{
    public class ReplicationDataAccess : IReplicationDataAccess
    {
        public void RegisterCrmReplicationTeamLeaderEvent(string eventType, string objectType, string objectId)
        {
            CrmReplicationTeamLeaderEvent crmReplicationTeamLeaderEvent = new CrmReplicationTeamLeaderEvent
            {
                EventType = eventType,
                ObjectType = objectType,
                ObjectId = objectId,
                Source = "TL",
                ReceivedUtc = DateTime.UtcNow,
                ProcessCount = 0
            };

            using (var dbCtx = new QuintessenceEntities())
            {
                dbCtx.CrmReplicationTeamLeaderEvents.Add(crmReplicationTeamLeaderEvent);
                dbCtx.SaveChanges();
            }
        }
    }
}