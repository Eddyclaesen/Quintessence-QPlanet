using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditSimulationModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        public List<EditSimulationTranslationModel> SimulationTranslations { get; set; }
    }
}