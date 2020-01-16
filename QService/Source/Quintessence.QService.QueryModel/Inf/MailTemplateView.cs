using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class MailTemplateView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string FromAddress { get; set; }

        [DataMember]
        public string BccAddress { get; set; }

        [DataMember]
        public string StoredProcedureName { get; set; }

        [DataMember]
        public List<MailTemplateTranslationView> MailTemplateTranslations { get; set; }
    }
}