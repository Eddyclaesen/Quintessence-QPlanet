using System;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremTranslation : IEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TheoremId { get; set; }

        [DataMember]
        public Theorem Theorem { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Language Language { get; set; }

        [DataMember]
        public string Quote { get; set; }

        [DataMember]
        public Audit Audit { get; set; }
    }
}
