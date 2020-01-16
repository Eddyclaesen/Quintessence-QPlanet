using System;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract]
    public class EmailTemplate : IEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Language Language { get; set; }

        [DataMember]
        public int TheoremListRequestTypeId { get; set; }

        [DataMember]
        public TheoremListRequestType TheoremListRequestType { get; set; }

        [DataMember]
        public Audit Audit { get; set; }
    }
}
