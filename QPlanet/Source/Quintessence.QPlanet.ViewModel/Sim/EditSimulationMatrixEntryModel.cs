using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditSimulationMatrixEntryModel : BaseEntityModel
    {
        private Guid? _simulationDepartmentId;
        private Guid? _simulationLevelId;

        [Display(Name = "Simulation set")]
        [Required]
        public Guid SimulationSetId { get; set; }

        [Display(Name = "Simulation department")]
        public Guid? SimulationDepartmentId
        {
            get { return _simulationDepartmentId; }
            set { _simulationDepartmentId = value == Guid.Empty ? null : value; }
        }

        [Display(Name = "Simulation level")]
        public Guid? SimulationLevelId
        {
            get { return _simulationLevelId; }
            set { _simulationLevelId = value == Guid.Empty ? null : value; }
        }

        [Display(Name = "Simulation")]
        public Guid SimulationId { get; set; }

        [Display(Name = "Preparation time (min)")]
        [Required]
        [Range(0, 480)]
        public int Preparation { get; set; }

        [Display(Name = "Execution time (min)")]
        [Required]
        [Range(0, 480)]
        public int Execution { get; set; }

        [Display(Name = "QCandidateLayout")] 
        public int QCandidateLayout { get; set; }

        public IList<SimulationLanguageModel> SimulationLanguages { get; set; }

        public string SimulationSetName { get; set; }

        public string SimulationDepartmentName { get; set; }

        public string SimulationLevelName { get; set; }

        public string SimulationName { get; set; }

        public IEnumerable<SimulationSetView> SimulationSets { get; set; }

        public IEnumerable<QCandidateSelectListItemModel> QcCandidateLayouts
        {
            get { return QCandidateLayoutType.GetAll<QCandidateLayoutType>().Select(ss => new QCandidateSelectListItemModel(ss.Id, ss.Name));  }
        }

        public IEnumerable<SimulationDepartmentView> SimulationDepartments { get; set; }

        public IEnumerable<SimulationLevelView> SimulationLevels { get; set; }

        public IEnumerable<SimulationView> Simulations { get; set; }

        public IEnumerable<SimulationSetSelectListItemModel> SimulationSetSelectListItems
        {
            get { return SimulationSets.Select(ss => new SimulationSetSelectListItemModel(ss)); }
        }

        public IEnumerable<SimulationDepartmentSelectListItemModel> SimulationDepartmentSelectListItems
        {
            get
            {
                yield return new SimulationDepartmentSelectListItemModel();
                foreach (
                    var items in SimulationDepartments.Select(sd => new SimulationDepartmentSelectListItemModel(sd)))
                    yield return items;
            }
        }

        public IEnumerable<SimulationLevelSelectListItemModel> SimulationLevelSelectListItems
        {
            get
            {
                yield return new SimulationLevelSelectListItemModel();
                foreach (
                    var items in SimulationLevels.Select(sl => new SimulationLevelSelectListItemModel(sl)))
                    yield return items;
            }
        }

        public IEnumerable<SimulationSelectListItemModel> SimulationSelectListItems
        {
            get { return Simulations.Select(s => new SimulationSelectListItemModel(s)); }
        }
    }
}