using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class LeaderScoreView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Instruct { get; set; }

        [DataMember]
        public int Convince { get; set; }

        [DataMember]
        public int Consult { get; set; }

        [DataMember]
        public int Delegate { get; set; }

        [DataMember]
        public int Effectivity { get; set; }

        [DataMember]
        public int Decile { get; set; }
    }
}