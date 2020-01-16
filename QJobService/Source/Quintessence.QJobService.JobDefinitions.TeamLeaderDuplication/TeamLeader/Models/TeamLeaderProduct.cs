using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderProduct
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "external_id")]
        public int? ExternalId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "vat")]
        public decimal? Vat { get; set; }

        [JsonProperty(PropertyName = "booking_account")]
        public string BookingAccount { get; set; }

        [JsonProperty(PropertyName = "booking_account_id")]
        public int? BookingAccountId { get; set; }

        [JsonProperty(PropertyName = "stock")]
        public int? Stock { get; set; }

        [JsonProperty(PropertyName = "price_value_16007")]
        public decimal? Price { get; set; }

        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
        }
    }
}