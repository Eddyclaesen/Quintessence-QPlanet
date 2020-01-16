using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateCategoryInvoicingRequest : UpdateBaseInvoicingRequest
    {
        [DataMember]
        public int CategoryDetailType { get; set; }
    }
}