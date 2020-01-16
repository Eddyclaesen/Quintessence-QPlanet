using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class CreateMailTemplateRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string StoredProcedureName { get; set; }

        [DataMember]
        public string FromAddress { get; set; }

        [DataMember]
        public string BccAddress { get; set; }
    }
}