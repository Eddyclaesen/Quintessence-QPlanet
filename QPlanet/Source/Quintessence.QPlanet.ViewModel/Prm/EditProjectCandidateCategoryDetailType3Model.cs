using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.Infrastructure.Nullable;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateCategoryDetailType3Model : EditProjectCandidateCategoryDetailTypeModel
    {
        [Display(Name = "Date")]
        public DateTime? Deadline { get; set; }

        [Display(Name = "Date")]
        public string DeadlineString
        {
            get { return Deadline.ToString("dd/MM/yyyy"); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Deadline = null;
                }
                else
                {
                    try { Deadline = DateTime.Parse(value); }
                    catch (Exception) { }
                }
            }
        }

        [Display(Name = "Login code")]
        public string LoginCode { get; set; }
    }
}