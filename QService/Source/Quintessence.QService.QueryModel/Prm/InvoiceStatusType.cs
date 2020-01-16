using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum InvoiceStatusType
    {
        [EnumMember]
        [EnumMemberName("Planned")]
        Planned = 10,

        [EnumMember]
        [EnumMemberName("Not billable")]
        NotBillable = 15,

        [EnumMember]
        [EnumMemberName("Draft")]
        Draft = 20,
        
        [EnumMember]
        [EnumMemberName("To verify")]
        ToVerify = 30,
        
        [EnumMember]
        [EnumMemberName("Contested by proma")]
        ContestedByProma = 40,
        
        [EnumMember]
        [EnumMemberName("Ready for approval")]
        ReadyForApproval = 50,
        
        [EnumMember]
        [EnumMemberName("Sent for approval")]
        SentForApproval = 60,
        
        [EnumMember]
        [EnumMemberName("Contested by customer")]
        ContestedByCustomer = 70,
        
        [EnumMember]
        [EnumMemberName("Fixed price")]
        FixedPrice = 80,
        
        [EnumMember]
        [EnumMemberName("Ready for invoicing")]
        ReadyForInvoicing = 90,
        
        [EnumMember]
        [EnumMemberName("To check by KA")]
        CheckByCustomerAssistant = 95,
        
        [EnumMember]
        [EnumMemberName("Invoiced")]
        Invoiced = 100

    }
}
