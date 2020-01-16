using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sec
{
    [DataContract(IsReference = true)]
    public class EmployeeView : UserView
    {
        [DataMember]
        public decimal HourlyCostRate { get; set; }
    }
}