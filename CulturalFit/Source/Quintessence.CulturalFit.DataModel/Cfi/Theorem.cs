using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class Theorem : IEntity
    {
        private List<TheoremTranslation> _theoremTranslations;

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TheoremListId { get; set; }

        [DataMember]
        public bool IsLeastApplicable { get; set; }

        [DataMember]
        public bool IsMostApplicable { get; set; }
        
        [DataMember]
        public List<TheoremTranslation> TheoremTranslations
        {
            get { return _theoremTranslations ?? (_theoremTranslations = new List<TheoremTranslation>()); }
            set { _theoremTranslations = value; }
        }

        [DataMember]
        public TheoremList TheoremList { get; set; }

        [DataMember]
        public Audit Audit { get; set; }

        public TheoremTranslation AddTheoremTranslation(TheoremTranslation theoremTranslation = null)
        {
            if (theoremTranslation == null)
                theoremTranslation = new TheoremTranslation { Id = Guid.NewGuid() };

            TheoremTranslations.Add(theoremTranslation);
            theoremTranslation.TheoremId = Id;
            theoremTranslation.Theorem = this;
            return theoremTranslation;
        }

        public string GetTranslation(int languageId)
        {
            var translation = TheoremTranslations.FirstOrDefault(tl => tl.LanguageId == languageId);

            return translation != null ? translation.Quote : null;
        }
    }
}
