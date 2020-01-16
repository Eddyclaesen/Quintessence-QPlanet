using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class Theorem : EntityBase
    {
        public Guid TheoremListId { get; set; }
        public bool IsLeastApplicable { get; set; }
        public bool IsMostApplicable { get; set; }

        #region Navigation properties
        public TheoremList TheoremList { get; set; }
        #endregion
    }
}