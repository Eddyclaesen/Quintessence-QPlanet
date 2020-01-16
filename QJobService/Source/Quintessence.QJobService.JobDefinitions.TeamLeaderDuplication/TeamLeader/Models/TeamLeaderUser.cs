using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderUser
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "gsm")]
        public string Gsm { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool? Active { get; set; }

        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
            Email = System.Net.WebUtility.HtmlDecode(Email);
            Gsm = System.Net.WebUtility.HtmlDecode(Gsm);
            Telephone = System.Net.WebUtility.HtmlDecode(Telephone);
            Title = System.Net.WebUtility.HtmlDecode(Title);
        }
    }
}
