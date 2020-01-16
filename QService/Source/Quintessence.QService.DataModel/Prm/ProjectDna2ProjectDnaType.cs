using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectDna2ProjectDnaType : EntityBase
    {
        public Guid ProjectDnaId { get; set; }
        public Guid ProjectDnaTypeId { get; set; }
    }
}