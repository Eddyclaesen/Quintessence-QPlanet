using System;
using Quintessence.QJobService.Interfaces;

namespace Quintessence.QJobService.JobDefinitions.Replication
{
    public class ProjectReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                ExecuteProcedure("EXEC	[dbo].[CrmReplicationProject_Sync]");
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Error running CrmReplicationProject_Sync", exception);
            }
        }
    }
}
