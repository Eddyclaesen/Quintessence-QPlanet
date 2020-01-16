using System;
using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderCall
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "due_date")]
        public double? DueDateUnix { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "date_day")]
        public string DateDay { get; set; }

        [JsonProperty(PropertyName = "date_hour")]
        public string DateHour { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "gsm")]
        public string Gsm { get; set; }

        [JsonProperty(PropertyName = "related_project_id")]
        public int? RelatedProjectId { get; set; }

        [JsonProperty(PropertyName = "related_milestone_id")]
        public int? RelatedMilestoneId { get; set; }

        [JsonProperty(PropertyName = "for")]
        public string ForType { get; set; }

        [JsonProperty(PropertyName = "for_id")]
        public int? ForId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? DueDate
        {
            get { return TLC.DateTimeHelper.UnixTimeStampToDateTime(DueDateUnix); }
        }

        [JsonIgnore()]
        public DateTime? CallDateTime
        {
            get { return TLC.DateTimeHelper.CombineDateAndTimeStringsToDateTime(DateDay, DateHour); }
        }
        #endregion

        public void HtmlDecodeFields()
        {
            UserName = System.Net.WebUtility.HtmlDecode(UserName);
            Telephone = System.Net.WebUtility.HtmlDecode(Telephone);
            Gsm = System.Net.WebUtility.HtmlDecode(Gsm);
        }
    }
}