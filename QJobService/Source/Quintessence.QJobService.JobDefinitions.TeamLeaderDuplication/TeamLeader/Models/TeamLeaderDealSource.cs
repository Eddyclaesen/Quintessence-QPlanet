using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderDealSource
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
        }
    }
}
