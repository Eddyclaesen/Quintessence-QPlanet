using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class TheoremList : EntityBase
    {
        public int TheoremListTypeId { get; set; }
        public Guid TheoremListRequestId { get; set; }
        public string VerificationCode { get; set; }
        public bool IsCompleted { get; set; }

        #region Navigation properties
        public TheoremListRequest TheoremListRequest { get; set; }
        #endregion
    }
}