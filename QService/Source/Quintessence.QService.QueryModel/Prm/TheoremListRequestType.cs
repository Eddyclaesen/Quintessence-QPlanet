using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum TheoremListRequestType
    {
        [EnumMember]
        [EnumMemberName("As is")]
        AsIs = 1,

        [EnumMember]
        [EnumMemberName("As is & to be")]
        AsIsToBe = 2,
    }
}