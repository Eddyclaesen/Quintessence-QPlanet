using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeUser
    {
        [JsonProperty(PropertyName = "PrimaryKey")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "contactId")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "personId")]
        public int? PersonId { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "userGroup")]
        public string UserGroup { get; set; }

        [JsonProperty(PropertyName = "otherGroups")]
        public string OtherGroups { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        public void HtmlDecodeFields()
        {
        }

        #region Enriched Properties

        public string UserGroupParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(UserGroup); }
        }

        public string OtherGroupsParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(OtherGroups); }
        }
        #endregion


        #region Computed Properties

        [JsonIgnore()]
        public string Name
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }
        #endregion
    }
}
