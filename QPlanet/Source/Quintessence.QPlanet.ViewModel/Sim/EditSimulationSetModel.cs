﻿using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditSimulationSetModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
    }
}
