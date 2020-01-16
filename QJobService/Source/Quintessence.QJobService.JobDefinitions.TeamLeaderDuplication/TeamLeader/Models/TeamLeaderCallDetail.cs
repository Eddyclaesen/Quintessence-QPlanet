using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderCallDetail
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        #region CustomFields        

        [JsonIgnore()]
        public string Klant { get; set; }

        [JsonIgnore()]
        public string Intern { get; set; }

        [JsonIgnore()]
        public string Sales { get; set; }

        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            // TeamLeaders detail and list methods return customfields in different ways
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {
                    case TLC.Constants.CustomFieldNames.Call.Klant:
                        if (hasCustomFieldsProperty)
                        {
                            int? klantKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (klantKey.HasValue && customField.Options.ContainsKey(klantKey.Value))
                                Klant = customField.Options[klantKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Klant = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Call.Intern:
                        if (hasCustomFieldsProperty)
                        {
                            int? internKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (internKey.HasValue && customField.Options.ContainsKey(internKey.Value))
                                Intern = customField.Options[internKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Intern = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Call.Sales:
                        if (hasCustomFieldsProperty)
                        {
                            int? salesKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (salesKey.HasValue && customField.Options.ContainsKey(salesKey.Value))
                                Sales = customField.Options[salesKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Sales = jObject[customField.PropertyName].ToString();
                        break;
                }
            }
        }
        #endregion

        public void HtmlDecodeFields()
        {
            Klant = System.Net.WebUtility.HtmlDecode(Klant);
            Intern = System.Net.WebUtility.HtmlDecode(Intern);
            Sales = System.Net.WebUtility.HtmlDecode(Sales);
        }
    }
}