using System;
using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.Service.Contracts.DataContracts
{
    [DataContract]
    public class RetrieveTheoremListRequestRequest
    {
        public RetrieveTheoremListRequestRequest()
        {
        }

        public RetrieveTheoremListRequestRequest(string verificationcode)
            : this()
        {
            VerificationCode = verificationcode;
        }

        public RetrieveTheoremListRequestRequest(Guid theoremListRequestId)
            : this()
        {
            Id = theoremListRequestId;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }
    }
}
