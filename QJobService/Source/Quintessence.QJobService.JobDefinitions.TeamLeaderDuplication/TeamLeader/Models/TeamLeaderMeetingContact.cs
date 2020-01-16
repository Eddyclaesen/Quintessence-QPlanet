using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderMeetingContact
    {

        [JsonProperty(PropertyName = "contact_id")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}