using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum PricingModelType
    {
        [EnumMember]
        [EnumMemberName("Time & Material")]
        TimeAndMaterial = 1,

        [EnumMember]
        [EnumMemberName("Fixed price")]
        FixedPrice = 2
    }
}