using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class Contact
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "forename")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "gsm")]
        public string Gsm { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "linked_company_ids")]
        public string LinkedCompanyIds { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public bool? IsFormerEmployee
        {
            get { return String.IsNullOrEmpty(FormerEmployee) ? (bool?)null : FormerEmployee != "0"; }
        }
        #endregion

        #region CustomFields

        [JsonIgnore()]
        public int? LegacyId { get; set; }

        [JsonIgnore()]
        public string Positie { get; set; }

        [JsonIgnore()]
        public string FormerEmployee { get; set; }


        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            // TeamLeaders detail and list methods return customfields in different ways
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {
                    case Constants.CustomFieldNames.Contact.LegacyId:
                        if (hasCustomFieldsProperty)
                            LegacyId = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LegacyId = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case Constants.CustomFieldNames.Contact.Positie:
                        if (hasCustomFieldsProperty)
                        {
                            int? positieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (positieKey.HasValue && customField.Options.ContainsKey(positieKey.Value))
                                Positie = customField.Options[positieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Positie = jObject[customField.PropertyName].ToString();
                        break;
                    case Constants.CustomFieldNames.Contact.FormerEmployee:
                        if (hasCustomFieldsProperty)
                            FormerEmployee = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            FormerEmployee = jObject[customField.PropertyName].ToString();
                        break;
                }
            }
        }

        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? LinkedCompanyId { get; set; }

        [JsonIgnore()]
        public string LinkedCompanyName { get; set; }

        #endregion

        public void HtmlDecodeFields()
        {
            FirstName = System.Net.WebUtility.HtmlDecode(FirstName);
            LastName = System.Net.WebUtility.HtmlDecode(LastName);
        }
    }
}