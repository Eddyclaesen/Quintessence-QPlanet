using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum ScoringTypeCodeType
    {
        [EnumMemberName("With indicators")]
        WithIndicators = 10,

        [EnumMemberName("Only competences")]
        WithoutIndicators = 20,
    }
}