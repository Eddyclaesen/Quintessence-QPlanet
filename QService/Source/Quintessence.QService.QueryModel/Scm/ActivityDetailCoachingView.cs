using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailCoachingView : ActivityDetailView
    {
        [DataMember]
        public string TargetGroup { get; set; }

        [DataMember]
        public int SessionQuantity { get; set; }
    }
}
