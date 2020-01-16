using Newtonsoft.Json;


namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class CompanyContact
    {
        [JsonProperty(PropertyName = "id")]
        public int? ContactTeamLeaderId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string DirectPhone { get; set; }

        [JsonProperty(PropertyName = "gsm")]
        public string MobilePhone { get; set; }

        #region Enriched Properties

        [JsonIgnore()]
        public int? ContactLegacyId { get; set; }

        [JsonIgnore()]
        public string FirstName { get; set; }

        [JsonIgnore()]
        public string LastName { get; set; }

        #endregion

        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
        }
    }
}

