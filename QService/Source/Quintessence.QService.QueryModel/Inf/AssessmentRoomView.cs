using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class AssessmentRoomView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int OfficeId { get; set; }

        [DataMember]
        public string OfficeFullName { get; set; }

        [DataMember]
        public string OfficeShortName { get; set; }
    }
}