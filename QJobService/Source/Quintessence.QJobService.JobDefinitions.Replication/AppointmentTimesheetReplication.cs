using System;
using Quintessence.QJobService.Interfaces;

namespace Quintessence.QJobService.JobDefinitions.Replication
{
    public class AppointmentTimesheetReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                ExecuteProcedure("EXEC	[dbo].[CrmReplicationAppointmentTimesheet_Sync]");
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Error running CrmReplicationAppointmentTimesheet_Sync", exception);
            }
        }
    }
}
