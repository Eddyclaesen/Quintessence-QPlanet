using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class TimeTracking
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "project_id")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    
        [JsonProperty(PropertyName = "project_title")]
        public string ProjectTitle { get; set; }

        [JsonProperty(PropertyName = "date")]
        public double? Date { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? StartDate
        {
            get
            {
                return DateTimeHelper.UnixTimeStampToDateTime(Date);
            }
        }

        [JsonIgnore()]
        public DateTime? EndDate
        {
            get
            {
                if (Duration.HasValue)
                {
                    DateTime? startDate = DateTimeHelper.UnixTimeStampToDateTime(Date);
                    if(startDate.HasValue)
                        return startDate.Value.AddMinutes(Duration.Value);
                }
                return null;
            }
        }
        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? ProjectLegacyId { get; set; }

        [JsonIgnore()]
        public int? AssociateId { get; set; }
        #endregion

        public void HtmlDecodeFields()
        {
            Description = System.Net.WebUtility.HtmlDecode(Description);
            ProjectTitle = System.Net.WebUtility.HtmlDecode(ProjectTitle);
        }
    }
}
