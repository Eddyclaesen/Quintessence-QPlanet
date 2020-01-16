using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.Shared;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class EditProjectCandidateScoresActionModel
    {
        public ProjectCandidateView ProjectCandidate { get; set; }

        public AssessmentDevelopmentProjectView Project { get; set; }

        public List<CandidateScoreReportTypeView> ReportTypes { get; set; }

        public CandidateScoreReportTypeView ReportType { get; set; }

        public CandidateReportDefinitionView ReportDefinition { get; set; }

        public List<EditProjectCandidateClusterScoreModel> Clusters { get; set; }

        public List<EditProjectCandidateCompetenceScoreModel> Competences { get; set; }

        public List<ProjectCandidateIndicatorSimulationFocusedScoreView> ProjectCandidateIndicatorSimulationFocusedScores { get; set; }

        public List<ProjectCandidateIndicatorSimulationScoreView> ProjectCandidateIndicatorSimulationScores { get; set; }

        public List<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get; set; }

        public bool IsIndicatorScoringEnabled { get; set; }

        public IEnumerable<SimulationModel> GetSimulations(Guid? competenceId = null)
        {
            if (ProjectCandidateIndicatorSimulationFocusedScores != null && ProjectCandidateIndicatorSimulationFocusedScores.Count > 0)
            {
                return ProjectCandidateIndicatorSimulationFocusedScores
                    .Where(pcisfs => competenceId == null || pcisfs.DictionaryCompetenceId == competenceId)
                    .Select(Mapper.DynamicMap<SimulationModel>)
                    .Distinct(new SimulationModelComparer());
            }
            if (ProjectCandidateIndicatorSimulationScores != null && ProjectCandidateIndicatorSimulationScores.Count > 0)
            {
                return ProjectCandidateIndicatorSimulationScores
                    .Where(pcisfs => competenceId == null || pcisfs.DictionaryCompetenceId == competenceId)
                    .Select(Mapper.DynamicMap<SimulationModel>)
                    .Distinct(new SimulationModelComparer());
            }

            return new List<SimulationModel>(0);
        }

        public decimal? GetIndicatorSimulationAverage(Guid indicatorId)
        {
            if (ProjectCandidateIndicatorSimulationFocusedScores != null && ProjectCandidateIndicatorSimulationFocusedScores.Count > 0)
            {
                var average = ProjectCandidateIndicatorSimulationFocusedScores
                    .Where(pcisfs => pcisfs.DictionaryIndicatorId == indicatorId && pcisfs.Score.HasValue && pcisfs.Score.Value != 0)
                    .Average(pcisfs => pcisfs.Score);

                return average != null ? Math.Round(average.Value, 2) : average;
            }

            if (ProjectCandidateIndicatorSimulationScores != null && ProjectCandidateIndicatorSimulationScores.Count > 0)
            {
                var average = ProjectCandidateIndicatorSimulationScores
                    .Where(pcisfs => pcisfs.DictionaryIndicatorId == indicatorId && pcisfs.Score.HasValue && pcisfs.Score.Value != 0)
                    .Average(pcisfs => pcisfs.Score);

                return average != null ? Math.Round(average.Value, 2) : average;
            }

            return null;
        }

        public decimal? GetIndicatorSimulationScore(Guid indicatorId, SimulationModel simluation)
        {
            if (ProjectCandidateIndicatorSimulationFocusedScores != null && ProjectCandidateIndicatorSimulationFocusedScores.Count > 0)
            {
                var projectCandidateIndicatorSimulationScore = ProjectCandidateIndicatorSimulationFocusedScores.SingleOrDefault(
                    pcisfs => pcisfs.DictionaryIndicatorId == indicatorId
                              && pcisfs.SimulationSetId == simluation.SimulationSetId
                              && pcisfs.SimulationDepartmentId == simluation.SimulationDepartmentId
                              && pcisfs.SimulationLevelId == simluation.SimulationLevelId
                              && pcisfs.SimulationId == simluation.SimulationId);
                if (projectCandidateIndicatorSimulationScore != null)
                    return projectCandidateIndicatorSimulationScore.Score;
            }

            if (ProjectCandidateIndicatorSimulationScores != null && ProjectCandidateIndicatorSimulationScores.Count > 0)
            {
                var projectCandidateIndicatorSimulationScore = ProjectCandidateIndicatorSimulationScores.SingleOrDefault(
                    pcisfs => pcisfs.DictionaryIndicatorId == indicatorId
                              && pcisfs.SimulationSetId == simluation.SimulationSetId
                              && pcisfs.SimulationDepartmentId == simluation.SimulationDepartmentId
                              && pcisfs.SimulationLevelId == simluation.SimulationLevelId
                              && pcisfs.SimulationId == simluation.SimulationId);
                if (projectCandidateIndicatorSimulationScore != null)
                    return projectCandidateIndicatorSimulationScore.Score;
            }

            return null;
        }

        public IEnumerable<string> GetClusterRemarks(Guid dictionaryClusterId)
        {
            if (ProjectCandidateCompetenceSimulationScores != null)
            {
                return ProjectCandidateCompetenceSimulationScores
                    .Where(pccss => pccss.DictionaryClusterId == dictionaryClusterId)
                    .Select(pccss => pccss.Remarks);
            }

            return new List<string>(0);
        }

        public IEnumerable<string> GetCompetenceRemarks(Guid dictionaryCompetenceId)
        {
            if (ProjectCandidateCompetenceSimulationScores != null)
            {
                return ProjectCandidateCompetenceSimulationScores
                    .Where(pccss => pccss.DictionaryCompetenceId == dictionaryCompetenceId)
                    .Select(pccss => pccss.Remarks);
            }

            return new List<string>(0);
        }

        public decimal? GetCompetenceSimulationAverage(Guid competenceId)
        {
            var average = ProjectCandidateCompetenceSimulationScores
                .Where(pcisfs => pcisfs.DictionaryCompetenceId == competenceId)
                .Average(pcisfs => pcisfs.Score);

            return average != null ? Math.Round(average.Value, 2) : average;
        }

        public decimal? GetCompetenceSimulationScore(Guid competenceId, SimulationModel simluation)
        {
            var projectCandidateCompetenceSimulationScore = ProjectCandidateCompetenceSimulationScores.SingleOrDefault(
                pcisfs => pcisfs.DictionaryCompetenceId == competenceId
                          && pcisfs.SimulationSetId == simluation.SimulationSetId
                          && pcisfs.SimulationDepartmentId == simluation.SimulationDepartmentId
                          && pcisfs.SimulationLevelId == simluation.SimulationLevelId
                          && pcisfs.SimulationId == simluation.SimulationId);
            return projectCandidateCompetenceSimulationScore != null
                ? projectCandidateCompetenceSimulationScore.Score
                : null;
        }
    }
}