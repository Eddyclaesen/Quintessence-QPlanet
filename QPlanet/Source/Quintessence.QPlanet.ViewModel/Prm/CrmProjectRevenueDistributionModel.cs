using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CrmProjectRevenueDistributionModel : BaseEntityModel
    {
        public Guid ProjectId { get; set; }

        public int CrmProjectId { get; set; }

        public decimal? Revenue { get; set; }

        public string CrmProjectName { get; set; }

        public string CrmProjectStatusName { get; set; }
    }
}
