using System.Collections.Generic;
using Quintessence.CulturalFit.UI.Admin.Models.Admin.Entities;

namespace Quintessence.CulturalFit.UI.Admin.Models.Admin
{
    public class CandidateOverviewModel
    {
        /// <summary>
        /// Gets or sets the candidate entities.
        /// </summary>
        /// <value>
        /// The candidate entities.
        /// </value>
        public List<CandidateEntity> CandidateEntities { get; set; }

        public int? ContactId { get; set; }
    }
}