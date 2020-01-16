using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremListTemplate : IEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        private List<TheoremTemplate> _theoremTemplates;

        [DataMember]
        public List<TheoremTemplate> TheoremTemplates
        {
            get { return _theoremTemplates ?? (_theoremTemplates = new List<TheoremTemplate>()); }
            set { _theoremTemplates = value; }
        }

        [DataMember]
        public Audit Audit { get; set; }

        public TheoremTemplate AddTheoremTemplate(TheoremTemplate theoremTemplate)
        {
            if (theoremTemplate == null)
                theoremTemplate = new TheoremTemplate { Id = Guid.NewGuid() };

            TheoremTemplates.Add(theoremTemplate);
            theoremTemplate.TheoremListTemplateId = Id;
            theoremTemplate.TheoremListTemplate = this;
            return theoremTemplate;
        }
    }
}