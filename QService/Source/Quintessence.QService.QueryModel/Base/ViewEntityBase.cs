using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Base
{
    [DataContract(IsReference = true)]
    public abstract class ViewEntityBase : IViewEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Audit Audit { get; set; }
    }
}