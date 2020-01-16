using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectTypeCategoryDefaultValueView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectTypeCategoryId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public ProjectTypeCategoryView ProjectTypeCategory { get; set; }
    }
}
