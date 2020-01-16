using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class Company
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "account_manager_id")]
        public string Associate { get; set; }

        #region CustomFields

        [JsonIgnore()]
        public int? LegacyId { get; set; }

        [JsonIgnore()]
        public string Afdeling { get; set; }

        [JsonIgnore()]
        public string AccountManager { get; set; }

        [JsonIgnore()]
        public string Assistent { get; set; }

        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            // TeamLeaders detail and list methods return customfields in different ways
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {
                    case Constants.CustomFieldNames.Company.LegacyId:
                        if (hasCustomFieldsProperty)
                            LegacyId = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LegacyId = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case Constants.CustomFieldNames.Company.Afdeling:
                        if (hasCustomFieldsProperty)
                            Afdeling = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Afdeling = jObject[customField.PropertyName].ToString();
                        break;
                    case Constants.CustomFieldNames.Company.Accountmanager:
                        if (hasCustomFieldsProperty)
                            AccountManager = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            AccountManager = jObject[customField.PropertyName].ToString();
                        break;
                    case Constants.CustomFieldNames.Company.Assistent:
                        if (hasCustomFieldsProperty)
                            Assistent = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Assistent = jObject[customField.PropertyName].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? AssociateId { get; set; }

        [JsonIgnore()]
        public int? AccountManagerId { get; set; }

        [JsonIgnore()]
        public int? AssistentId { get; set; }

        #endregion
        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
            Afdeling = System.Net.WebUtility.HtmlDecode(Afdeling);
        }

        public bool HasUpDates(CrmReplicationContact contact)
        {
            if (LegacyId.HasValue && LegacyId != contact.Id)
                return true;
            if(Name != contact.Name)
                return true;
            if (Afdeling != contact.Department)
                return true;
            if (AccountManagerId != contact.AccountManagerId)
                return true;
            if (AssistentId != contact.AssociateId)
                return true;

            return false;
        }
    }
}