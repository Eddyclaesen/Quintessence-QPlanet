using System;
using System.Collections.Generic;
using System.Linq;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class IndicatorSimulationsModel
    {
        public Dictionary<Guid, Dictionary<Guid, bool>> Matrix { get; set; }

        public List<ProjectCategoryDetailDictionaryIndicatorView> DictionaryIndicators { get; set; }

        public List<ProjectCategoryDetailSimulationCombinationView> SimulationCombinations { get; set; }

        public IEnumerable<IGrouping<Guid, ProjectCategoryDetailDictionaryIndicatorView>> DictionaryCompetences { get; set; }
    }
}