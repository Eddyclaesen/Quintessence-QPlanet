using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class SearchCrmProjectResultItemView
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string CrmProjectName { get; set; }

        [DataMember]
        public string CrmProjectStatusName { get; set; }
    }
}
