using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderContactCompanyRelation
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "contact_id")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "company_id")]
        public int? CompanyId { get; set; }

        [JsonProperty(PropertyName = "function")]
        public string Function { get; set; }

        public void HtmlDecodeFields()
        {
            Function = System.Net.WebUtility.HtmlDecode(Function);
        }
    }
}