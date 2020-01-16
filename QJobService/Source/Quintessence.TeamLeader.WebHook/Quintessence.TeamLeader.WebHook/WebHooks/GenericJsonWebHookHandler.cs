using System;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

using Newtonsoft.Json.Linq;
using Quintessence.TeamLeader.WebHook.DataAccess;

namespace Quintessence.TeamLeader.WebHook.WebHooks
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public class FieldNames
        {
            public static string EventType = "event_type";
            public static string ObjectType = "object_type";
            public static string ObjectId = "object_id";
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
                da.RegisterCrmReplicationTeamLeaderEvent(data[FieldNames.EventType].ToString(), data[FieldNames.ObjectType].ToString(), data[FieldNames.ObjectId].ToString());
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