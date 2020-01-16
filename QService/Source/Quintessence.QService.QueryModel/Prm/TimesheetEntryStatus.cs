using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum TimesheetEntryStatus
    {
        [EnumMember]
        [EnumMemberName("Draft")]
        Draft = 100,

        [EnumMember]
        [EnumMemberName("To verify")]
        ToVerify = 110,

        [EnumMember]
        [EnumMemberName("Contested by proma")]
        ContestedByProma = 150,

        [EnumMember]
        [EnumMemberName("Ready for approval")]
        ReadyForApproval = 200,

        [EnumMember]
        [EnumMemberName("Sent for approval")]
        SentForApproval = 210,

        [EnumMember]
        [EnumMemberName("Ready for invoicing")]
        ReadyForInvoicing = 220,

        [EnumMember]
        [EnumMemberName("Contested by customer")]
        ContestedByCustomer = 250,

        [EnumMember]
        [EnumMemberName("Ready for import")]
        ReadyForImport = 300,

        [EnumMember]
        [EnumMemberName("Imported")]
        Imported = 400,

        [EnumMember]
        [EnumMemberName("Fixed price")]
        FixedPrice = 900,
    }
}
