using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Quintessence.CulturalFit.DataModel.Cfi;

namespace Quintessence.CulturalFit.UI.Admin.Models.Admin
{
    public class CustomerRequestModel
    {
        public int ProjectId { get; set; }

        public int ContactId { get; set; }
        public string ContactName { get; set; }

        [DisplayName("First name")]
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Email { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "*")]
        public DateTime Deadline { get; set; }

        public GenderType Gender { get; set; }

        [DisplayName("Language")]
        public List<Language> Languages { get; set; }

        public int SelectedLanguageId { get; set; }

        public int SelectedGenderId { get; set; }

        [DisplayName("Type of questionnaire")]
        public List<TheoremListRequestType> TheoremListRequestTypes { get; set; }

        public int SelectedTheoremListRequestTypeId { get; set; }

        public List<string> ErrorMessages { get; set; }

        public Guid? ContactPersonId { get; set; }

        public Guid TheoremListRequestId { get; set; }

        public bool IsMailSent { get; set; }
    }
}