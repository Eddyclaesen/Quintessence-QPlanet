using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectRole : EntityBase
    {
        [StringLength(int.MaxValue, MinimumLength = 1)]
        [Required]
        [Display(Name = "Project role")]
        public string Name { get; set; }

        public int? ContactId { get; set; }
    }
}
