using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeContact
    {
        [JsonProperty(PropertyName = "contactId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "streetAddress/line1")]
        public string StreetAddressLine1 { get; set; }

        [JsonProperty(PropertyName = "postAddress/line1")]
        public string PostAddressLine1 { get; set; }

        [JsonProperty(PropertyName = "postAddress/zip")]
        public string PostAddressZip { get; set; }

        [JsonProperty(PropertyName = "postAddress/city")]
        public string PostAddressCity { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "contactPhone/formattedNumber")]
        public string ContactPhone { get; set; }

        [JsonProperty(PropertyName = "ContactFax/formattedNumber")]
        public string ContactFax { get; set; }

        [JsonProperty(PropertyName = "url/URLAddress")]
        public string UrlAddress { get; set; }

        [JsonProperty(PropertyName = "email/emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "contactAssociate/fullName")]
        public string ContactAssociateFullName { get; set; }

        [JsonProperty(PropertyName = "contactAssociate/personId")]
        public int? ContactAssociatePersonId { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "business")]
        public string Business { get; set; }

        [JsonProperty(PropertyName = "orgNr")]
        public string OrgNr { get; set; }

        [JsonProperty(PropertyName = "number")]  // = TeamleaderId
        public int? Number { get; set; }

        [JsonProperty(PropertyName = "stop")]
        public bool Stop { get; set; }

        [JsonProperty(PropertyName = "contactNoMail")]
        public bool ContactNoMail { get; set; }

        [JsonProperty(PropertyName = "contactUdef/SuperOffice:2")]
        public string Assistent { get; set; }

        [JsonProperty(PropertyName = "contactUdef/SuperOffice:3")]
        public string FidelisatieNorm { get; set; }

        [JsonProperty(PropertyName = "contactUdef/SuperOffice:4")]
        public bool FocusList { get; set; }

        [JsonProperty(PropertyName = "updatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        #region Enriched Properties

        public string FidelisatieNormParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(FidelisatieNorm); }
        }

        public string CategoryParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(Category); }
        }

        public string BusinessParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(Business); }
        }
        #endregion
    }
}
