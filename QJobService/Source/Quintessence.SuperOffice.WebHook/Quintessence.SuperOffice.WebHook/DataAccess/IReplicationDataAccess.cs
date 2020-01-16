using System;

namespace Quintessence.SuperOffice.WebHook.DataAccess
{
    public interface IReplicationDataAccess
    {
        void RegisterCrmReplicationSuperOfficeEvent(string eventType, string objectType, string objectId, string objectChanges, int? byAssociateId);
    }
}
