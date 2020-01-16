using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderCompany
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "taxcode")]
        public string TaxCode { get; set; }

        [JsonProperty(PropertyName = "business_type")]
        public string BusinessType { get; set; }

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

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "fax")]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "iban")]
        public string Iban { get; set; }

        [JsonProperty(PropertyName = "bic")]
        public string Bic { get; set; }

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
        public string Afdeling { get; set; }

        [JsonIgnore()]
        public string AccountManagerTwo { get; set; }

        [JsonIgnore()]
        public string Assistent { get; set; }

        [JsonIgnore()]
        public string Business { get; set; }

        [JsonIgnore()]
        public string FidelisatieNorm { get; set; }

        [JsonIgnore()]
        public int? FocusList { get; set; }

        
        [JsonIgnore()]
        public string LinkedInProfile { get; set; }


        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            // TeamLeaders detail and list methods return customfields in different ways
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {
                    case TLC.Constants.CustomFieldNames.Company.LegacyId:
                        if (hasCustomFieldsProperty)
                            LegacyId = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LegacyId = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.Afdeling:
                        if (hasCustomFieldsProperty)
                            Afdeling = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Afdeling = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.Accountmanager:
                        if (hasCustomFieldsProperty)
                            AccountManagerTwo = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            AccountManagerTwo = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.Assistent:
                        if (hasCustomFieldsProperty)
                            Assistent = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Assistent = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.Business:
                        if (hasCustomFieldsProperty)
                            Business = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Business = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.Fidelisatienorm:
                        if (hasCustomFieldsProperty)
                            FidelisatieNorm = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            FidelisatieNorm = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.FocusList:
                        if (hasCustomFieldsProperty)
                            FocusList = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            FocusList = jObject[customField.PropertyName].ToObject<int?>();
                        break;
                    case TLC.Constants.CustomFieldNames.Company.LinkedInProfile:
                        if (hasCustomFieldsProperty)
                            LinkedInProfile = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            LinkedInProfile = jObject[customField.PropertyName].ToString();
                        break;        
                }
            }
        }
        #endregion

        public void HtmlDecodeFields()
        {
            Name = System.Net.WebUtility.HtmlDecode(Name);
            TaxCode = System.Net.WebUtility.HtmlDecode(TaxCode);
            BusinessType = System.Net.WebUtility.HtmlDecode(BusinessType);
            Street = System.Net.WebUtility.HtmlDecode(Street);
            Number = System.Net.WebUtility.HtmlDecode(Number);
            ZipCode = System.Net.WebUtility.HtmlDecode(ZipCode);
            City = System.Net.WebUtility.HtmlDecode(City);
            Country = System.Net.WebUtility.HtmlDecode(Country);
            WebSite = System.Net.WebUtility.HtmlDecode(WebSite);
            Email = System.Net.WebUtility.HtmlDecode(Email);
            Telephone = System.Net.WebUtility.HtmlDecode(Telephone);
            Fax = System.Net.WebUtility.HtmlDecode(Fax);
            Iban = System.Net.WebUtility.HtmlDecode(Iban);
            Bic = System.Net.WebUtility.HtmlDecode(Bic);
        }
    }
}