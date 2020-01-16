using System;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

using Newtonsoft.Json.Linq;
using Quintessence.SuperOffice.WebHook.DataAccess;

namespace Quintessence.SuperOffice.WebHook.WebHooks
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public class FieldNames
        {
            public static string EventType = "Event";
            public static string ObjectType = "Entity";
            public static string ObjectId = "PrimaryKey";
            public static string ObjectChanges = "Changes";
            public static string ByAssociateId = "ChangedByAssociateId";
        }

        public GenericJsonWebHookHandler()
        {
            this.Receiver = GenericJsonWebHookReceiver.ReceiverName;
        }

        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            JObject data = null;
            try
            {
                data = context.GetDataOrDefault<JObject>();
                IReplicationDataAccess da = new ReplicationDataAccess();

                int? byAssociateId = null;
                int tmp;
                if (int.TryParse(data[FieldNames.ByAssociateId].ToString(), out tmp))
                    byAssociateId = tmp;

                da.RegisterCrmReplicationSuperOfficeEvent(data[FieldNames.EventType].ToString(), 
                                                          data[FieldNames.ObjectType].ToString(), 
                                                          data[FieldNames.ObjectId].ToString(), 
                                                          data[FieldNames.ObjectChanges].ToString(),
                                                          byAssociateId);

                //For debug purposes:
                //string path = HttpContext.Current.Server.MapPath("~/App_Data/SuperOfficeEventLog.txt");
                //File.AppendAllText(path, String.Format("{0} - {1} - {2}", DateTime.UtcNow.ToLongTimeString(), data?.ToString() ?? "null", Environment.NewLine));

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {                
                string path = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");
                File.AppendAllText(path, String.Format("{0} - {1} - {2}", DateTime.UtcNow.ToLongTimeString(), data?.ToString() ?? "null", ex + Environment.NewLine));
                throw;
            }           
        }
    }
}