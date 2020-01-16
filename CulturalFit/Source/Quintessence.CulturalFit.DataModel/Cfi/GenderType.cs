using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract]
    public enum GenderType
    {
        [EnumMember]
        M = 1,

        [EnumMember]
        V = 2
    }
}
