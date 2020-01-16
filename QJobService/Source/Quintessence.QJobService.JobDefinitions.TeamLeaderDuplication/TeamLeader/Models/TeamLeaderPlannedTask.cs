using System;
using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderPlannedTask
    {
        [JsonProperty(PropertyName = "todo_id")]
        public int? TodoId { get; set; }

        [JsonProperty(PropertyName = "startdate")]
        public double? StartDateUnix { get; set; }

        [JsonProperty(PropertyName = "duration_minutes")]
        public int? DurationMinutes { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "project_id")]
        public int? ProjectId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? StartDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(StartDateUnix);
            }
        }
        #endregion

        public void HtmlDecodeFields()
        {
        }
    }
}
