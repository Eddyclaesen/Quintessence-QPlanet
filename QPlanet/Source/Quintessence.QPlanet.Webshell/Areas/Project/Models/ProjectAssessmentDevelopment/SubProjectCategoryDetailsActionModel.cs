using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class SubProjectCategoryDetailsActionModel
    {
        public List<EditProjectSubCategoryDetailModelBase> ProjectSubCategoryDetails { get; set; }

        public AssessmentDevelopmentProjectView Project { get; set; }

        public string ProjectTypeCategoryName { get; set; }

        public Guid? ProjectTypeCategoryId { get; set; }
    }
}