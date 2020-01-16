using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectDnaView : ViewEntityBase
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string CrmProjectName { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ContactDepartment { get; set; }

        public string ContactFullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(ContactDepartment)
                    ? ContactName
                    : string.Format("{0} ({1})", ContactName, ContactDepartment);
            }
        }

        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public bool ApprovedForReferences { get; set; }

        [DataMember]
        public List<ProjectDnaSelectedTypeView> ProjectDnaTypes { get; set; }

        [DataMember]
        public List<ProjectDnaCommercialTranslationView> ProjectDnaCommercialTranslations { get; set; }

        [DataMember]
        public List<ProjectDnaSelectedContactPersonView> ProjectDnaContactPersons { get; set; }
    }
}