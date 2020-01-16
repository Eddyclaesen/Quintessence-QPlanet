using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectTypeCategoryUnitPriceView: ViewEntityBase
    {
        [DataMember]
        public Guid ProjectTypeCategoryId { get; set; }

        [DataMember]
        public ProjectTypeCategoryView ProjectTypeCategory { get; set; }

        [DataMember]
        public Guid ProjectTypeCategoryLevelId { get; set; }

        [DataMember]
        public ProjectTypeCategoryLevelView ProjectTypeCategoryLevel { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}