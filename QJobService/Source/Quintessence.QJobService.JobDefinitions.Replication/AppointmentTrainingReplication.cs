using System;
using Quintessence.QJobService.Interfaces;

namespace Quintessence.QJobService.JobDefinitions.Replication
{
    public class AppointmentTrainingReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                ExecuteProcedure("EXEC	[dbo].[CrmReplicationAppointmentTraining_Sync]");
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Error running CrmReplicationAppointmentTraining_Sync", exception);
            }
        }
    }
}
