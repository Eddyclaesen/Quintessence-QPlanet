using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.Service.Contracts.DataContracts
{
    [DataContract]
    public class SearchTheoremListRequestsRequest
    {
        [DataMember]
        public string VerificationCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CandidateName { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}
