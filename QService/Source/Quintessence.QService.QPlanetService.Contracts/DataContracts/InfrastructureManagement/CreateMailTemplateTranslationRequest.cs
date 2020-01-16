using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class CreateMailTemplateTranslationRequest
    {
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Guid MailTemplateId { get; set; }
    }
}