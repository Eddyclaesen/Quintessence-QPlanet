using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderProject
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "phase")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public double? UnixStartDate { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? StartDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(UnixStartDate);
            }
        }
        #endregion

        #region CustomFields

        [JsonIgnore()]
        public int? LegacyId { get; set; }

        [JsonIgnore()]
        public string BoekJaar { get; set; }

        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            // TeamLeaders detail and list methods return customfields in different ways
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {
                    case TLC.Constants.CustomFieldNames.Project.LegacyId:
                        if (hasCustomFieldsProperty)
                            LegacyId = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                        {
                            string legacyIdAsString = jObject[customField.PropertyName].ToString();
                            int legacyId;
                            if(int.TryParse(legacyIdAsString, out legacyId))
                                LegacyId = legacyId;
                        }
                        break;
                    case TLC.Constants.CustomFieldNames.Project.Boekjaar:
                        if (hasCustomFieldsProperty)
                        {
                            int? boekJaarKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (boekJaarKey.HasValue && customField.Options.ContainsKey(boekJaarKey.Value))
                                BoekJaar = customField.Options[boekJaarKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            BoekJaar = jObject[customField.PropertyName].ToString();
                        break;
                }
            }
        }
        #endregion

        public void HtmlDecodeFields()
        {
            Title = System.Net.WebUtility.HtmlDecode(Title);
        }
    }
}