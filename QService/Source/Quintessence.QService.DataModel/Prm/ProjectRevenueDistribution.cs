using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectRevenueDistribution : EntityBase
    {
        public Guid ProjectId { get; set; }

        public int CrmProjectId { get; set; }

        public decimal? Revenue { get; set; }
    }
}