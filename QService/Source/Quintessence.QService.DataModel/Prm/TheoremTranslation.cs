using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class TheoremTranslation : EntityBase
    {
        public Guid TheoremId { get; set; }
        public int LanguageId { get; set; }
        public string Quote { get; set; }

        #region Navigation properties
        public Theorem Theorem { get; set; }
        #endregion
    }
}