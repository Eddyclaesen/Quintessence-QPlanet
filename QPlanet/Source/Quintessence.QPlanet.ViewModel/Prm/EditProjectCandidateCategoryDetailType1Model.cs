using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateCategoryDetailType1Model : EditProjectCandidateCategoryDetailTypeModel
    {
        [Display(Name="Scheduled date")]
        public DateTime? ScheduledDate { get; set; }

        [Display(Name = "Location")]
        public int OfficeId { get; set; }

        public string OfficeName
        {
            get { return Offices.SingleOrDefault(o => o.Value ==OfficeId.ToString()).Text; }
        }
    }
}