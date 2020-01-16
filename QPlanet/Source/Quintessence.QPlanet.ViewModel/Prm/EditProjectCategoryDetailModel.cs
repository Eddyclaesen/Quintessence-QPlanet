using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCategoryDetailModel : BaseEntityModel
    {
        public bool ProjectTypeCategoryIsMain { get; set; }
        public string ProjectTypeCategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid ProjectTypeCategoryId { get; set; }
    }
}