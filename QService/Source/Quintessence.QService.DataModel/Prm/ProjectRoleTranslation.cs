using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectRoleTranslation : EntityBase
    {
        public Guid ProjectRoleId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
    }
}