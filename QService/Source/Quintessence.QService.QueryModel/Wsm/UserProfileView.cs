using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.QueryModel.Wsm
{
    [DataContract(IsReference = true)]
    public class UserProfileView : ViewEntityBase
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public List<CrmUserEmailView> Emails { get; set; }

        [DataMember]
        public List<UserProfileContactView> Contacts { get; set; }
    }
}
