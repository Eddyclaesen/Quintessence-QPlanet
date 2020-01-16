using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sec
{
    [DataContract(IsReference = true)]
    public class OperationView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid OperationDomainId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
