using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectDocumentMetadataView
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public Guid DocumentUniqueId { get; set; }
    }
}