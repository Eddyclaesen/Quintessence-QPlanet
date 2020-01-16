using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderDeal
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty(PropertyName = "quotation_nr")]
        public int? QuotationNr { get; set; }

        [JsonProperty(PropertyName = "total_price_excl_vat")]
        public decimal? TotalPriceExclVat { get; set; }

        [JsonProperty(PropertyName = "phase_id")]
        public int? PhaseId { get; set; }

        [JsonProperty(PropertyName = "responsible_user_id")]
        public int? ResponsibleUserId { get; set; }

        [JsonProperty(PropertyName = "entry_date")]
        public double? EntryDateUnix { get; set; }

        [JsonProperty(PropertyName = "latest_activity_date")]
        public double? LatestActivityDateUnix { get; set; }

        [JsonProperty(PropertyName = "close_date")]
        public double? CloseDateUnix { get; set; }

        [JsonProperty(PropertyName = "date_lost")]
        public double? DateLostUnix { get; set; }

        [JsonProperty(PropertyName = "reason_refused")]
        public string ReasonRefused { get; set; }

        [JsonProperty(PropertyName = "source_id")]
        public int? SourceId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? EntryDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(EntryDateUnix);
            }
        }

        [JsonIgnore()]
        public DateTime? LatestActivityDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(LatestActivityDateUnix);
            }
        }

        [JsonIgnore()]
        public DateTime? CloseDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(CloseDateUnix);
            }
        }

        [JsonIgnore()]
        public DateTime? DateLost
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(DateLostUnix);
            }
        }
        #endregion

        #region CustomFields

        [JsonIgnore()]
        public int? Probability { get; set; }
        #endregion

        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {    
                    case TLC.Constants.CustomFieldNames.Deal.Probability:
                        if (hasCustomFieldsProperty)
                            Probability = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Probability = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                }
            }
        }

        public void HtmlDecodeFields()
        {
            Title = System.Net.WebUtility.HtmlDecode(Title);
            CustomerName = System.Net.WebUtility.HtmlDecode(CustomerName);
        }
    }
}
