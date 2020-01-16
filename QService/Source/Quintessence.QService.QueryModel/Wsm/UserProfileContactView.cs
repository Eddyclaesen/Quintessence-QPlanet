using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Wsm
{
    [DataContract(IsReference = true)]
    public class UserProfileContactView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid UserProfileId { get; set; }

        [DataMember]
        public UserProfileView UserProfile { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ContactDepartment { get; set; }

        public string ContactFullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(ContactDepartment)
                  ? ContactName
                  : string.Format("{0} ({1})", ContactName, ContactDepartment);
            }
        }
    }
}
