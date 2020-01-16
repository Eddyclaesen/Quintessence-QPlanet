using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Quintessence.QJobService.JobDefinitions.Replication
{
    public abstract class ReplicationBase
    {
        protected static void ExecuteProcedure(string commandText)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = commandText;
                    command.CommandTimeout = (int)TimeSpan.FromMinutes(2).TotalSeconds;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}