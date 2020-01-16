using System;

namespace Quintessence.TeamLeader.WebHook.DataAccess
{
    public interface IReplicationDataAccess
    {
        void RegisterCrmReplicationTeamLeaderEvent(string eventType, string objectType, string objectId);
    }
}
