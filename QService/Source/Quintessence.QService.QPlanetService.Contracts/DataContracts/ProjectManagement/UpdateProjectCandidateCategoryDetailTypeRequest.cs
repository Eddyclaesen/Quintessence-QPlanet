using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    [KnownType(typeof(UpdateProjectCandidateCategoryDetailType1Request))]
    [KnownType(typeof(UpdateProjectCandidateCategoryDetailType2Request))]
    [KnownType(typeof(UpdateProjectCandidateCategoryDetailType3Request))]
    public class UpdateProjectCandidateCategoryDetailTypeRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public Guid ProjectCategoryDetailTypeId { get; set; }
        
        [DataMember]
        public decimal InvoiceAmount { get; set; }

        [DataMember]
        public string BceEntity { get; set; }
    }
}