using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Rep
{
    [DataContract(IsReference = true)]
    public class CandidateReportDefinitionView : ViewEntityBase
    {
        [DataMember]
        public int? ContactId { get; set; }

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
        public string Name { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public List<CandidateReportDefinitionFieldView> CandidateReportDefinitionFields { get; set; }
    }
}