using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.Authentication
{
    [DataContract]
    public class NegociateAuthenticationTokenResponse
    {
        [DataMember]
        public Guid AuthenticationTokenId { get; set; }
    }
}
