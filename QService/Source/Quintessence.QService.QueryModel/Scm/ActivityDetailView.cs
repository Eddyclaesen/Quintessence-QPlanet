using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ActivityDetailCoachingView))]
    [KnownType(typeof(ActivityDetailConsultingView))]
    [KnownType(typeof(ActivityDetailSupportView))]
    [KnownType(typeof(ActivityDetailTrainingView))]
    [KnownType(typeof(ActivityDetailWorkshopView))]
    public class ActivityDetailView : ActivityView
    {
        [DataMember]
        public string Description { get; set; }
    }
}
