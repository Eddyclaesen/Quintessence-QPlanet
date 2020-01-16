using System;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class EditProjectCandidatesModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ProjectTypeCategoryId { get; set; }

        public string ProjectTypeCategoryName { get; set; }

        public bool HasSubProjectCategoryDetails { get; set; }
    }
}