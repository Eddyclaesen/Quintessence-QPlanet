using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderTaskDetail
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "due_date")]
        public double? UnixDueDate { get; set; }

        [JsonIgnore()]
        public int? ResponsibleUserId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? DueDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(UnixDueDate);
            }
        }
        #endregion

        public void FillResponsibleUser(JObject jObject)
        {
            // TeamLeaders detail and list methods return customfields in different ways            
            IDictionary<string, JToken> dictionary = jObject;

            if (dictionary.ContainsKey("responsible_users"))
            {
                JObject responsibleUsers = dictionary["responsible_users"] as JObject;
                if (responsibleUsers != null && responsibleUsers["users"] != null)
                {
                    ResponsibleUserId = (int)responsibleUsers["users"][0];
                }                
            }
        }

        public void HtmlDecodeFields()
        {
        }
    }
}