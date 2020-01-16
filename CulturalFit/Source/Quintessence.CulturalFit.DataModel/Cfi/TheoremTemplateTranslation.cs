using System;
using System.Data.Services.Common;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremTemplateTranslation : IEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TheoremTemplateId { get; set; }

        [DataMember]
        public TheoremTemplate TheoremTemplate { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public Boolean IsDefault { get; set; }

        [DataMember]
        public Audit Audit { get; set; }
    }
}
