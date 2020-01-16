using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class ProjectUser
    {
        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        #region Enriched Properties

        [JsonIgnore()]
        public int? UserLegacyId { get; set; }
        #endregion
    }
}
