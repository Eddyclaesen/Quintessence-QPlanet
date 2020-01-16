using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class Project
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        
        [JsonProperty(PropertyName = "phase")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public string StartDate { get; set; }

        [JsonProperty(PropertyName = "start_date_formatted")]
        public string StartDateFormatted { get; set; }

        [JsonProperty(PropertyName = "contact_or_company")]
        public string ContactOrCompany { get; set; }

        [JsonProperty(PropertyName = "contact_or_company_id")]
        public string ContactOrCompanyId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? BoekJaarStartDate
        {
            get
            {
                DateTime? startDate, endDate;
                DeriveBoekJaarDates(out startDate, out endDate);
                return startDate;

            }
        }

        [JsonIgnore()]
        public DateTime? BoekJaarEndDate
        {
            get
            {
                DateTime? startDate, endDate;
                DeriveBoekJaarDates(out startDate, out endDate);
                return endDate;
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
                    case Constants.CustomFieldNames.Project.LegacyId:
                        if (hasCustomFieldsProperty)
                            LegacyId = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LegacyId = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case Constants.CustomFieldNames.Project.Boekjaar:
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

        #region Enriched Properties

        [JsonIgnore()]
        public int? StatusId { get; set; }

        [JsonIgnore()]
        public int? ContactId { get; set; }

        [JsonIgnore()]
        public int? AssociateId { get; set; }
        #endregion

        public void HtmlDecodeFields()
        {
            Title = System.Net.WebUtility.HtmlDecode(Title);
        }
 
        public bool IsOfCurrentBookYear()
        {
            DateTime? startDate, endDate;
            DeriveBoekJaarDates(out startDate, out endDate);
            if (startDate.HasValue && endDate.HasValue)
            {
                return startDate < DateTime.Today && endDate > DateTime.Today;
            }
            return false;
        }

        private void DeriveBoekJaarDates(out DateTime? startDate, out DateTime? endDate)
        {
            startDate = null;
            endDate = null;
            if (!String.IsNullOrEmpty(BoekJaar) && BoekJaar.Contains(@"/"))
            {
                string[] parts = BoekJaar.Split('/');
                int startYear, endYear;
                if (parts.Length == 2 && int.TryParse(parts[0], out startYear) && int.TryParse(parts[1], out endYear))
                {
                    startDate = new DateTime(startYear + 2000, 4, 1);
                    endDate =  new DateTime(endYear + 2000, 3, 31, 23, 59, 59);
                }
            }   
            if (!String.IsNullOrEmpty(BoekJaar) && BoekJaar.Length == 4)
            {
                int Year;
                if (int.TryParse(BoekJaar, out Year))
                {
                    startDate = new DateTime(Year, 1, 1);
                    endDate = new DateTime(Year, 12, 31, 23, 59, 59);
                }
                
            }
        }

    }
}
