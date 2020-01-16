using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderContact
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "forename")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string WebSite { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "gsm")]
        public string Gsm { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "fax")]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "language_code")]
        public string LanguageCode { get; set; }

        [JsonProperty(PropertyName = "date_added")]
        public double? DateAddedUnix { get; set; }

        [JsonProperty(PropertyName = "date_edited")]
        public double? DateEditedUnix { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? DateAdded
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(DateAddedUnix);
            }
        }

        [JsonIgnore()]
        public DateTime? DateEdited
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(DateEditedUnix);
            }
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
                    case TLC.Constants.CustomFieldNames.Contact.LegacyId:
                        if (hasCustomFieldsProperty)
                            LegacyId = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LegacyId = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case TLC.Constants.CustomFieldNames.Contact.Positie:
                        if (hasCustomFieldsProperty)
                        {
                            int? positieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (positieKey.HasValue && customField.Options.ContainsKey(positieKey.Value))
                                Positie = customField.Options[positieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Positie = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Contact.FormerEmployee:
                        if (hasCustomFieldsProperty)
                            FormerEmployee = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            FormerEmployee = jObject[customField.PropertyName].ToString();
                        break;
                }
            }
        }

        #endregion

        public void HtmlDecodeFields()
        {
            FirstName = System.Net.WebUtility.HtmlDecode(FirstName);
            LastName = System.Net.WebUtility.HtmlDecode(LastName);
            Street = System.Net.WebUtility.HtmlDecode(Street);
            Number = System.Net.WebUtility.HtmlDecode(Number);
            ZipCode = System.Net.WebUtility.HtmlDecode(ZipCode);
            City = System.Net.WebUtility.HtmlDecode(City);
            Country = System.Net.WebUtility.HtmlDecode(LastName);
            WebSite = System.Net.WebUtility.HtmlDecode(WebSite);
            Email = System.Net.WebUtility.HtmlDecode(Email);
            Gsm = System.Net.WebUtility.HtmlDecode(Gsm);
            Telephone = System.Net.WebUtility.HtmlDecode(Telephone);
            Fax = System.Net.WebUtility.HtmlDecode(Fax);
        }
    }
}