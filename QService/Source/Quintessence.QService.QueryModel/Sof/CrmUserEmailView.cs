using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmUserEmailView
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public UserView User { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int Rank { get; set; }

        //No Datamember!
        public Guid UserProfileId
        {
            get { return UserId; }
            set { UserId = value; }
        }

        [DataMember]
        public UserProfileView UserProfile  { get; set; }
    }
}
