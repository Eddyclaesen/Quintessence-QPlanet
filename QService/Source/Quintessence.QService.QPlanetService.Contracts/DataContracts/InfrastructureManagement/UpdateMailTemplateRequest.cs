using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class UpdateMailTemplateRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FromAddress { get; set; }

        [DataMember]
        public string BccAddress { get; set; }

        [DataMember]
        public List<UpdateMailTemplateTranslationRequest> MailTemplateTranslations { get; set; }
    }
}