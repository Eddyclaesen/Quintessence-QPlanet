using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmActiveProjectView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int AssociateId { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public int ProjectStatusId { get; set; }

        [DataMember]
        public CrmProjectStatusView ProjectStatus { get; set; }
    }
}
