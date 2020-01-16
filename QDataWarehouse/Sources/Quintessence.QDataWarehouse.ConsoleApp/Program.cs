using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QDataWarehouse.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataWarehouse"].ConnectionString))
                {
                    var command = new SqlCommand { Connection = connection, CommandType = CommandType.StoredProcedure, CommandText = "SyncSo" };
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    Console.WriteLine("{0} lines modified", result);
                    //Console.ReadKey();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                //Console.ReadKey();
            }
        }
    }
}
