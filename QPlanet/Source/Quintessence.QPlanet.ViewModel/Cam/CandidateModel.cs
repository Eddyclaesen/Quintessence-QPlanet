using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Cam
{
    public class CandidateModel : BaseEntityModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Reference { get; set; }

        public List<ProjectCandidateView> ProjectCandidates { get; set; }

        public List<LanguageView> Languages { get; set; }

        [Display(Name="Language")]
        public int LanguageId { get; set; }

        public List<SelectListItem> CreateGenderSelectListItems()
        {
            return Enum.GetValues(typeof(GenderType)).OfType<GenderType>().Select(g =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = g.ToString().ToUpperInvariant(),
                                                                                                Text = EnumMemberNameAttribute.GetName(g),
                                                                                                Selected = g.ToString() == Gender
                                                                                            }).ToList();
        }

        [Display(Name = "Has QCandidate Access")]
        public bool HasQCandidateAccess { get; set; }
    }
}
