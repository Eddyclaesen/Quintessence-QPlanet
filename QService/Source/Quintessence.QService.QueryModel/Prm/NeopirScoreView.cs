using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class NeopirScoreView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int TestId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Scale { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public int NormScore { get; set; }
    }
}