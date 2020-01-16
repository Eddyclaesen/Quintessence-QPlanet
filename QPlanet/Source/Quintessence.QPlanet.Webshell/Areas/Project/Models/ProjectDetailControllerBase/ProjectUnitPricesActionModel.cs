using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class ProjectUnitPricesActionModel
    {
        public Guid ProjectId { get; set; }
        public List<EditProjectCategoryDetailModel> ProjectCategoryDetails { get; set; }
    }
}