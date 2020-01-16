using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficePerson
    {
        [JsonProperty(PropertyName = "personId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "contactId")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "mrmrs")]
        public string MrMrs { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "email/emailAddress")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone/formattedNumber")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "retired")]
        public bool Retired { get; set; }

        [JsonProperty(PropertyName = "personUdef/SuperOffice:1")]
        public bool Inside { get; set; }

        [JsonProperty(PropertyName = "personUdef/SuperOffice:2")]
        public bool Nps { get; set; }

        [JsonProperty(PropertyName = "personUdef/SuperOffice:3")]
        public bool CommercialEmails { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public string FullName
        {
            get
            {                
                return FirstName + " " + LastName;
            }
        }
        #endregion
    }
}
