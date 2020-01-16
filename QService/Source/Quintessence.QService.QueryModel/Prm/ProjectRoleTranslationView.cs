using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectRoleTranslationView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectRoleId { get; set; }

        [DataMember]
        public ProjectRoleView ProjectRole { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}