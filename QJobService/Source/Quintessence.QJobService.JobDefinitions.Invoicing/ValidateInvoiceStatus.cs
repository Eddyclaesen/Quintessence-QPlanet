using System;
using System.Configuration;
using System.Data.SqlClient;
using Quintessence.QJobService.Interfaces;

namespace Quintessence.QJobService.JobDefinitions.Invoicing
{
    public class ValidateInvoiceStatus : IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            //ProjectCandidate
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ConnectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "EXEC [dbo].[ProjectCandidate_ValidateInvoiceStatus]";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Something went wrong during ProjectCandidate_ValidateInvoiceStatus", exception);
            }

            //ProjectCandidateCategoryDetailType1
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ConnectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "EXEC [dbo].[ProjectCandidateCategoryDetailType1_ValidateInvoiceStatus]";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Something went wrong during ProjectCandidateCategoryDetailType1_ValidateInvoiceStatus", exception);
            }

            //ProjectCandidateCategoryDetailType2
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ConnectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "EXEC [dbo].[ProjectCandidateCategoryDetailType2_ValidateInvoiceStatus]";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Something went wrong during ProjectCandidateCategoryDetailType2_ValidateInvoiceStatus", exception);
            }

            //ProjectCandidateCategoryDetailType3
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ConnectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "EXEC [dbo].[ProjectCandidateCategoryDetailType3_ValidateInvoiceStatus]";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                jobControllerService.WriteError("Something went wrong during ProjectCandidateCategoryDetailType3_ValidateInvoiceStatus", exception);
            }
        }
    }
}
