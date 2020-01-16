using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Base
{
    public interface IViewEntity
    {
        [DataMember]
        Guid Id { get; set; }

        [DataMember]
        Audit Audit { get; set; }
    }
}
