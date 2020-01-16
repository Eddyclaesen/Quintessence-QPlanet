using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Dim;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectAssessmentDevelopmentModel : EditProjectModelBase
    {
        private List<DictionarySelectListItemModel> _dictionaries;
        private List<ProjectCategorySelectListItemModel> _availableMainProjectCategories;
        private List<ProjectSubCategoryCheckboxItemModel> _availableProjectSubCategories;

        [Display(Name = "Function title")]
        public string FunctionTitle { get; set; }

        [Display(Name = "Function title [EN]")]
        public string FunctionTitleEN { get; set; }

        [Display(Name = "Function title [FR]")]
        public string FunctionTitleFR { get; set; }

        [Display(Name = "Function information")]
        public string FunctionInformation { get; set; }

        public Guid? ParentProjectId { get; set; }

        [Display(Name = "Subproject of")]
        public string ParentProjectName { get; set; }

        public string ParentProjectTypeName { get; set; }

        [Display(Name = "Dictionary")]
        public Guid? DictionaryId { get; set; }

        [Display(Name = "Main category")]
        public Guid? ProjectTypeCategoryId { get; set; }

        public List<ProjectStatusTypeSelectListItemModel> ProjectStatusses { get; set; }

        public List<DictionarySelectListItemModel> Dictionaries
        {
            get
            {
                return _dictionaries
                    ?? (_dictionaries = new List<DictionarySelectListItemModel> { new DictionarySelectListItemModel { Id = null, Name = string.Empty } });
            }
        }

        public List<ProjectCategorySelectListItemModel> AvailableMainProjectCategories
        {
            get { return _availableMainProjectCategories ?? (_availableMainProjectCategories = new List<ProjectCategorySelectListItemModel>()); }
            set { _availableMainProjectCategories = value; }
        }

        public List<ProjectSubCategoryCheckboxItemModel> AvailableProjectSubCategories
        {
            get { return _availableProjectSubCategories ?? (_availableProjectSubCategories = new List<ProjectSubCategoryCheckboxItemModel>()); }
            set { _availableProjectSubCategories = value; }
        }

        public string ProjectTypeCategoryName { get; set; }

        public bool HasSubProjectCategoryDetails { get; set; }

        public string ProjectTypeCategoryCode { get; set; }
    }
}
