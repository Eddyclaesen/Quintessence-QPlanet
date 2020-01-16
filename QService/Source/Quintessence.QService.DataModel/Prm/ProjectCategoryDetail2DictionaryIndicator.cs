using System;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCategoryDetail2DictionaryIndicator
    {
        public Guid Id { get; set; }
        public Guid ProjectCategoryDetailId { get; set; }
        public Guid DictionaryIndicatorId { get; set; }
        public bool IsDefinedByRole { get; set; }
        public bool? IsStandard { get; set; }
        public bool? IsDistinctive { get; set; }
    }
}
