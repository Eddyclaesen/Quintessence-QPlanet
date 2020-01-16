using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateSubcategoryRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int? CrmTaskId { get; set; }

        [DataMember]
        public int? Execution { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int SubcategoryType { get; set; }

        [DataMember]
        public List<UpdateProjectTypeCategoryDefaultValueRequest> ProjectTypeCategoryDefaultValues { get; set; }
    }
}
