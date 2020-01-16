using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ProductTypeView : ViewEntityBase
    {	
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal UnitPrice{ get; set; }
    }
}