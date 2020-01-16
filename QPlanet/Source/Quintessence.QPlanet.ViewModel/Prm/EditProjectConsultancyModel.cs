using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectConsultancyModel : EditProjectModelBase
    {
        public List<ProjectStatusTypeSelectListItemModel> ProjectStatusses { get; set; }

        public Guid ProjectPlanId { get; set; }

        [Display(Name = "Project information")]
        public string ProjectInformation { get; set; }

        public bool IsCurrentUserProjectManager { get; set; }

        public bool IsCurrentUserCustomerAssistant { get; set; }
    }
}
