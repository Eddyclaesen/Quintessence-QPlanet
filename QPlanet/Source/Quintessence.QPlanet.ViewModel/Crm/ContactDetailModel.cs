using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Crm
{
    public class ContactDetailModel : BaseEntityModel
    {
        [Display(Name = "General information")]
        public string Remarks { get; set; }

        public Guid ProjectId { get; set; }
    }
}
