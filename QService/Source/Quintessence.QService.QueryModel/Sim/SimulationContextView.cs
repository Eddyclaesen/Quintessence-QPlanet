using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationContextView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UserNameBase { get; set; }

        [DataMember]
        public int PasswordLength { get; set; }
    }
}