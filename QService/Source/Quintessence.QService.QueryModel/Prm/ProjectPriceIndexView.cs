using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectPriceIndexView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public decimal Index { get; set; }
    }
}