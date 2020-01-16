using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ProjectComplaintResponse
    {
        [DataMember]
        public ProjectComplaintView ProjectComplaint { get; set; }

        [DataMember]
        public List<ComplaintTypeView> ComplaintTypes { get; set; }
    }
}