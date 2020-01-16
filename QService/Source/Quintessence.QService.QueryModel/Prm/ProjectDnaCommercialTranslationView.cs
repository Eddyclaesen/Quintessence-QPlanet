using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectDnaCommercialTranslationView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectDnaId { get; set; }

        [DataMember]
        public ProjectDnaView ProjectDna { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public string CommercialName { get; set; }

        [DataMember]
        public string CommercialRecap { get; set; }
    }
}