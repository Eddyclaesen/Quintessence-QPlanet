using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectRoleView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? ContactId { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public List<ProjectRoleTranslationView> ProjectRoleTranslations { get; set; }
    }
}
