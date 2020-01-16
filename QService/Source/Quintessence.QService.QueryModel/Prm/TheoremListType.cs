using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum TheoremListType
    {
        [EnumMember]
        [EnumMemberName("As is")]
        AsIs = 1,

        [EnumMember]
        [EnumMemberName("To be")]
        ToBe = 2
    }
}