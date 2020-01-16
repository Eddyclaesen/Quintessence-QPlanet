using System;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectGeneral
{
    public class GenerateCandidateReportModel
    {
        /// <summary>
        /// Gets or sets the project candidate id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        public Guid CandidateReportDefinitionId { get; set; }

        public Guid? ScoringCoAssessorId { get; set; }

        public Guid AuditVersionId { get; set; }
    }
}