using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class ReportingActionModel : BaseEntityModel
    {
        public Guid? CandidateReportDefinitionId { get; set; }

        public List<CandidateReportDefinitionView> CandidateReportDefinitions { get; set; }

        public List<CandidateScoreReportTypeView> ReportTypes { get; set; }

        public int CandidateScoreReportTypeId { get; set; }

        public List<ReportDefinitionView> ReportDefinitions { get; set; }
    }
}