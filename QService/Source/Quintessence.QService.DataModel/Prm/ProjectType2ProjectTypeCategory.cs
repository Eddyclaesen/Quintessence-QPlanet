using System;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectType2ProjectTypeCategory
    {
        public Guid ProjectTypeId { get; set; }
        public Guid ProjectTypeCategoryId { get; set; }
        public bool IsMain { get; set; }
    }
}