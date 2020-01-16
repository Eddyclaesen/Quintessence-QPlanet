using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmProjectView
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

        [DataMember]
        public List<ProjectView> Projects { get; set; }

        [DataMember]
        public DateTime? BookyearFrom { get; set; }

        [DataMember]
        public DateTime? BookyearTo { get; set; }
    }
}
