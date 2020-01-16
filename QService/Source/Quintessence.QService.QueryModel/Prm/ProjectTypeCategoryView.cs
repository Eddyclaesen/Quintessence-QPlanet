using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectTypeCategoryView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int? CrmTaskId { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int? SubCategoryType { get; set; }

        [DataMember]
        public int? Execution { get; set; }

        [DataMember]
        public bool IsMain { get; set; }

        [DataMember]
        public List<ProjectTypeCategoryDefaultValueView> ProjectTypeCategoryDefaultValues { get; set; }
    }
}