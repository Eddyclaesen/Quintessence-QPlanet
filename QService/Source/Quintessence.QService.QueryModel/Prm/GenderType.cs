using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum GenderType
    {
        [EnumMember]
        [EnumMemberName("Male")]
        M = 1,

        [EnumMember]
        [EnumMemberName("Female")]
        F = 2,
    }
}