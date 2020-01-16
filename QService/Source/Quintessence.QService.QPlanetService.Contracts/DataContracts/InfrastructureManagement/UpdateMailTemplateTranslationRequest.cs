using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class UpdateMailTemplateTranslationRequest : UpdateRequestBase
    {
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }
    }
}