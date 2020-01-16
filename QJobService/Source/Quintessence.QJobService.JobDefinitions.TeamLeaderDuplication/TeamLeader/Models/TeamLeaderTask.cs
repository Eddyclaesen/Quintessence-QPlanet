using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderTask
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "project_id")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "finished")]
        public bool? IsFinished { get; set; }

        #region CustomFields

        [JsonIgnore()]
        public string FirstName { get; set; }

        [JsonIgnore()]
        public string LastName { get; set; }

        [JsonIgnore()]
        public string Gender { get; set; }

        [JsonIgnore()]
        public string Language { get; set; }

        [JsonIgnore()]
        public string Code { get; set; }

        [JsonIgnore()]
        public string Lokatie { get; set; }

        [JsonIgnore()]
        public int? Reservatie { get; set; }

        [JsonIgnore()]
        public string Intern { get; set; }

        [JsonIgnore()]
        public string Klant { get; set; }

        [JsonIgnore()]
        public string Overige { get; set; }

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
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.FirstName:
                        if (hasCustomFieldsProperty)
                            FirstName = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            FirstName = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.LastName:
                        if (hasCustomFieldsProperty)
                            LastName = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LastName = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Gender:
                        if (hasCustomFieldsProperty)
                        {
                            int? genderKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (genderKey.HasValue && customField.Options.ContainsKey(genderKey.Value))
                                Gender = customField.Options[genderKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Gender = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Language:
                        if (hasCustomFieldsProperty)
                        {
                            int? languageKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (languageKey.HasValue && customField.Options.ContainsKey(languageKey.Value))
                                Language = customField.Options[languageKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Language = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Code:
                        if (hasCustomFieldsProperty)
                            Code = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Code = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Reservatie:
                        if (hasCustomFieldsProperty)
                            Reservatie = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Reservatie = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Lokatie:
                        if (hasCustomFieldsProperty)
                        {
                            int? lokatieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (lokatieKey.HasValue && customField.Options.ContainsKey(lokatieKey.Value))
                                Lokatie = customField.Options[lokatieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Lokatie = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Intern:
                        if (hasCustomFieldsProperty)
                        {
                            int? internKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (internKey.HasValue && customField.Options.ContainsKey(internKey.Value))
                                Intern = customField.Options[internKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Intern = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Klant:
                        if (hasCustomFieldsProperty)
                        {
                            int? klantKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (klantKey.HasValue && customField.Options.ContainsKey(klantKey.Value))
                                Klant = customField.Options[klantKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Klant = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Overige:
                        if (hasCustomFieldsProperty)
                        {
                            int? overigeKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (overigeKey.HasValue && customField.Options.ContainsKey(overigeKey.Value))
                                Overige = customField.Options[overigeKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Overige = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.TeamLeaderTask.Sales:
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
            Description = System.Net.WebUtility.HtmlDecode(Description);
            FirstName = System.Net.WebUtility.HtmlDecode(FirstName);
            Intern = System.Net.WebUtility.HtmlDecode(Intern);
            Klant = System.Net.WebUtility.HtmlDecode(Klant);
            LastName = System.Net.WebUtility.HtmlDecode(LastName);
            Overige = System.Net.WebUtility.HtmlDecode(Overige);
            Sales = System.Net.WebUtility.HtmlDecode(Sales);
        }
    }
}