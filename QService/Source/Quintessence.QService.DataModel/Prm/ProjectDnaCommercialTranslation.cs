using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectDnaCommercialTranslation : EntityBase
    {
        public Guid ProjectDnaId { get; set; }
        public int LanguageId { get; set; }
        public string CommercialName { get; set; }
        public string CommercialRecap { get; set; }
    }
}