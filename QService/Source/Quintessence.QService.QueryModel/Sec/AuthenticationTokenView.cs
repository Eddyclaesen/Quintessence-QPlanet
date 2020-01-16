using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sec
{
    [DataContract(IsReference = true)]
    public class AuthenticationTokenView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public DateTime ValidFrom { get; set; }

        [DataMember]
        public DateTime ValidTo { get; set; }

        [DataMember]
        public DateTime IssuedOn { get; set; }

        [DataMember]
        public UserView User { get; set; }
    }
}
