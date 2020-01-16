using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class Person
    {
        public Person()
        {
            MobilePhones = new List<PersonPhone>();
            PersonEmails = new List<PersonEmail>();
        }

        [JsonProperty(PropertyName = "personId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "personNumber")]  // = TeamleaderId
        public int? LegacyId { get; set; }

        [JsonProperty(PropertyName = "UserDefinedFields")]
        public UserDefinedFields UserdefinedFields { get; set; }

        [JsonProperty(PropertyName = "Emails")]
        public List<PersonEmail> PersonEmails { get; set; }

        [JsonProperty(PropertyName = "MobilePhones")]
        public List<PersonPhone> MobilePhones { get; set; }

        [JsonProperty(PropertyName = "OfficePhones")]
        public List<PersonPhone> OfficePhones { get; set; }

        [JsonProperty(PropertyName = "Retired")]
        public bool Retired { get; set; }

        [JsonProperty(PropertyName = "Contact")]
        public PersonContact PersonContact { get; set; }

        [JsonProperty(PropertyName = "Position")]
        public PersonPosition PersonPosition { get; set; }
        
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public string Position
        {
            get
            {
                return (PersonPosition != null) ? PersonPosition.Value : String.Empty;
            }
        }

        [JsonIgnore()]
        public string Email
        {
            get
            {
                if(PersonEmails.Any())
                    return PersonEmails[0].Value;
                return String.Empty;
            }
        }

        [JsonIgnore()]
        public string MobilePhone
        {
            get
            {
                if (MobilePhones.Any())
                    return MobilePhones[0].Value;
                return String.Empty;
            }
        }

        [JsonIgnore()]
        public string DirectPhone
        {
            get
            {
                if (OfficePhones.Any())
                    return OfficePhones[0].Value;
                return String.Empty;
            }
        }

        [JsonIgnore()]
        public int? ContactId
        {
            get { return (PersonContact != null ) ? PersonContact.ContactId : null; }
        }
        #endregion

        #region CustomFields

        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? LinkedCompanyId { get; set; }

        [JsonIgnore()]
        public string LinkedCompanyName { get; set; }

        [JsonIgnore()]
        public int? CrmReplicationPersonId { get; set; }

        #endregion

        public void HtmlDecodeFields()
        {
        }
    }

    public class PersonEmail
    {
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }

    public class PersonPhone
    {
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }

    public class PersonContact
    {
        [JsonProperty(PropertyName = "ContactId")]
        public int? ContactId { get; set; }
    }

    public class PersonPosition
    {
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }
}
