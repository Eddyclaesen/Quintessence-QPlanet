using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationContextUserView : ViewEntityBase
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public DateTime ValidFrom{ get; set; }

        [DataMember]
        public DateTime ValidTo { get; set; }

        [DataMember]
        public Guid SimulationContextId { get; set; }
    }
}