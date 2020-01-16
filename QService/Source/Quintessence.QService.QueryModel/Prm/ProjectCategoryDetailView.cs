using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ProjectCategoryAcDetailView))]
    [KnownType(typeof(ProjectCategoryFaDetailView))]
    [KnownType(typeof(ProjectCategoryDcDetailView))]
    [KnownType(typeof(ProjectCategoryFdDetailView))]
    [KnownType(typeof(ProjectCategoryEaDetailView))]
    [KnownType(typeof(ProjectCategoryPsDetailView))]
    [KnownType(typeof(ProjectCategorySoDetailView))]
    [KnownType(typeof(ProjectCategoryCaDetailView))]
    [KnownType(typeof(ProjectCategoryCuDetailView))]
    [KnownType(typeof(ProjectCategoryDetailType1View))]
    [KnownType(typeof(ProjectCategoryDetailType2View))]
    [KnownType(typeof(ProjectCategoryDetailType3View))]
    public class ProjectCategoryDetailView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public ProjectView Project { get; set; }

        [DataMember]
        public Guid ProjectTypeCategoryId { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public int PricingModelId { get; set; }

        [DataMember]
        public ProjectTypeCategoryView ProjectTypeCategory { get; set; }
    }
}