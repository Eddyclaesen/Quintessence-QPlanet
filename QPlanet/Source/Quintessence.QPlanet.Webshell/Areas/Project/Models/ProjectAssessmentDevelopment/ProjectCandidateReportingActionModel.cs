using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class ProjectCandidateReportingActionModel
    {
        public ProjectCandidateView ProjectCandidate { get; set; }
        public ProjectView Project { get; set; }
        public CandidateReportDefinitionView CandidateReportDefinition { get; set; }
        public List<ReportDefinitionView> ReportDefinitions { get; set; }

        public Guid GetCandidateReportDefinitionId()
        {
            return CandidateReportDefinition != null ? CandidateReportDefinition.Id : Guid.Empty;
        }

        public bool MotivationScores { get; set; }
    }
}