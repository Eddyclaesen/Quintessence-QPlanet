using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum SubcategoryType
    {
        [EnumMember]
        [EnumMemberName("Type 1")]
        Type1 = 1,

        [EnumMember]
        [EnumMemberName("Type 2")]
        Type2 = 2,

        [EnumMember]
        [EnumMemberName("Type 3")]
        Type3 = 3
    }
}
