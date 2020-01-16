using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmAssociateView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int? UserGroupId { get; set; }

        [DataMember]
        public UserView User { get; set; }
    }
}
