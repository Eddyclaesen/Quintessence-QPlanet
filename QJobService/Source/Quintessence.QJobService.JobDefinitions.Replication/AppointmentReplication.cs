using System;
using Quintessence.QJobService.Interfaces;

namespace Quintessence.QJobService.JobDefinitions.Replication
{
    public class AppointmentReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                ExecuteProcedure("EXEC	[dbo].[CrmReplicationAppointment_Sync]");
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Error running CrmReplicationAppointment_Sync", exception);
            }
        }
    }
}
