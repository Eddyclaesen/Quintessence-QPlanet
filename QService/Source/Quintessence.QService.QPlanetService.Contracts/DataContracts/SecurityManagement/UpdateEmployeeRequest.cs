using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement
{
    [DataContract]
    public class UpdateEmployeeRequest : UpdateUserRequest
    {
        [DataMember]
        public decimal HourlyCostRate { get; set; }
    }
}