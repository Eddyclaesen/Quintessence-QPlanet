using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class PlannedTask
    {
        [JsonProperty(PropertyName = "todo_id")]
        public int? TaskId { get; set; }

        [JsonProperty(PropertyName = "startdate")]
        public double? StartDate { get; set; }

        [JsonProperty(PropertyName = "duration_minutes")]
        public int? Duration { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "project_id")]
        public int? ProjectId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? AppointmentStartDateTime
        {
            get
            {
                return DateTimeHelper.UnixTimeStampToDateTime(StartDate);
            }
        }
        #endregion
    }
}
