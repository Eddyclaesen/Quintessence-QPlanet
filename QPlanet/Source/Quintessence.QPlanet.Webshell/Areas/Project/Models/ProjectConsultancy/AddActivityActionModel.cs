using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class AddActivityActionModel
    {
        public List<ActivityTypeSelectListItemModel> ActivityTypes { get; set; }

        public Guid ActivityTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}