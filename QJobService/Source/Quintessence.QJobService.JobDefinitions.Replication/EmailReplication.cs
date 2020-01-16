using System;
using Quintessence.QJobService.Interfaces;

namespace Quintessence.QJobService.JobDefinitions.Replication
{
    public class EmailReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                ExecuteProcedure("EXEC	[dbo].[CrmReplicationEmail_Sync]");
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Error running CrmReplicationEmail_Sync", exception);
            }
        }
    }
}
