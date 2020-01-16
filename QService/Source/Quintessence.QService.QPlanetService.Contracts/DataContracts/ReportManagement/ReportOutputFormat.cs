using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public enum ReportOutputFormat
    {
        [EnumMember]
        Xml,

        [EnumMember]
        Csv,

        [EnumMember]
        Image,

        [EnumMember]
        Pdf,

        [EnumMember]
        MHtml,

        [EnumMember]
        Html4,

        [EnumMember]
        Html32,

        [EnumMember]
        Excel,

        [EnumMember]
        Word,
    }
}