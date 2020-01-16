using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectType2ProjectCategoryView
    {
        [DataMember]
        public Guid ProjectTypeId { get; set; }

        [DataMember]
        public Guid ProjectTypeCategoryId { get; set; }

        [DataMember]
        public bool IsMain { get; set; }
    }
}