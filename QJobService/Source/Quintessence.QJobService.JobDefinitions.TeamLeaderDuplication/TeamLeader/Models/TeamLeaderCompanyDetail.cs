using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderCompanyDetail
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "sector_name")]
        public string Sector { get; set; }

        [JsonProperty(PropertyName = "account_manager_id")]
        public int? AccountManagerId { get; set; }
        
        public void HtmlDecodeFields()
        {
            Sector = System.Net.WebUtility.HtmlDecode(Sector);
        }
    }
}