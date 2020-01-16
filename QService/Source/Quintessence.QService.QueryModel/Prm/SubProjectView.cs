using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class SubProjectView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid MainProjectId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ProjectTypeName { get; set; }
    }
}
