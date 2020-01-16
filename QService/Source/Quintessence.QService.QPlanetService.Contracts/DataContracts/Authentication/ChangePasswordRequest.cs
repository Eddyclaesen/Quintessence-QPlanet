using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.Authentication
{
    [DataContract]
    public class ChangePasswordRequest
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string CurrentPassword { get; set; }

        [DataMember]
        public string NewPassword { get; set; }

        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}