using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Sec
{
    public class EditEmployeeModel : EditUserModel
    {
        [Display(Name = "Hourly cost rate")]
        public decimal HourlyCostRate { get; set; }
    }
}