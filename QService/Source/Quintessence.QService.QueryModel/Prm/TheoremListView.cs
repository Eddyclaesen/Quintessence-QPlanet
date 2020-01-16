using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class TheoremListView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int TheoremListTypeId { get; set; }

        [DataMember]
        public Guid TheoremListRequestId { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }

        [DataMember]
        public bool IsCompleted { get; set; }
    }
}