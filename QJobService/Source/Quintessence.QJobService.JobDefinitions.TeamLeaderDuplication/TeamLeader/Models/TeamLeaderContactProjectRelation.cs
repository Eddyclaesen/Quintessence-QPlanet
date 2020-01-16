using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderContactProjectRelation
    {
        [JsonProperty(PropertyName = "group")]
        public string Group { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? ObjectId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
            Role = System.Net.WebUtility.HtmlDecode(Role);
            Telephone = System.Net.WebUtility.HtmlDecode(Telephone);
            Email = System.Net.WebUtility.HtmlDecode(Email);
        }
    }
}