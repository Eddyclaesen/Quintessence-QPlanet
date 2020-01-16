using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectDna2CrmPerson : EntityBase
    {
        public Guid ProjectDnaId { get; set; }
        public int CrmPersonId { get; set; }
    }
}