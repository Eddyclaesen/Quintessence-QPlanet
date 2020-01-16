using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ProductView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ProductTypeName { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}