using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "PrimaryKey")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonIgnore()]
        public int? AssociateId { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public string Email
        {
            get
            {
                return UserName;
            }
        }

        #endregion

        public void HtmlDecodeFields()
        {
            //Name = System.Net.WebUtility.HtmlDecode(Name);
        }
    }
}
