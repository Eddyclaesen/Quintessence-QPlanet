using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum ComplaintSeverityType
    {
        [EnumMember]
        [EnumMemberName("Low")]
        Low = 10,

        [EnumMember]
        [EnumMemberName("Medium")]
        Medium = 20,

        [EnumMember]
        [EnumMemberName("High")]
        High = 30
    }
}