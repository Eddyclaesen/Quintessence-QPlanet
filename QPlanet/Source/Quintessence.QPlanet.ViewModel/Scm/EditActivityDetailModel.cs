using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityDetailModel : BaseEntityModel
    {
        public Guid ProjectId { get; set; }

        [Display(Name = "Description")]
        public virtual string Description { get; set; }

        public List<ActivityProfileView> ActivityProfiles { get; set; }
    }
}
