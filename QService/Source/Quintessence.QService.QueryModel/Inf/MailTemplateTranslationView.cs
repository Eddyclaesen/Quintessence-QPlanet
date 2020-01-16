using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class MailTemplateTranslationView : ViewEntityBase
    {
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Guid MailTemplateId { get; set; }

        [DataMember]
        public MailTemplateView MailTemplate { get; set; }
    }
}