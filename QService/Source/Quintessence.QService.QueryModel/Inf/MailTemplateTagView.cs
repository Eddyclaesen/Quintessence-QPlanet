using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class MailTemplateTagView
    {
        [DataMember]
        public string Tag { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}