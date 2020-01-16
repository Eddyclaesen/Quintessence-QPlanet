using System;
using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderMeeting
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "duration_minutes")]
        public int? DurationMinutes { get; set; }

        [JsonProperty(PropertyName = "date_timestamp")]
        public double? DateTimeStampUnix { get; set; }

        [JsonProperty(PropertyName = "date_day")]
        public string DateDay { get; set; }

        [JsonProperty(PropertyName = "date_hour")]
        public string DateHour { get; set; }

        [JsonProperty(PropertyName = "related_project_id")]
        public int? RelatedProjectId { get; set; }

        [JsonProperty(PropertyName = "related_milestone_id")]
        public int? RelatedMilestoneId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? DateTimeStamp
        {
            get { return TLC.DateTimeHelper.UnixTimeStampToDateTime(DateTimeStampUnix); }
        }

        [JsonIgnore()]
        public DateTime? MeetingDateTime
        {
            get { return TLC.DateTimeHelper.CombineDateAndTimeStringsToDateTime(DateDay, DateHour); }
        }
        #endregion

        public void HtmlDecodeFields()
        {
            Title = System.Net.WebUtility.HtmlDecode(Title);
        }
    }
}