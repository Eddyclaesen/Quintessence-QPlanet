using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Crm
{
    [DataContract]
    public class ContactDetailView : ViewEntityBase
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string Remarks { get; set; }
    }
}
