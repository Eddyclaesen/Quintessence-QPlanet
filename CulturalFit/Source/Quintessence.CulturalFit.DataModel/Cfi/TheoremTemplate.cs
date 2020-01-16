using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremTemplate : IEntity
    {
        private List<TheoremTemplateTranslation> _theoremTemplateTranslations;

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TheoremListTemplateId { get; set; }

        [DataMember]
        public TheoremListTemplate TheoremListTemplate { get; set; }

        [DataMember]
        public Audit Audit { get; set; }
        
        [DataMember]
        public List<TheoremTemplateTranslation> TheoremTemplateTranslations
        {
            get { return _theoremTemplateTranslations ?? (_theoremTemplateTranslations = new List<TheoremTemplateTranslation>()); }
            set { _theoremTemplateTranslations = value; }
        }

        public string GetDefaultTranslation(int languageId)
        {
            var translation = TheoremTemplateTranslations.FirstOrDefault(ttt => ttt.LanguageId == languageId);

            if (translation == null)
                translation = TheoremTemplateTranslations.FirstOrDefault(ttt => ttt.IsDefault);

            if (translation == null)
                throw new NullReferenceException(string.Format("Unable to get the TheoremTemplateTranslation for TheoremTemplate with Id {0}", Id));

            return translation.Text;
        }
    }
}
