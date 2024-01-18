using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class ProjectRoiOrderView
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public Guid CompetenceId { get; set; }

        [DataMember]
        public int Order { get; set; }
    }
}
